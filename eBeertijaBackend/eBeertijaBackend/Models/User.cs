using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.Models
{
    public class User: BaseBoObject
    {
        [MaxLength(30), Column(TypeName = "varchar")]
        public string Username { get; set; }


        [MaxLength(20), Column(TypeName = "varchar")]
        public string FullName { get; set; }

        [MaxLength(250), Column(TypeName = "varchar")]
        public string Email { get; set; }

        public VrstaUsera Vrsta { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        public string OIB { get; set; }
    }
    public enum VrstaUsera
    {
        ADMINISTRATOR = 1, VODITELJ = 2, KONOBAR = 3
    }
}
