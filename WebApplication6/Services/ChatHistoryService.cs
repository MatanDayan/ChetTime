using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChetTime.Services
{
    public class ChatHistoryService : IChatHistoryService
    {

        public ChatHistoryService()
        {
            Data = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> Data { get; }

        public void Add(string username, string msg)
        {
            if (Data.ContainsKey(username))
            {
                Data[username].Add(msg);
            }
            else
            {
                Data.Add(username, new List<string>() { msg });

            }

        }

        public IEnumerable<string> GetHistory()
        {
            List<string> allMessages = new List<string>();

            foreach (var item in Data)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    allMessages.Add($"{item.Key} : {item.Value[i]}");
                }
            }

            return allMessages;
            
        }
    }
}
