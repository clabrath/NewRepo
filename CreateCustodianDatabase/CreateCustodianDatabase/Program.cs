using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CreateCustodianDatabase.Models;
using CreateCustodianDatabase.Helpers;
using System.Net;
using Newtonsoft.Json;
using System.Globalization;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;

namespace CreateCustodianDatabase
{
    class Program
    {
        private static string InFileName =
            @"C:\Users\HT386LN\source\repos\NewRepo\CreateCustodianDatabase\CreateCustodianDatabase\us-500.csv";

        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");

                List<string> rawData = await Helper.LoadCSVFileIntoMemory(InFileName);
                List<Employee> employees = new List<Employee>();

                // just for the hell of it should use of different type of loop

                Console.WriteLine("\n\n====> Show usage of differnt loop methods  <====\n\n");

                for(var i=9; i<50; i+=2)// start at 9 end at 50 increment by 2
                {
                    Console.WriteLine("{0} --> {1}", i, rawData[i]);
                }

                // let's do some string manipulation 
                // I am just interested in the email address and web site

                var idx = 0;
                do
                {
                    var thisRecord = rawData[idx++];
                    try
                    {
                        if (thisRecord.Contains("@"))
                        {
                            var delim = ',';
                            var data = thisRecord.Split(delim);
                            var email = data[11].Replace("\"", "");
                            var website = data[12].Replace("\"", "");
                            var beg = email.IndexOf('@');// host name
                                                         //  var end = email.IndexOf( '\"', beg+1);

                            var hostname = email.Substring(beg);//, end-beg);

                            //var email = thisRecord.Substring(beg, end);
                            Console.WriteLine("email = " + email);
                            Console.WriteLine("host name = " + hostname);
                            Console.WriteLine("web site = " + website);
                        }
                    }
                    // catch and log the records with errors but contine with all valid records
                    catch(Exception excp)
                    {
                        Console.WriteLine("this record contains a parse error");
                    }
                }
                while (idx < 20);
                
                
                List<string> bigData = new List<string>();

                foreach (var t in rawData)
                {
                    if (t.Contains("first_name")) continue;
                     
                    var data3 = t.Replace("\",\"","|");
                    data3 = data3.Replace("\",", "|");
                    data3 = data3.Replace(",\"","|");

                    var delim = '|';
                    var dataElements = data3.Split(delim);

                    var firstName = dataElements[0].Replace("\"", "");// this is sample string operation
                    var lastName = dataElements[1].Replace("\"", "");// this is sample string operation

                    var address = dataElements[3].Replace("\"", ""); 
                    var city = dataElements[4].Replace("\"", "");
                    var state = dataElements[6].Replace("\"", "");
                    var zipcode = dataElements[7].Replace("\"", "");
                    var phone1 = dataElements[8].Replace("\"", "");
                    var phone2 = dataElements[9].Replace("\"", "");

                   var website = dataElements[11].Replace("\"", "");
                    var email = dataElements[10].Replace("\"", "");// this is sample string operation
        
                    new Employee(firstName, lastName, address, city, state, zipcode, email, website, phone1, phone2);
                }

                // so all employee data was successfully loaded in memory
                var employeesList = Employee.GetEmployees();

                var validEmployees = employeesList.Where(x => x.Email.Contains("@"));

                Console.WriteLine("{0} / {1} employees loaded with valid email address...",
                    validEmployees.Count(), employeesList.Count());

            }
            catch(Exception excp)
            {
                Console.WriteLine("Error encountered. Error code = " + excp.Message);
         //      return;
            }

            await DoIt();

       //     await SendRecordsToAzure();

        }


        public static async Task DoIt()
        {
           /* var response =
            * 
            * */
             await HttpClient2.Doit("https://localhost:44301/api/employee/post", new NameValueCollection()
            {

                { "firstname", "Clarence" },
                { "lastname", "Brathwaite" }
            });
        }




        ////public static class Http
        ////{
        ////    public static async Task<bool> Post(string url, NameValueCollection pairs)
        ////    {
        ////        byte[] response = null;

        ////        using (var httpClient = new HttpClient())
        ////        {
        ////            httpClient.BaseAddress = new Uri(url);
        ////            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        ////            httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

        ////            var content = new HttpContent(
        ////          new[]
        ////      {new KeyValuePair<string, string>("firstname", "Clarence"),
        ////            //new KeyValuePair<string, string>("client_id", "dataImporter"),
        ////            //new KeyValuePair<string, string>("client_secret", env!="dev" ? "PLACEHOLDER" : Constants.CLIENT_SECRET),
        ////            //new KeyValuePair<string, string>("scope", "platformApi"),
        ////            //new KeyValuePair<string, string>("grant_type", "password"),
        ////            //new KeyValuePair<string, string>("username", userName),
        ////            //new KeyValuePair<string, string>("password", password)
        ////      }); ;



        ////            var resp = await httpClient.PostAsync(url, content);

        ////         //   var data = await resp.ReadAsStringAsync();
        ////        }

        ////        return true;
        ////    }
        ////}


            

        public async static Task SendRecordsToAzure()
        {
            //  this is the static list of Employees with valid email addresses
            var azureEmployees = Employee.GetEmployees();

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://localhost:44301/");
            // here is another example of string manupalation converting to a json string
            // we do a POST action to upload the data to the web API
            // no need to send all the data just 50 records

            var jsonList = new List<string>();

            foreach(var emp in azureEmployees.Take(50))
            {
                // DTO are efficient in web development by just sending the data you need to the server
                // reducing the payload in the HTTP request
                // in the future this payload could be send all at once as a json array
                // this implementation will make 50 calls to Middle Tier
                // using a JSON Array reduces it to 1 call
                var json = JsonConvert.SerializeObject(emp);
                jsonList.Add(json);
            }

           //// var content = new FormUrlEncodedContent(
           ////     new[] {


           ////         new KeyValuePair<string, string>("Content-Type","application/json"),
           //////     new KeyValuePair<string, string>("JsonArray", jsonList[0])
           ////     });

           ////// await client.PostAsync("api/employee/post", content);

           //// var client= new HttpClient()
           //// {
                
           ////  //   DefaultRequestHeaders = { ContentType=new System.Net.Mime.ContentType("application/json" ) },
           ////     BaseAddress = new Uri("https://localhost:44301/"),

           //// //    httpClient.BaseAddress = new Uri(url);
           //// DefaultRequestHeader = {}
           //// httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");



        ////};

        ////   // client.

        ////    var response2 = await client.PostAsync("api/employee/post", content);

        ////    //var response = client.PostAsync(new HttpRequestMessage
        ////    //{
        ////    //    Method = HttpMethod.Post,
        ////    //    RequestUri = new Uri("https://localhost:44301/api/Employee/Post")
        ////    //}).Result;

        ////    var data = await response2.Content.ReadAsStringAsync();

        ////    var endDate = DateTime.Now;
        }
    }
}
