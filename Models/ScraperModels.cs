using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ScraperModels
    {
        public class Lead
        {
            public string Website { get; set; }
            public string Email { get; set; }
            public string Facebook { get; set; }
            public string Twitter { get; set; }
            public string LinkedIn { get; set; }
            public string Instagram { get; set; }
            public List<string> ListEmails { get; set; }
        }

        public class ScrapeProxy
        {
            public int id { get; set; }
            public string Address { get; set; }
            public int Port { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public int NumberThreads { get; set; }
        }

        public class GlobalVariables
        {
            public int ConnectionLimit { get; set; }
        }
    }
}
