using SaoToAngularAndCSharp.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaoToAngularAndCSharp.DAL.Repostories
{
    public interface IRepostory
    {
        Task<List<UserModel>> getAllUsers();
        Task<MessageHistoryModel> GetHistory(string sender, string reciver);
        Task<UserModel> GetUserByLogin(string username, string password);
        Task<string> MakeNewUser(string username, string password, string userDisplay);
        Task SendMessage(string sendById, string sentToId, string message);
    }
}