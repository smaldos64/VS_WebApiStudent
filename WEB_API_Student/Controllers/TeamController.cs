using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEB_API_Student.Models;

namespace WEB_API_Student.Controllers
{
    public class TeamController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/Team
        public List<Object> GetTeams()
        {
            List<Team> TeamList = db.Teams.ToList();
            List<object> JsonList = new List<object>();

            foreach (Team Team_Object in TeamList)
            {
                var ListItem = new
                {
                    TeamID = Team_Object.TeamID,
                    TeamName = Team_Object.TeamName
                };
                JsonList.Add(ListItem);
            }
            return (JsonList);
        }

        // GET: api/Team/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Team
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Team/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Team/5
        public void Delete(int id)
        {
        }
    }
}
