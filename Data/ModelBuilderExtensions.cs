namespace AspnetCoreReactSPA.Data;
public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder builder, ApplicationDbContext _context)
    {
        builder.Entity<ApplicationUser>().HasData(new[]{
                    new {
                        Id = "1",
                        UserName = "admin",
                        NormalizedUserName = "admin".ToUpper(),
                        Email = "admin@example.com",
                        NormalizedEmail = "admin@example.com".ToUpper(),
                        EmailConfirmed = true,
                        PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Admin123@net7.0"),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        CreatedOn = DateTime.Now,
                        CreatedBy = "admin",
                        AccessFailedCount = 0,
                        LockoutEnabled = false,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false
                    }
                }
        );

        builder.Entity<IdentityRole>().HasData(new[] {
                new {Id = "1", Name = "Administrator",NormalizedName="Administrator".ToUpper(), ConcurrencyStamp=Guid.NewGuid().ToString()},
                new {Id = "2", Name = "User", NormalizedName="User".ToUpper(), ConcurrencyStamp=Guid.NewGuid().ToString()}
            });
        builder.Entity<IdentityUserRole<string>>().HasData(new[] {
                new { UserId = "1", RoleId = "1" }
            });
        }
    public static void PerformMigrations(IServiceProvider serviceProvider)
    {
        serviceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
    }
}
