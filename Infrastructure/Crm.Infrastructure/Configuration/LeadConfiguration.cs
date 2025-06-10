namespace Crm.Infrastructure.Configuration;
internal sealed class LeadConfiguration : IEntityTypeConfiguration<Lead>
{
	public void Configure(EntityTypeBuilder<Lead> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.CreatedAt)
			   .HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.HasOne(x => x.Contact)
			.WithMany(y => y.Leads)
			.HasForeignKey(a => a.ContactId);

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