using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflow.Models
{   
    [Table("Profiles")]
    public class Profile
    {
        public int ProfileId { get; set; }
        public byte[] profilePicture { get; set; }
        public string location { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<Question> Questions { get; set; }


        public Profile()
        {
            this.profilePicture = null;
            this.location = "Enter your location here";

        }

      
    }
}
