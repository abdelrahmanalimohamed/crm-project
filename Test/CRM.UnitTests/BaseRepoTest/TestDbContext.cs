﻿using Microsoft.EntityFrameworkCore;

namespace CRM.UnitTests.BaseRepoTest;
internal class TestDbContext : DbContext
{
	public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
	{
	}
	public DbSet<TestEntity> TestEntities { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<TestEntity>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
			entity.Property(e => e.Description).HasMaxLength(500);
		});

		base.OnModelCreating(modelBuilder);
	}
}