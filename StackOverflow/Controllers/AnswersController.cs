using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflow.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StackOverflow.Controllers
{
	[Authorize]
	public class AnswersController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;

		public AnswersController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
		{
			_userManager = userManager;
			_db = db;
		}

		// GET: /<controller>/
		public async Task<IActionResult> Index()
		{
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			return View(_db.Answers.Where(x => x.User.Id == currentUser.Id));
		}

		public IActionResult Create(int id)
		{
            ViewBag.Question = _db.Questions.FirstOrDefault(m => m.QuestionId == id);
            ViewBag.Number = 1;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Answer answer)
		{
            
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			answer.User = currentUser;
			_db.Answers.Add(answer);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}


		public IActionResult Edit(int id)
		{
			var thisAnswer = _db.Answers.FirstOrDefault(m => m.AnswerId == id);

			return View(thisAnswer);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Answer answer)
		{
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			answer.User = currentUser;
			_db.Entry(answer).State = EntityState.Modified;
			_db.SaveChanges();
			return RedirectToAction("Index");

		}

		public IActionResult Delete(int id)
		{
			var thisAnswer = _db.Answers.FirstOrDefault(m => m.AnswerId == id);
			return View(thisAnswer);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteAnswer(int id)
		{
			var thisAnswer = _db.Answers.FirstOrDefault(m => m.AnswerId == id);
			_db.Remove(thisAnswer);
			_db.SaveChanges();
			return View(thisAnswer);
		}
    }
}
