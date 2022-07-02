using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class Video
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(50)]
        public string VideoUrl { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int View { get; set; }
        public int Timestamp { get; set; }
        public int Channel_id { get; set; }
    }
}
