namespace Crm.Infrastructure.Configuration;
internal sealed class NoteConfiguration : IEntityTypeConfiguration<Note>
{
	public void Configure(EntityTypeBuilder<Note> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.CreatedAt)
					.HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.Property(x => x.Content)
			.IsRequired(true)
			.HasMaxLength(100);

		builder.HasOne(x => x.User)
			.WithMany(y => y.Notes)
			.HasForeignKey(x => x.UserId);

		builder.HasOne(x => x.Deal)
			.WithMany(y => y.Notes)
			.HasForeignKey(x => x.DealId);

		builder.HasOne(x => x.Company)
			.WithMany(y => y.Notes)
			.HasForeignKey(x => x.CompanyId);

		builder.HasOne(x => x.Contact)
			.WithMany(y => y.Notes)
			.HasForeignKey(x => x.ContactId);
	}
}