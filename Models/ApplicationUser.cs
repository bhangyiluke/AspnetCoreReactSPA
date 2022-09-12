namespace AspnetCoreReactSPA.Models;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser() : base() { }
    public virtual byte[] Avatar { get; set; } = null!;
    public DateTime? CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string UpdatedBy { get; set; }
}
