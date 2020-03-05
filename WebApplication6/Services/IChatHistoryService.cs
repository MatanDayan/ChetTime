using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChetTime.Services
{
    
        public interface IChatHistoryService
        {
            Dictionary<string, List<string>> Data { get; }
            void Add(string username, string msg);
            IEnumerable<string> GetHistory();
        }
    
}
