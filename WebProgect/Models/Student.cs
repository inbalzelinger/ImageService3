using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Student
    {
        string id;
        string lastName;
        string firstName;


        public Student(string firstName, string lastName, string id)
        {
            this.id = id;
            this.lastName = lastName;
            this.firstName = firstName;

        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ID")]
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }
    }
}
