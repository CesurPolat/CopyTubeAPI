using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(500)]
        public string Context { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int Timestamp { get; set; }
        public int Video_id { get; set; }
        public int? Reply_id { get; set; }
        public int Channel_id { get; set; }
    }
}
