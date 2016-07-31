using System;

namespace Library10.Common.Entities
{
    public class ApplicationError
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Address { get; set; }
        public string Request { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}