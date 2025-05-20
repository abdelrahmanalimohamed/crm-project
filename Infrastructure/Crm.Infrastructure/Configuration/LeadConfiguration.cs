namespace Crm.Infrastructure.Configuration;
internal sealed class LeadConfiguration : IEntityTypeConfiguration<Lead>
{
	public void Configure(EntityTypeBuilder<Lead> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.CreatedAt)
			   .HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.Property(x => x.FirstName)
			.IsRequired(true)
			.HasMaxLength(20);

		builder.Property(x => x.LastName)
			.IsRequired(true)
			.HasMaxLength(20);

		builder.OwnsOne(c => c.Email, email =>
		{
			email.Property(e => e.Value)
				.HasColumnName("Email")
				.IsRequired()
				.HasMaxLength(150);
		});

		builder.OwnsOne(c => c.Phone, phone =>
		{
			phone.Property(p => p.Value)
				.HasColumnName("Phone")
				.IsRequired()
				.HasMaxLength(20);
		});

		builder.HasOne(x => x.User)
			.WithMany(y => y.Leads)
			.HasForeignKey(a => a.UserId);

		builder.HasMany(x => x.Tasks)
			.WithOne(t => t.Lead)
			.HasForeignKey(t => t.LeadId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(x => x.Notes)
			.WithOne(t => t.Lead)
			.HasForeignKey(t => t.LeadId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(x => x.Activities)
			.WithOne(t => t.Lead)
			.HasForeignKey(t => t.LeadId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}