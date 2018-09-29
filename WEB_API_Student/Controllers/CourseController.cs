using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEB_API_Student.Models;

namespace WEB_API_Student.Controllers
{
    public class CourseController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/Course
        public List<Object> GetCourses()
        {
            List<Course> CourseList = db.Courses.ToList();
            List<object> JsonList = new List<object>();

            foreach (Course Course_Object in CourseList)
            {
                var ListItem = new
                {
                    CourseID = Course_Object.CourseID,
                    CourseName = Course_Object.CourseName
                };
                JsonList.Add(ListItem);
            }
            return (JsonList);
        }

        // GET: api/Course/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Course
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Course/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Course/5
        public void Delete(int id)
        {
        }
    }
}
