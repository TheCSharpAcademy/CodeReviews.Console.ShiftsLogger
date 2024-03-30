using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShiftLogger.Models;

namespace ShiftLogger.Data
{
    public class ShiftLoggerContext : DbContext
    {
        public ShiftLoggerContext (DbContextOptions<ShiftLoggerContext> options)
            : base(options)
        {
        }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
