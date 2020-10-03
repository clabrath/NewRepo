using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace CreateCustodianDatabase.Helpers
{
    public static class Logger
    {
        private static WebClient _client = new WebClient();
      //  public string Message { get; set; }
        public static async Task Log(string msg) {
            try
            {
                // let's use a web client to log messages
                using (WebClient client = new WebClient())
                {
                    client.BaseAddress = "";
                    ////var url = new Uri { AbsoluteUri = "Log" };
                    ////var data = JsonConvert.SerializeObject(msg);
                    ////var response = await client.UploadDataAsync(url, data);

                }
            }
            catch (Exception excp)
            {
                throw excp;
            }

    }
    }
}
