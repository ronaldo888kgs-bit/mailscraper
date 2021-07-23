using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MailScraper
{
    public class TimedWebClient : WebClient
    {
        // Timeout in milliseconds, default = 600,000 msec
        public int Timeout { get; set; }

        public TimedWebClient()
        {
            this.Timeout = 60 * 1000;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {

            try {
                var objWebRequest = base.GetWebRequest(address);
                objWebRequest.Timeout = this.Timeout;   
                return objWebRequest;
            }
            catch (Exception e) {
                return null;
            }
            
        }
    }

}
