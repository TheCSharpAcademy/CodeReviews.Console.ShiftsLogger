using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.API.Data
{
  public class Worker
  {
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public required string FirstName { get; set; }

    [StringLength(50)]
    public required string LastName { get; set; }

    public IEnumerable<Shift> Shifts { get; set; } = new List<Shift>();
  }
}
