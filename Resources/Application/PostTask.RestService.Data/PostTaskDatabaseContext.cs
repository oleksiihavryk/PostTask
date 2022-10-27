using Microsoft.EntityFrameworkCore;
using PostTask.RestService.Data.EntityConfigurations;
using PostTask.RestService.Domain;
using AppTask = PostTask.RestService.Domain.Task;

namespace PostTask.RestService.Data;
/// <summary>
///     Application database context
/// </summary>
public class PostTaskDatabaseContext : DbContext
{
    /// <summary>
    ///     Tasks database set
    /// </summary>
    public DbSet<AppTask> Tasks { get; set; } = null!;
    /// <summary>
    ///     States database set
    /// </summary>
    public DbSet<State> States { get; set; } = null!;
    /// <summary>
    ///     Database set of only user states
    /// </summary>
    public DbSet<UserState> UserStates { get; set; } = null!;
    /// <summary>
    ///     Task group database set
    /// </summary>
    public DbSet<TaskGroup> TaskGroups { get; set; } = null!;
    /// <summary>
    ///     Groups database set
    /// </summary>
    public DbSet<GroupFolder> GroupFolders { get; set; } = null!;

    /// <summary>
    ///     Create database context configured by options
    /// </summary>
    /// <param name="options">
    ///     Database configuration options
    /// </param>
    public PostTaskDatabaseContext(DbContextOptions<PostTaskDatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GroupFolderConfiguration());
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new TaskItemConfiguration());
        modelBuilder.ApplyConfiguration(new StateConfiguration());
        modelBuilder.ApplyConfiguration(new TaskGroupConfiguration());
    }
}