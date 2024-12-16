using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan? Duration => (EndTime - StartTime);
    }
}
