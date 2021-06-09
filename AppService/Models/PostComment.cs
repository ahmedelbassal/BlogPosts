using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
//using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppService.Models
{
    public class PostComment
    {


        public int Id { get; set; }

        [MaxLength(300, ErrorMessage = "maximum characters should be lower than 300 character")]
        public String Content { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime ModifiedAt { get; set; } 

        [ForeignKey("BlogPost")]
        public int PostId { get; set; }

        //[JsonIgnore]
        public virtual BlogPost Post { get; set; }
    }
}
