using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflow.Models
{
    [Table("Answers")]
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        [Required]
        public string Answer_Body { get; set; }
        public bool Best_Answer { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }


        public ApplicationUser User { get; set; }
    }
}
