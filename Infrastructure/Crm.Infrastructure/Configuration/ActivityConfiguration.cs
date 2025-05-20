namespace Crm.Infrastructure.Configuration;
internal sealed class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
	public void Configure(EntityTypeBuilder<Activity> builder)
	{
		builder.HasKey(k => k.Id);

		builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.Property(x => x.Description)
			.IsRequired(true)
			.HasMaxLength(100);

		builder.Property(a => a.Type)
				.IsRequired();

		builder.Property(a => a.ActivityDate)
			   .IsRequired();

		builder.HasOne(a => a.Contact)
			.WithMany(y => y.Activities)
			.HasForeignKey(a => a.ContactId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(a => a.Company)
			.WithMany(y => y.Activities)
			.HasForeignKey(a => a.CompanyId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
