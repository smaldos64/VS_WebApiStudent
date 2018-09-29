using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_API_Student.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        public string StudentName { get; set; }

        public string StudentLastName { get; set; }

        public int TeamID { get; set; }
        public virtual Team Team { get; set; }

        public virtual List<Course> Courses { get; set; }
    }
}
