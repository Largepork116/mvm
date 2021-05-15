using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Entities.Base;
using DavidMorales.Domain.Security.Authentication;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DavidMorales.Infrastructure.Context
{
    public class AppContext : IdentityDbContext<AppUser, AppRole, Int64>
    {
        public AppContext(DbContextOptions<AppContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            base.OnModelCreating(builder);

            // Identity
            builder.Entity<AppUser>(entity => { entity.ToTable("Users", "identity"); });
            builder.Entity<AppRole>(entity => { entity.ToTable("Roles", "identity"); });
            builder.Entity<IdentityUserRole<Int64>>(entity => { entity.ToTable("UsersRoles", "identity"); });
            builder.Entity<IdentityUserClaim<Int64>>(entity => { entity.ToTable("UsersClaims", "identity"); });
            builder.Entity<IdentityUserLogin<Int64>>(entity => { entity.ToTable("UsersLogins", "identity"); });
            builder.Entity<IdentityUserToken<Int64>>(entity => { entity.ToTable("UsersTokens", "identity"); });
            builder.Entity<IdentityRoleClaim<Int64>>(entity => { entity.ToTable("RolesClaims", "identity"); });
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<LogDataChange> LogsDataChanges { get; set; }

        #region Methods
        public override int SaveChanges()
        {
            AuditEntities();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AuditEntities();
            return (await base.SaveChangesAsync(true, cancellationToken));
        }

        private void AuditEntities()
        {
            // Each entity that inherits from auditable
            foreach (EntityEntry<Auditable> entry in ChangeTracker.Entries<Auditable>())
            {
                var now = DateTime.UtcNow;
                var _appIdentity = this.GetService<AppIdentity>();

                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedBy").CurrentValue = _appIdentity.Username;
                    entry.Property("CreatedAt").CurrentValue = now;
                }
                else if (entry.State == EntityState.Modified) // si la entidad fue actualizada
                {
                    entry.Property("CreatedBy").CurrentValue = entry.Property("CreatedBy").OriginalValue;
                    entry.Property("CreatedAt").CurrentValue = entry.Property("CreatedAt").OriginalValue;

                    var tableName = entry.Metadata.GetTableName(); 
                    var pk = entry.OriginalValues[entry.Metadata.FindPrimaryKey().Properties.First()] ?? 0;

                    var changes = new List<string>();

                    foreach (var item in entry.Properties.Where(x => x.IsModified))
                    {
                        var columnName = item.Metadata.Name;
                        var oldValue = item.OriginalValue == null ? "" : item.OriginalValue.ToString();
                        var newValue = item.CurrentValue == null ? "" : item.CurrentValue.ToString();

                        var change = $"{columnName} : {oldValue} => {newValue}";
                        if (oldValue != newValue)
                        {
                            changes.Add(change);
                        }
                    }

                    if(changes.Count > 0)
                    {
                        var log = new LogDataChange
                        {
                            Table = tableName,
                            Pk = int.Parse(pk.ToString()),
                            Changes = string.Join(Environment.NewLine, changes),
                            UpdatedBy = _appIdentity.Username,
                            UpdatedAt = now
                        };
                        entry.Context.Set<LogDataChange>().Add(log);
                    }
                }
            }
        }
        #endregion



    }

}
