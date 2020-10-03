using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAT_ClassProjectWebApplication.Models
{
    public class MessageType
    {
        public string Type { get; set; } 
    }
    public class Notification
    {
        public string Message { get; set; }
        public MessageType MsgType { get; set; }
        public string Sender { get; set; }
        public List<Employee> Recepients { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class Exceptions
    {
        public int Level { get; set; }
    }
    public class Log
    {
        public string Information { get; set; }
        public Exception Exceptions { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
