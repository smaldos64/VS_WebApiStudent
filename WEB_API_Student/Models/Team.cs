using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_API_Student.Models
{
    public class Team
    {
        public int TeamID { get; set; }

        public string TeamName { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public override string ToString()
        {
            return this.TeamID + " : " + this.TeamName;
        }
    }
}
