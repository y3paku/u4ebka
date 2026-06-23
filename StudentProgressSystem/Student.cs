using System;

namespace StudentProgressSystem.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
    }
}