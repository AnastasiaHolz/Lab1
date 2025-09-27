using System;
using ConsoleApp.Utils;

namespace ConsoleApp.Models
{
    public class Student : Person, ISkill
    {
        public string StudentId { get; private set; }
        public int Course       { get; private set; }
        public string Gender    { get; private set; } 
        public string Residence { get; private set; }
        
        public string Address { get; set; }


        public Student(string firstName, string lastName, string studentId, int course, string gender, string residence)
            : base(firstName, lastName)
        {
            if (!Validators.ValidName(firstName)) throw new ArgumentException("Invalid first name");
            if (!Validators.ValidName(lastName)) throw new ArgumentException("Invalid last name");
            if (!Validators.ValidStudentId(studentId)) throw new ArgumentException("Invalid student id");
            if (!Validators.ValidCourse(course)) throw new ArgumentException("Invalid course");

            StudentId = studentId;
            Course = course;
            Gender = gender;
            Residence = residence;
        }

        public override void Study()
        {
            if (Course < 6) Course++;
        }

        public void Skate()
        {
          
        }
    }
}
