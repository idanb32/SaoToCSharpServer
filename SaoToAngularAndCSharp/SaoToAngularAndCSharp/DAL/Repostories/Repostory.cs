using MongoDB.Driver;
using SaoToAngularAndCSharp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaoToAngularAndCSharp.DAL.Repostories
{
    public class Repostory : IRepostory
    {
        private readonly IMongoCollection<UserModel> users;
        private readonly IMongoCollection<MessageHistoryModel> messageHistories;
        public Repostory(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionURL);
            var database = client.GetDatabase(settings.DatabaseName);
            users = database.GetCollection<UserModel>(settings.CollectionName).WithWriteConcern(WriteConcern.WMajority);
            messageHistories = database.GetCollection<MessageHistoryModel>(settings.CollectionName2).WithWriteConcern(WriteConcern.WMajority);
        }

        public async Task<List<UserModel>> getAllUsers()
        {
            return await users.Find(s => true).ToListAsync();
        }
        // maybe add user auth
        public async Task<string> MakeNewUser(string username, string password, string userDisplay)
        {
            var newUser = new UserModel() { Username = username, Password = password, UserDisplay = userDisplay,MessageHistoryies=new List<MessageConnectorModel>() };
            await users.InsertOneAsync(newUser);
            Console.WriteLine(newUser);
            return newUser.Id;
        }

        // maybe add user auth

        public async Task<UserModel> GetUserByLogin(string username, string password)

        {
            var filter = Builders<UserModel>.Filter.And(Builders<UserModel>.Filter.Eq(user => user.Username, username),
                Builders<UserModel>.Filter.Eq(user2 => user2.Password, password));
            var foundUser = await users.Find<UserModel>(filter).FirstOrDefaultAsync();
            return foundUser;
        }
        public async Task SendMessage(string sendById, string sentToId, string message)
        {
            MessageModel newMessage = new MessageModel(message, sendById);

            var messageHistory = await GetHistory(sendById, sentToId);
            if (messageHistory == null)
            {
                await CreateMessageHistory(sendById, sentToId, newMessage);
                return;
            }

            var updateHistory = Builders<MessageHistoryModel>.Update.Push("ChatLog", newMessage);
            var filterHistory = Builders<MessageHistoryModel>.Filter.Eq(e => e.Id, messageHistory.Id);

            var res = await messageHistories.FindOneAndUpdateAsync(filterHistory, updateHistory);
        }

        private async Task<MessageHistoryModel> CreateMessageHistory(string sendById, string sentToId, MessageModel newMessage)
        {
            var newMessageHistory = new MessageHistoryModel() { SentBy = sendById, SentTo = sentToId, ChatLog = new List<MessageModel> { newMessage } };
            await messageHistories.InsertOneAsync(newMessageHistory);
            var newHistoryId = newMessageHistory.Id;

            var messageConnectorForSender = new MessageConnectorModel() { MessageHistoryId = newHistoryId, OtherUserId = sentToId };
            var messageConnectorForReciver = new MessageConnectorModel() { MessageHistoryId = newHistoryId, OtherUserId = sendById };

            var updateSender = Builders<UserModel>.Update.Push("MessageHistoryies", messageConnectorForSender);
            var updateReciver = Builders<UserModel>.Update.Push("MessageHistoryies", messageConnectorForReciver);

            var filterForSender = Builders<UserModel>.Filter.Eq(e => e.Id, sendById);
            var filterForReciver = Builders<UserModel>.Filter.Eq(e => e.Id, sentToId);

            var resForSender = await users.FindOneAndUpdateAsync(filterForSender, updateSender);
            var resForReciver = await users.FindOneAndUpdateAsync(filterForReciver, updateReciver);
            return newMessageHistory;
        }

        private async Task<MessageHistoryModel> CreateMessageHistory(string sendById, string sentToId)
        {
            var newMessageHistory = new MessageHistoryModel() { SentBy = sendById, SentTo = sentToId };
            await messageHistories.InsertOneAsync(newMessageHistory);
            var newHistoryId = newMessageHistory.Id;

            var messageConnectorForSender = new MessageConnectorModel() { MessageHistoryId = newHistoryId, OtherUserId = sentToId };
            var messageConnectorForReciver = new MessageConnectorModel() { MessageHistoryId = newHistoryId, OtherUserId = sendById };

            var updateSender = Builders<UserModel>.Update.Push("MessageHistoryies", messageConnectorForSender);
            var updateReciver = Builders<UserModel>.Update.Push("MessageHistoryies", messageConnectorForReciver);

            var filterForSender = Builders<UserModel>.Filter.Eq(e => e.Id, sendById);
            var filterForReciver = Builders<UserModel>.Filter.Eq(e => e.Id, sentToId);

            var resForSender = await users.FindOneAndUpdateAsync(filterForSender, updateSender);
            var resForReciver = await users.FindOneAndUpdateAsync(filterForReciver, updateReciver);
            return newMessageHistory;
        }

        public async Task<MessageHistoryModel> GetHistory(string sender, string reciver)
        {
            var normalRes = await messageHistories.Find<MessageHistoryModel>(history => (history.SentBy == sender && history.SentTo == reciver)).FirstOrDefaultAsync();
            if (normalRes != null) return normalRes;
            var flipRes = await messageHistories.Find<MessageHistoryModel>(history => (history.SentBy == reciver && history.SentTo == sender)).FirstOrDefaultAsync();
            if (flipRes != null) return flipRes;
            return await CreateMessageHistory(sender, reciver);
        }
    }
}
