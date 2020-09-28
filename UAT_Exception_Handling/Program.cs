using System;
using System.IO;

namespace UAT_Exception_Handling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World. UAT Class Exception Handling Project!");

            try
            {
                using (StreamReader sw = new StreamReader("us-500.csv"))
                {
                    string buffer = string.Empty;
                    while ((buffer = sw.ReadLine()) != null)
                    {
                        Console.WriteLine(buffer);
                    }
                }
            }
            catch(Exception excp)
            {
                Console.WriteLine("error : " + excp.Message);
            }
        }
    }
}
