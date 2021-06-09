using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppService.Models
{
    public class BlogPost
    {

        public int Id { get; set; }

        [StringLength(60,ErrorMessage ="title characters length should be from 3 to 60 characters",MinimumLength =3)]
        public String Title { get; set; }

        [MaxLength(300, ErrorMessage ="maximum characters should be lower than 300 character")]
        public String Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

       //[JsonIgnore]
        public virtual ICollection<PostComment> Comments { get; set; }
    }
}
