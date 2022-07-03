using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CommentDto
    {
        [Required]
        public string Context { get; set; }
        [Required]
        public int Video_id { get; set; }
        public int Reply_id { get; set; }
    }
}
