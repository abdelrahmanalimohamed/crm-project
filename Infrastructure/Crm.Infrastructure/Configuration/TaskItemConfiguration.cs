namespace Crm.Infrastructure.Configuration;
internal sealed class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
	public void Configure(EntityTypeBuilder<TaskItem> builder)
	{
		builder.HasKey(k => k.Id);

		builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.Property(x => x.Description)
			.IsRequired(true)
			.HasMaxLength(100);

		builder.Property(x => x.DueDate)
			.IsRequired(true);

		builder.Property(x => x.IsCompleted)
			  .HasDefaultValue(false);

		builder.HasOne(x => x.User)
			.WithMany(y => y.Tasks)
			.HasForeignKey(x => x.UserId);

		builder.HasOne(x => x.Deal)
			.WithMany(y => y.Tasks)
			.HasForeignKey(x => x.DealId);

		builder.HasOne(x => x.Company)
			.WithMany(y => y.Tasks)
			.HasForeignKey(x => x.CompanyId);

		builder.HasOne(x => x.Contact)
			.WithMany(y => y.Tasks)
			.HasForeignKey(x => x.ContactId);

		builder.HasOne(x => x.Lead)
			.WithMany(y => y.Tasks)
			.HasForeignKey(x => x.LeadId);
	}
}
