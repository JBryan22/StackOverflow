using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StackOverflow.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content_Body { get; set; }
        public bool Is_Answered { get; set; }
        public DateTime Submission_Date { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
