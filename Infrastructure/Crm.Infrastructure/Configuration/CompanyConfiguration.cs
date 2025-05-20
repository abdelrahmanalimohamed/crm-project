namespace Crm.Infrastructure.Configuration
{
	internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
	{
		public void Configure(EntityTypeBuilder<Company> builder)
		{
			builder.Property(x => x.Name)
				.IsRequired(true)
				.HasMaxLength(100);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			builder.Property(x => x.Industry)
				.IsRequired(true)
				.HasMaxLength(100);

			builder.OwnsOne(c => c.Email, email =>
			{
				email.Property(e => e.Value)
					.HasColumnName("Email")
					.IsRequired()
					.HasMaxLength(150);
			});

			builder.OwnsOne(c => c.PhoneNumber, phone =>
			{
				phone.Property(p => p.Value)
					.HasColumnName("PhoneNumber")
					.IsRequired()
					.HasMaxLength(20);
			});

			builder.OwnsOne(c => c.Address, address =>
			{
				address.Property(a => a.Street)
					.HasColumnName("Street")
					.HasMaxLength(150)
					.IsRequired();

				address.Property(a => a.City)
					.HasColumnName("City")
					.HasMaxLength(100)
					.IsRequired();

				address.Property(a => a.Country)
					.HasColumnName("Country")
					.HasMaxLength(100)
					.IsRequired();
			});
		}
	}
}
