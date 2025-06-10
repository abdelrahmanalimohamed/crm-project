namespace Crm.Infrastructure.AppDbContext;
public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}
	 public DbSet<Activity> Activities { get; set; }
	 public DbSet<Company> Companies { get; set; }
  	 public DbSet<Deal> Deals { get; set; }
	 public DbSet<Lead> Leads { get; set; }
	 public DbSet<TaskItem> TaskItems { get; set; }
	 public DbSet<Note> Notes { get; set; }
	 public DbSet<Contact> Contacts { get; set; }
	 public DbSet<User> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Ignore DomainEventsBase class
		modelBuilder.Ignore<DomainEventsBase>();
		
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}
}