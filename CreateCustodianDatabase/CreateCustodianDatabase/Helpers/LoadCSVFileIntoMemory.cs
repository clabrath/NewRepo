using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustodianDatabase.Helpers
{
    public static partial class Helper
    {
        public static /* async Task<List<string>>*/
            List<string> LoadCSVFileIntoMemory(string fileName)
        {
            // add exception handling to reader
            try
            {
                List<string> data = new List<string>();
                using (StreamReader r = new StreamReader(fileName))
                {
                    var buffer = string.Empty;
                    while ((buffer = r.ReadLine()) != null)
                    {
                        Console.WriteLine(buffer);
                        data.Add(buffer);
                    }
                    // let's log the fact that we loaded all records successfully
                    var msg = String.Format("{0} records successfully load from CSV file...", data.Count);
                    //                    Logger.Log(msg);
                }
                return data;
                // return await data;
            }
            catch (Exception excp)
            {
                throw excp;
            }
        }
    }
}