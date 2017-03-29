using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Web.Models;

namespace Web.Controllers
{
  public class VoteController : ApiController
  {
        private SupercarModelContext db = new SupercarModelContext();

        // POST api/Vote
      public HttpResponseMessage Post(Vote vote)
      {
          if (!ModelState.IsValid)
          {
              throw new Exception();
          }
          if (!User.Identity.IsAuthenticated)
          {
              return Request.CreateResponse(HttpStatusCode.Forbidden);
          }

          //var connString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
          //var sqlString = "INSERT INTO Vote(UserId, SupercarId, Comments) VALUES (" + vote.UserId + ", " +
          //                vote.SupercarId + ", '" + vote.Comments + "')";

          //using (var conn = new SqlConnection(connString))
          //{
          //    var command = new SqlCommand(sqlString, conn);
          //    command.Connection.Open();
          //    command.ExecuteNonQuery();
          //}

          db.Votes.Add(new Vote()
          {
              UserId = vote.UserId,
              SupercarId = vote.SupercarId,
              Comments = vote.Comments
          });

          db.SaveChanges();

          return Request.CreateResponse(HttpStatusCode.Created);
      }
  }
}