using System;

namespace BLL.DTOs
{
    public class ReaderDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime DoB { get; set; }
    }
}