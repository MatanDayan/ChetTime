using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChetTime.Models;
using ChetTime.Services;

namespace ChetTime.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IChatHistoryService history;

        public ChatHub(IChatHistoryService history)
        {
            this.history = history;
        }

        public async Task GetHistory()
        {
            try
            {
                var ret = history.GetHistory();
                await Clients.Caller.SendAsync("SetHistory", ret);
            }
            catch(Exception ex)
            {

            }
        }

        public async Task SendMessage(string user, string message)
        {
            try
            {
                history.Add(user, message);

                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
 