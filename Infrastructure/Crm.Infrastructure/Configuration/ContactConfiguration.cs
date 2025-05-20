namespace Crm.Infrastructure.Configuration
{
	internal sealed class ContactConfiguration : IEntityTypeConfiguration<Contact>
	{
		public void Configure(EntityTypeBuilder<Contact> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(x => x.CreatedAt)
					.HasDefaultValueSql("CURRENT_TIMESTAMP");

			builder.Property(c => c.FirstName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(c => c.LastName)
				.IsRequired()
				.HasMaxLength(100);

			builder.OwnsOne(c => c.Email, email =>
			{
				email.Property(e => e.Value)
					.HasColumnName("Email")
					.IsRequired()
					.HasMaxLength(150);
			});

			// Value Object: Phone
			builder.OwnsOne(c => c.Phone, phone =>
			{
				phone.Property(p => p.Value)
					.HasColumnName("PhoneNumber")
					.IsRequired()
					.HasMaxLength(20);
			});

			builder.HasOne(c => c.Company)
					.WithMany(c => c.Contacts)
					.HasForeignKey(c => c.CompanyId)
					.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
