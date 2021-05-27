using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.Models
{
    public class BaseBoObject
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [MaxLength(250), Column(TypeName = "varchar")]
        public string CreatedBy { get; set; }

        [MaxLength(250), Column(TypeName = "varchar")]
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
