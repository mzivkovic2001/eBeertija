using ebeertijaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PreviousPassword { get; set; }
        public string Password { get; set; }
        public bool IsPasswordUpdate { get; set; }
        public string OIB { get; set; }
        public string FullName { get; set; }
        public VrstaUsera Vrsta { get; set; }
        public string Email { get; set; }
    }
}
