using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaoToAngularAndCSharp.Hubs
{
    public class ChatHub : Hub
    {
        public Dictionary<string,string> ClientsByMongoId { get; set; }

        public  Task OnConnectedAsync(string id)
        {
            ClientsByMongoId.Add(id, Context.ConnectionId);
            return  base.OnConnectedAsync();
        }

        public override Task OnConnectedAsync()
        {
            var currentClientId = ClientsByMongoId.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            ClientsByMongoId.Remove(currentClientId);
            return base.OnConnectedAsync();
        }
        public async Task SendMessage(string userId , string message)
        {
            var clientConnectionId = ClientsByMongoId[userId];
            var currentClientId = ClientsByMongoId.FirstOrDefault(x=> x.Value== Context.ConnectionId).Key;
            await Clients.Client(clientConnectionId).SendAsync("ReciveMessage", currentClientId, message);
        }

        public async Task GetOnlineUsers(string id)
        {
            var onlineIds = ClientsByMongoId.Keys;
            await Clients.Client(Context.ConnectionId).SendAsync("GetOnlineUsers", onlineIds);
        }

        // get offline need to make

        // user clicked need to make

        // sign off need to make
        
        // need to make all of the sheshbesh hub

    }
}
