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
    public class QuestionsController : Controller
    {
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;

		public QuestionsController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
		{
			_userManager = userManager;
			_db = db;
		}

		// GET: /<controller>/
		public async Task<IActionResult> Index()
		{
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			return View(_db.Questions.Where(x => x.User.Id == currentUser.Id));
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Question question)
		{
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			question.User = currentUser;
			_db.Questions.Add(question);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

        public IActionResult Detail(int id)
        {
           var thisQuestion =  _db.Questions.Include(m => m.Answers)
               .FirstOrDefault(m => m.QuestionId == id);

            return View(thisQuestion);
        }

        public IActionResult Edit(int id)
        {
			var thisQuestion = _db.Questions.FirstOrDefault(m => m.QuestionId == id);

			return View(thisQuestion);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Question question)
        {
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			question.User = currentUser;
			_db.Entry(question).State = EntityState.Modified;
			_db.SaveChanges();
			return RedirectToAction("Index");
            
        }

        public IActionResult Delete(int id)
		{
			var thisQuestion = _db.Questions.FirstOrDefault(m => m.QuestionId == id);
			return View(thisQuestion);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteQuestion(int id)
        {
			var thisQuestion = _db.Questions.FirstOrDefault(m => m.QuestionId == id);
            _db.Remove(thisQuestion);
            _db.SaveChanges();
			return RedirectToAction("Index");
        }


	
    }
}
