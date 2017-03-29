using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Web.Filters;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
  [InitialiseDatabase]
  public class SupercarController : Controller
  {
    private SupercarModelContext db = new SupercarModelContext();

    public ActionResult Index(int id = 0)
    {
      var supercar = db.Supercars
                       .Include("Make")
                       .Include("Votes")
                       .Include("Votes.User")
                       .SingleOrDefault(s => s.SupercarId == id);

      if (supercar == null)
      {
        return HttpNotFound();
      }

      ViewBag.TotalVotes = (decimal)db.Votes.Count();

      if (User.Identity.IsAuthenticated)
      {
        var profile = db.UserProfiles.SingleOrDefault(u => u.Email == User.Identity.Name);
        if (profile != null)
        {
          ViewBag.FirstName = profile.FirstName;
          ViewBag.LastName = profile.LastName;
        }
      }

      return View(supercar);
    }

    public ActionResult Leaderboard(string orderBy = "votes", bool asc = true)
    {
      try
      {
          var supercars = db.Supercars.OrderBy(x => orderBy == "votes" ? x.Votes.Count :
                                                    orderBy == "PowerKw" ? x.PowerKw :
                                                    orderBy == "TorqueNm" ? x.TorqueNm :
                                                    orderBy == "ZeroToOneHundredKmInSecs" ? x.ZeroToOneHundredKmInSecs :
                                                    orderBy == "TopSpeedKm" ? x.TopSpeedKm : 
                                                    x.Votes.Count).ToList();
          if (!asc)
          {
              supercars.Reverse();
          }

          var leaderboard = supercars.Select(s => new Leaderboard
          {
            SupercarId = s.SupercarId,
            Make = s.Make.ToString(),
            Model = s.Model,
            PowerKw = s.PowerKw,
            TorqueNm = s.TorqueNm,
            ZeroToOneHundredKmInSecs = s.ZeroToOneHundredKmInSecs,
            TopSpeedKm = s.TopSpeedKm,
            Votes = s.Votes.Count
          });

        return View(leaderboard);
      }
      catch (SqlException)
      {
        return View("LeaderBoardError");
      }
    }
  }
}