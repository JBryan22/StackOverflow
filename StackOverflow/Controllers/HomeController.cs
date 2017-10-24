using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflow.Models;

namespace StackOverflow.Controllers
{
    public class HomeController : Controller

    {
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;

		public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
		{
			_userManager = userManager;
			_db = db;
		}

		public IActionResult Index()
        {
            
            return View(_db.Questions.ToList());
        }

        public async Task<IActionResult> Detail(int id)
        {
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			var thisQuestion = _db.Questions.Include(m => m.Answers)
				  .FirstOrDefault(m => m.QuestionId == id);

            if (currentUser != null)
            {
                RedirectToAction("Details", "Questions", thisQuestion);
            }

            return View(thisQuestion);
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
