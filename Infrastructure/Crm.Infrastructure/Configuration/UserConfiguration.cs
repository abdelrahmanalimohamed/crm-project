namespace Crm.Infrastructure.Configuration;
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(k => k.Id);

		builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.Property(x => x.Name)
				.HasMaxLength(20);

		builder.Property(x => x.Role)
			.IsRequired();

		builder.OwnsOne(u => u.Email, email =>
		{
			email.Property(e => e.Value)
				.HasColumnName("Email")
				.IsRequired()
				.HasMaxLength(100);
		});

		builder.HasMany(x => x.Activities)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(x => x.Notes)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(x => x.Tasks)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(x => x.Contacts)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(x => x.Leads)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Restrict);

	}
}
