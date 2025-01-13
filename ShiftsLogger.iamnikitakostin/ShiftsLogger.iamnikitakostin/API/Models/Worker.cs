using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Worker
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Department { get; set; }
}
