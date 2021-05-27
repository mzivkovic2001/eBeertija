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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User gabrijela = new User { Id = 1, Email = "gjerkovic@veleri.hr", IsActive = true, Username = "gabrijela", FullName = "Gabrijela Jerković", Vrsta = VrstaUsera.ADMINISTRATOR };
            User eni = new User { Id = 2, Email = "erabar@veleri.hr", IsActive = true, Username = "eni", FullName = "Eni Rabar", Vrsta = VrstaUsera.ADMINISTRATOR };
            User marin = new User { Id = 3, Email = "mzivkovic@veleri.hr", IsActive = true, Username = "marin", FullName = "Marin Živković", Vrsta = VrstaUsera.ADMINISTRATOR };

            byte[] passwordHash, passwordSalt;

            UserService.CreatePasswordHash("gabrijela", out passwordHash, out passwordSalt);


            gabrijela.PasswordHash = passwordHash;
            gabrijela.PasswordSalt = passwordSalt;
            
            UserService.CreatePasswordHash("enieni", out passwordHash, out passwordSalt);
            eni.PasswordHash = passwordHash;
            eni.PasswordSalt = passwordSalt;

            UserService.CreatePasswordHash("marin", out passwordHash, out passwordSalt);
            marin.PasswordHash = passwordHash;
            marin.PasswordSalt = passwordSalt;

            User[] users = { gabrijela, marin, eni };

            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
