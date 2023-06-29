using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class WorkItemProcessorDbContext : DbContext
{
    private IConfiguration _configuration;

    public WorkItemProcessorDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("WarehouseDatabase"));
    }

    public DbSet<WorkItemRevision> WorkItemRevisions { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
}