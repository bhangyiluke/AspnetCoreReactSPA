namespace AspnetCoreReactSPA.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call the parent model constructor
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>(b =>
        {
            b.Property(c => c.ConcurrencyStamp).IsConcurrencyToken();
            b.ToTable("Users");
        });
        modelBuilder.Entity<IdentityRole>(b =>
        {
            // Each User can have many entries in the UserRole join table
            b.ToTable("Roles");
        });
        modelBuilder.Entity<IdentityUserRole<string>>(b =>
        {
            b.ToTable("UserRoles");
        });
        modelBuilder.Entity<IdentityUserClaim<string>>(b =>
        {
            b.ToTable("UserClaims");
        });

        modelBuilder.Entity<IdentityUserLogin<string>>(b =>
        {
            b.ToTable("UserLogins");
        });

        modelBuilder.Entity<IdentityUserToken<string>>(b =>
        {
            b.ToTable("UserTokens");
        });
        modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
        {
            b.ToTable("RoleClaims");
        });
        modelBuilder.Seed(this);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e =>
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            try{
                if (entityEntry.State == EntityState.Added && entityEntry.GetType().GetProperty("CreatedOn") != null)
                {
                    entityEntry.Property("CreatedOn").CurrentValue = DateTime.Now;
                }
                else if (entityEntry.State == EntityState.Modified && entityEntry.GetType().GetProperty("UpdatedOn") != null)
                {
                    entityEntry.Property("UpdatedOn").CurrentValue = DateTime.Now;
                }
            }catch(Exception ex){
                Console.WriteLine(ex.StackTrace);
            }
        }

        return await base.SaveChangesAsync();
    }
}
