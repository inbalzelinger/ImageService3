using System;
using System.Collections.Generic;
namespace ContosoUniversity.Models
{
    public class Student {
        int id;
        string lastName;
        string firstName;
    

        public Student(int id, string lastName, string firstName)
        {
        this.id = id;
            this.lastName = lastName;
            this.firstName = firstName;

        }
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
    }
}
