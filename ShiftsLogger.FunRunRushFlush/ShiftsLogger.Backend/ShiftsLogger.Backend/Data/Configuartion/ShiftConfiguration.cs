using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Domain;

internal sealed class ShiftConfiguration : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .IsUnicode(false)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.EmployeeName).HasMaxLength(256).IsRequired();
        builder.Property(x => x.ShiftDescription).HasMaxLength(512).IsRequired();
        builder.Property(x => x.ShiftStart).IsRequired();
        builder.Property(x => x.ShiftEnd).IsRequired();

    }
}
