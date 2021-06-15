using ebeertijaBackend.Models;
using ebeertijaBackend.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.DatabaseContext
{
    public class BeertijaContext: DbContext
    {
        public BeertijaContext(DbContextOptions options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            throw new NotImplementedException("Call save change with user overload ");
            //return SaveChanges("Automatic call");
        }

        public int SaveChanges(string userName)
        {
            int returnValue = 0;
            // this.ChangeTracker.DetectChanges();
            try
            {
                var addedAuditedEntitiesd = ChangeTracker.Entries().Where(p => p.State == EntityState.Added)
                   .Select(p => p.Entity).ToList();
                var addedAuditedEntities = ChangeTracker.Entries()
                   .Where(p => p.State == EntityState.Added)
                   .Select(p => p.Entity);


                var modifiedAuditedEntities = ChangeTracker.Entries()
                  .Where(p => p.State == EntityState.Modified)
                  .Select(p => p.Entity);


                foreach (var added in addedAuditedEntities)
                    UpdateObject((BaseBoObject)added, userName, true);

                foreach (var modified in modifiedAuditedEntities)
                {
                    UpdateObject((BaseBoObject)modified, userName, false);

                }

                returnValue = base.SaveChanges();


            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException)
                {
                    //exception that is thrown when deleting with cascading constraint
                }
                returnValue = -1;
            }

            return returnValue;

        }

        private void UpdateObject(object boObject, string user, bool isInsert)
        {
            BaseBoObject boBaseObject = boObject as BaseBoObject;
            if (boBaseObject == null)
                return;
            var a = boBaseObject.GetType();
            boBaseObject.UpdatedAt = DateTime.Now;
            boBaseObject.UpdatedBy = user;

            if (isInsert)
            {
                boBaseObject.CreatedAt = DateTime.Now;
                boBaseObject.CreatedBy = user;
                boBaseObject.IsActive = true;
            }
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Cjenik> Cjenici { get; set; }
        public DbSet<StavkaCjenika> StavkeCjenika { get; set; }

        public DbSet<Stol> Stolovi { get; set; }
        public DbSet<Narudzba> Narudzbe { get; set; }
        public DbSet<StavkaNarudzbe> StavkeNarudzbe { get; set; }

        public DbSet<Racun> Racuni { get; set; }
        public DbSet<StavkaRacuna> StavkeRacuna { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User gabrijela = new User { Id = 1, Email = "admin@veleri.hr", IsActive = true, Username = "admin", FullName = "Admin Admin", Vrsta = VrstaUsera.ADMINISTRATOR };
            User eni = new User { Id = 2, Email = "voditelj@veleri.hr", IsActive = true, Username = "voditelj", FullName = "Voditelj Voditelj", Vrsta = VrstaUsera.VODITELJ };
            User marin = new User { Id = 3, Email = "konobar@veleri.hr", IsActive = true, Username = "konobar", FullName = "Konobar Konobar", Vrsta = VrstaUsera.KONOBAR };

            byte[] passwordHash, passwordSalt;

            UserService.CreatePasswordHash("admin", out passwordHash, out passwordSalt);


            gabrijela.PasswordHash = passwordHash;
            gabrijela.PasswordSalt = passwordSalt;
            
            UserService.CreatePasswordHash("voditelj", out passwordHash, out passwordSalt);
            eni.PasswordHash = passwordHash;
            eni.PasswordSalt = passwordSalt;

            UserService.CreatePasswordHash("konobar", out passwordHash, out passwordSalt);
            marin.PasswordHash = passwordHash;
            marin.PasswordSalt = passwordSalt;

            User[] users = { gabrijela, marin, eni };

            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
