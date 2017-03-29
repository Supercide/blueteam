﻿using System;
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
         // var supercars = db.Supercars.Select(x => x).OrderByField(orderBy, asc).ToList();
          var supercars = db.Supercars.SqlQuery("SELECT * FROM Supercar ORDER BY " + (orderBy == "votes" ? "SupercarId" : orderBy) +(asc ? " ASC" : " DESC")).ToList();

          if (orderBy == "votes")
          {
              supercars = supercars.OrderByDescending(s => s.Votes.Count()).ToList();
          }

          var leaderboard = supercars.Select(s => new Leaderboard
          {
            SupercarId = 1,
            Make = "",
            Model = "",
            PowerKw = 1,
            TorqueNm = 1,
            ZeroToOneHundredKmInSecs = 1,
            TopSpeedKm = 1,
            Votes = 1
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

//public static class SomeExtensionClass
//{
//    public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool Ascending)
//    {
//        var param = Expression.Parameter(typeof(T), "p");
//        var prop = Expression.Property(param, SortField);
//        var exp = Expression.Lambda(prop, param);
//        string method = Ascending ? "OrderBy" : "OrderByDescending";
//        Type[] types = new Type[] { q.ElementType, exp.Body.Type };
//        var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
//        return q.Provider.CreateQuery<T>(mce);
//    }
//}
