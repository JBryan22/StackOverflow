using System.ComponentModel.DataAnnotations;
using StackOverflow.Models;

namespace StackOverflow.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool hasLoggedIn { get; set; }


    }
}