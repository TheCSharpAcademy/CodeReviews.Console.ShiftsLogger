using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShiftsLogger.DAL.Entities;

namespace ShiftsLogger.DAL.Configuration;
public class ShiftConfiguration : IEntityTypeConfiguration<ShiftEntity>
{
	public void Configure(EntityTypeBuilder<ShiftEntity> builder)
	{
		builder.ToTable("Shift");

		builder.Property(s => s.Id)
				.ValueGeneratedNever();
	}
}
