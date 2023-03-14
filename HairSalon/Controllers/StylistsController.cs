using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller{
    private readonly HairSalonContext _db;

    public StylistsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Stylist> model = _db.Stylists.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Stylist stylist)
    {
      _db.Stylists.Add(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Stylist thisStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
      return View(thisStylist);
    }

    [HttpPost]
    public ActionResult Edit (Stylist stylist)
    {
      _db.Stylists.Update(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Stylist thisStylist = _db.Stylists
                              .Include(stylist => stylist.Clients)
                              .FirstOrDefault(stylist => stylist.StylistId == id);
      return View(thisStylist);
    }

    public ActionResult Delete (int id)
    {
      Stylist thisStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
      return View(thisStylist);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Stylist thisStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
      _db.Stylists.Remove(thisStylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // Thinking through logic of Find method -- just a GET method, doesn't need HttpPost -- DevTools says it's a Post
    public ActionResult Find (string stylistName)
    {
      return View();
    }

    [HttpPost, ActionName("Find")] // including HttpGet doesn't change DevTools's message
    public ActionResult FindStylist(string stylistName)
      {
      Stylist thisStylist = _db.Stylists
                              .Include(stylist => stylist.Clients) //don't think this is needed
                              .FirstOrDefault(stylist => stylist.StylistName == stylistName);
                              // .Where(stylist => stylist.StylistName.Contains(stylistName));
      return View(thisStylist); //should show Details page for thisStylist -- need to add in StylistId?
      // return View(thisStylist.StylistDetails);
    }

    // SELECT * FROM stylists WHERE StylistName -- documentation says you can use .Contains, but what variable can you use as a placeholder? If use full name, ignore placeholder issue. Currently not responding/showing stylist details page.
  }
}