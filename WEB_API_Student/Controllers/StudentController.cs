using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEB_API_Student.Models;
using Newtonsoft.Json;
using System.Web.Http.Cors;

namespace WEB_API_Student.Controllers
{
    [EnableCors(origins: "http://localhost:63744", headers: "*", methods: "*" )]
    public class StudentController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET api/GetStudents
        /// <summary>
        /// På denne funktionalitet er der lavet dokumentation. Se i filen StudentController.cs
        /// </summary>
        /// <returns>Liste af studerende</returns>
        /// 
        //[EnableCors("http://localhost:63744",
        //    null,
        //    "GET",
        //    "bar",
        //    SupportsCredentials = true)]
        [HttpGet]
        public List<Object> GetStudents()
        {
            List<Student> StudentList = db.Students.ToList();
            List<object> JsonList = new List<object>();
            //List<List<object>> JsonListCourse = new List<List<object>>();

            foreach (Student Student_Object in StudentList)
            {
                var ListItem = new
                {
                    StudentID = Student_Object.StudentID,
                    StudentName = Student_Object.StudentName,
                    StudentLastName = Student_Object.StudentLastName,
                    TeamID = Student_Object.TeamID,
                    TeamName = Student_Object.Team.TeamName,
                    CourseIDList = new List<int>(),
                    CourseNameList = new List<string>()
                };
                foreach (Course Course_Object in Student_Object.Courses)
                {
                    ListItem.CourseIDList.Add(Course_Object.CourseID);
                    ListItem.CourseNameList.Add(Course_Object.CourseName);
                }
                JsonList.Add(ListItem);
                // JsonList.Add(Student_Object); Giver jSon fejl, når man sender de enkelte
                // Studenter i listen som der er. Det vil sige med relationer. Det kan jSon ikke
                // klare.
            }
            return (JsonList);
        }

        // GET api/student/5
        public Object GetStudent(int ID)
        {
            Student Student_Object = db.Students.Find(ID);
            object jSonnObject = new object();

            var ListItem = new
            {
                StudentID = Student_Object.StudentID,
                StudentName = Student_Object.StudentName,
                StudentLastName = Student_Object.StudentLastName,
                TeamID = Student_Object.TeamID,
                TeamName = Student_Object.Team.TeamName,
                CourseIDList = new List<int>(),
                CourseNameList = new List<string>()
            };
            foreach (Course Course_Object in Student_Object.Courses)
            {
                ListItem.CourseIDList.Add(Course_Object.CourseID);
                ListItem.CourseNameList.Add(Course_Object.CourseName);
            }

            jSonnObject = ListItem;

            return (jSonnObject);
        }


        // POST api/student
        //public bool Post(Student Student_Object)
        public bool Post(dynamic jSon_Object)
        {
            Student Student_Object = new Student();
            Student_Object.Courses = new List<Course>();

            Student_Object.StudentName = jSon_Object.StudentName;
            Student_Object.StudentLastName = jSon_Object.StudentLastName;
            Student_Object.TeamID = jSon_Object.TeamID;

            List<int> ItemEntryListInt = new List<int>();

            try
            {
                for (int Counter = 0; Counter < jSon_Object.CourseIDList.Count; Counter++)
                {
                    ItemEntryListInt.Add(Convert.ToInt32(jSon_Object.CourseIDList[Counter]));
                }

                List<Course> CourseList = db.Courses.Where(i => ItemEntryListInt.Contains(i.CourseID)).ToList();

                Student_Object.Courses.AddRange(CourseList);
            }
            catch (Exception error)
            {

            }

            db.Students.Add(Student_Object);
            db.SaveChanges();
            
            return (true);

            // Den udkommenterede kode herunder kan man bruge, hvis ikke man skal
            // håndtere data i mange til mange relationen mellem student og 
            // Fag (Coures).

            //try
            //{
            //    if (null != Student_Object)
            //    {

            //        if ((!String.IsNullOrEmpty(Student_Object.StudentName)) &&
            //            (!String.IsNullOrEmpty(Student_Object.StudentLastName)) &&
            //            (0 != Student_Object.TeamID))
            //        {
            //            db.Students.Add(Student_Object);
            //            NumberOfStudentsSaved = db.SaveChanges();

            //            if (1 == NumberOfStudentsSaved)
            //            {
            //                return (true);
            //            }
            //            else
            //            {
            //                return (false);
            //            }
            //        }
            //        else
            //        {
            //            return (false);
            //        }
            //    }
            //    else
            //    {
            //        return (false);
            //    }
            //}
            //catch (Exception Error)
            //{
            //    return (false);
            //}
        }

        // PUT api/student/5
        //public bool Put(int id, Student Student_Object)
        public bool Put(int id, dynamic jSon_Object)
        {
            Student Student_Object = db.Students.Find(id);

            Student_Object.StudentName = jSon_Object.StudentName;
            Student_Object.StudentLastName = jSon_Object.StudentLastName;
            Student_Object.TeamID = jSon_Object.TeamID;

            List<int> ItemEntryListInt = new List<int>();

            try
            {
                for (int Counter = 0; Counter < jSon_Object.CourseIDList.Count; Counter++)
                {
                    ItemEntryListInt.Add(Convert.ToInt32(jSon_Object.CourseIDList[Counter]));
                }

                List<Course> CourseList = db.Courses.Where(i => ItemEntryListInt.Contains(i.CourseID)).ToList();

                Student_Object.Courses.Clear();
                Student_Object.Courses.AddRange(CourseList);
            }
            catch (Exception error)
            {

            }

            db.SaveChanges();

            return (true);

            // Den udkommenterede kode herunder kan man bruge, hvis ikke man skal
            // håndtere data i mange til mange relationen mellem student og 
            // Fag (Coures).

            //try
            //{
            //    if (null != Student_Object)
            //    {
            //        if ((!String.IsNullOrEmpty(Student_Object.StudentName)) &&
            //             (!String.IsNullOrEmpty(Student_Object.StudentName)) &&
            //             (0 != Student_Object.TeamID))
            //        {
            //            Student Student_ObjectFromDB = db.Students.Find(id);

            //            Student_ObjectFromDB.StudentName = Student_Object.StudentName;
            //            Student_ObjectFromDB.StudentLastName = Student_Object.StudentLastName;
            //            Student_ObjectFromDB.TeamID = Student_Object.TeamID;

            //            NumberOfStudentsSaved = db.SaveChanges();

            //            if (1 == NumberOfStudentsSaved)
            //            {
            //                return (true);
            //            }
            //            else
            //            {
            //                return (false);
            //            }
            //        }
            //        else
            //        {
            //            return (false);
            //        }
            //    }
            //    else
            //    {
            //        return (false);
            //    }
            //}
            //catch (Exception Error)
            //{
            //    return (false);
            //}
        }

        // DELETE api/students/5
        public bool Delete(int id)
        {
            int NumberOfPostsDeleted;

            Student Student_Object = db.Students.Find(id);

            try
            {
                if (null != Student_Object)
                {
                    db.Students.Remove(Student_Object);
                    NumberOfPostsDeleted = db.SaveChanges();
                    if (1 == NumberOfPostsDeleted)
                    {
                        return (true);
                    }
                    else
                    {
                        return (false);
                    }
                }
                else
                {
                    return (false);
                }
            }
            catch (Exception Error)
            {
                return (false);
            }
        }
    }
}
