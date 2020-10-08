using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace CreateCustodianDatabase.Helpers
{
    public class HttpClient2
    {
        private const string URL = "https://sub.domain.com/objects.json";
        private string urlParameters = "?api_key=123";


        public class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string EmailAddress { get; set; }
        }
        public static async Task DoIt2 (string uri)
        {
            try
            {
                HttpClient client = new HttpClient();
                uri = "https://localhost:44301/";
                client.BaseAddress = new Uri(uri);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/Employee/Get").Result;

                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                  //  Parse the response body.
                    var dataObjects = response.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll

                    var employees = JsonConvert.DeserializeObject<List<Employee>>(dataObjects);
                    
                    foreach (var d in employees)
                    {
                        Console.WriteLine("Email Address for ployee {0} is {1}",
d.LastName + ", " + d.FirstName, d.EmailAddress);

                    }
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch(Exception excp)
            {

            }

        }
        public static async Task Doit(string uri, NameValueCollection conn)
        {

            uri = "https://localhost:44301/";
await DoIt2(uri);
            return;// 


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         //   client.DefaultRequestHeaders.Add(new con)
            var data = JsonConvert.SerializeObject(new Employee { FirstName = "Clarence", LastName = "Brathwaite" });

            System.Net.Http.HttpContent content = new StringContent(data, UTF8Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("api/Employee/Post", content).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                //var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                //foreach (var d in dataObjects)
                //{
                //    Console.WriteLine("{0}", d.Name);
                //}
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}