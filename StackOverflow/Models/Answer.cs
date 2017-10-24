using System;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        [Required]
        public string Answer_Body { get; set; }
        public bool Best_Answer { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
