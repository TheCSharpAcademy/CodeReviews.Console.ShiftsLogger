using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.Domain;

public class Entity
{
    [Key]
    public int Id { get; set; }

}