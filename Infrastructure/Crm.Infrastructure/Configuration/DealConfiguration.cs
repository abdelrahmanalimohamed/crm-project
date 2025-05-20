namespace Crm.Infrastructure.Configuration;
internal sealed class DealConfiguration : IEntityTypeConfiguration<Deal>
{
	public void Configure(EntityTypeBuilder<Deal> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.CreatedAt)
		.HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.Property(d => d.Title)
		   .IsRequired()
		   .HasMaxLength(200);

		builder.Property(d => d.Amount)
			.HasColumnType("decimal(18,2)");

		builder.Property(x => x.DealStages)
			.IsRequired(true);

		builder.HasOne(d => d.Contact)
				.WithMany(c => c.Deals)
				.HasForeignKey(d => d.ContactId)
				.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(d => d.Company)
				.WithMany(c => c.Deals)
				.HasForeignKey(d => d.CompanyId)
				.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(d => d.Tasks)
			.WithOne(t => t.Deal)
			.HasForeignKey(t => t.DealId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(d => d.Notes)
			.WithOne(n => n.Deal)
			.HasForeignKey(n => n.DealId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(d => d.Activities)
			.WithOne(a => a.Deal)
			.HasForeignKey(a => a.DealId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}