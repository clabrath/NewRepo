using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
// this project is about exceptional handling in .NET
// Im starting this @ 10:41 PM Wednesday the 7th of October
// I'm thinking this should take less than an hour
// ok.  I'm done it's 11:26 PM

namespace UAT_Class_Assignment_Exceptional_Handling
{
    public class CustomDivisionByZero: System.Exception
    {
        public string Message = "(3rd) My personal trap for divide by zero.  You can't devide by zero";

    }
    public class CustomFileNotFound: System.Exception
    {
        public string Message = "(1st) Looks like a successful try for this error.  The filename you're looking for does not exist";
    }
    public class EmailAddressException: System.Exception
    {
        public string Message = "(2nd) Looks like a successful try for this error.  This email address is invalid";
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            try
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\temp\junk.txt"))
                {
                    sw.WriteLine();
                }
              //  var filename = new System.IO.StreamReader(@"C:\temp\old_data");
                if (System.IO.File.Exists(@"c:\temp\junk.txt2") == false)
                {
                    throw new CustomFileNotFound() ;
                }
                using (System.IO.StreamReader r = new System.IO.StreamReader(@""))
                {
                    var buffer = r.ReadToEnd();
                }

            }
            catch(CustomFileNotFound excp)
            {
                Console.WriteLine(excp.Message);
            }
            try
            {
                var email = "Clarence.Brathwaite@EY.COM";
                if (email.Contains("@"))
                {
                    throw new EmailAddressException();
                }
            }

            catch (EmailAddressException excp)
            {
                Console.WriteLine(excp.Message);
            }

            try
            {
                var twoThousand = 2000;
                var zero = 0;
               
                if (zero <= 0)
                    throw new CustomDivisionByZero(); 
                var result = twoThousand / zero;
//                var divideByZero = 2000 / 0;// interesting how smart .NET is wouldn't let you do this
            }
            catch(CustomDivisionByZero excp)
            {
                // throw new System.Exception(excp.Message);

                // you can throw this error to the caller but in this case we'll orint it here
                Console.WriteLine(excp.Message);
            }
        }
    }
}
