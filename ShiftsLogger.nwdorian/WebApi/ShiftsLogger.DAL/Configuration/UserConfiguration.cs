using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShiftsLogger.DAL.Entities;

namespace ShiftsLogger.DAL.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
	public void Configure(EntityTypeBuilder<UserEntity> builder)
	{
		builder.ToTable("User");

		builder.HasMany(u => u.Shifts)
				.WithMany(s => s.Users)
				.UsingEntity("UserShift", j =>
				{
					j.Property("UsersId").HasColumnName("UserId");
					j.Property("ShiftsId").HasColumnName("ShiftId");
				});

		builder.Property(u => u.Id)
				.ValueGeneratedNever();

		builder.Property(u => u.FirstName)
				.HasMaxLength(100)
				.IsRequired();

		builder.Property(u => u.LastName)
				.HasMaxLength(100)
				.IsRequired();

		builder.Property(u => u.Email)
				.HasMaxLength(100)
				.IsRequired();
	}
}
