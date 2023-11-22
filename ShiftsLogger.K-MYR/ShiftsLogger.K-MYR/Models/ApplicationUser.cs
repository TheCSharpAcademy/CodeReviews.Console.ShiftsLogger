using Microsoft.AspNetCore.Identity;

namespace ShiftsLogger.K_MYR;

public class ApplicationUser : IdentityUser
{
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}
