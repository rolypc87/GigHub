﻿using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{

	public class GigsController : Controller
    {
	    private readonly ApplicationDbContext _context;

	    public GigsController()
	    {
		    _context = new ApplicationDbContext();
	    }

		[Authorize]
		public ActionResult Create()
        {
	        var viewModel = new GigFormViewModel()
	        {
		        Genres = _context.Genres.ToList()
			};
            return View(viewModel);
        }

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(GigFormViewModel viewModel)
		{
			// removed because it was set as Foraing Key properties in the Gig model
				//var artist = _context.Users.Single(u => u.Id == artistId);
				//var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);
				//var gig = new Gig()
				//{
				//	Artist = artist,
				//	DateTime = DateTime.Parse($"{viewModel.Date} {viewModel.Time}"),
				//	Genre = genre,
				//	Venue = viewModel.Venue
				//};


			//Refactored Code
			if (!ModelState.IsValid)
			{
				viewModel.Genres = _context.Genres.ToList();
				return View("Create",viewModel);
			}

			var gig = new Gig()
			{
				ArtistId = User.Identity.GetUserId(),
				DateTime = viewModel.GetDateTime(),
				GenreId = viewModel.Genre,
				Venue = viewModel.Venue
			};
			_context.Gigs.Add(gig);
			_context.SaveChanges();

			return RedirectToAction("Index","Home");
		}
	}
}