using ebeertijaBackend.DatabaseContext;
using ebeertijaBackend.DTOs;
using ebeertijaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password, string CreatorUserName);
        void Update(User user, string password = null, string previousPassword = null);
        void Delete(int id);
        void UpdateGeneral(UserDto user);
    }

    public class UserService : IUserService
    {
        private BeertijaContext _context;

        public static string PEPPER = "123333osfdgsdfijgskjdg";
        public UserService(BeertijaContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Where(u => u.IsActive);
        }

        public User GetById(int id)
        {
            return _context.Users.Where(u => u.IsActive && u.Id == id).FirstOrDefault();
        }

        public User Create(User user, string password, string CreatorUserName)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ApplicationException("Lozinka je obvezan podatak.");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new ApplicationException("Korisničko ime \"" + user.Username + "\" je zauzeto.");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges(CreatorUserName);

            return user;
        }


        public void Update(User userParam, string password = null, string previousPassword = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new ApplicationException("Korisnik nije pronađen.");

            if (!VerifyPasswordHash(previousPassword, userParam.PasswordHash, userParam.PasswordSalt))
                throw new ApplicationException("Unesena je pogrešna trenutna lozinka. Pokušajte ponovo.");



            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges("userUpdate");
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.IsActive = false;
                _context.SaveChanges("userUpdate");
            }
        }

        // private helper methods

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("Lozinka je obvezan podatak.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Lozinka ne može biti prazno polje", "lozinka");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + PEPPER));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + PEPPER));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public void UpdateGeneral(UserDto korisnik)
        {
            var user = _context.Users.Where(u => u.Id == korisnik.Id).FirstOrDefault();

            if (user == null)
                throw new ApplicationException("Korisnik nije pronađen.");

            if (korisnik.Username != user.Username)
            {
                bool usernameExists = _context.Users.Where(u => u.Username == korisnik.Username).Any();


                if (usernameExists)
                {
                    throw new ApplicationException("Korisničko ime " + korisnik.Username + " je zauzeto.");
                }
            }

            // ažuriranje korisničkih podataka
            user.Username = korisnik.Username;
            user.FullName = korisnik.FullName;
            user.OIB = korisnik.OIB;
            user.Email = korisnik.Email;
            user.Vrsta = korisnik.Vrsta;
            _context.Users.Update(user);
            _context.SaveChanges("userUpdate");
        }
    }
}
