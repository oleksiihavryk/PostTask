using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostTask.RestService.Domain;
using AppTask = PostTask.RestService.Domain.Task;

namespace PostTask.RestService.Data.EntityConfigurations;
/// <summary>
///     Task entity configuration class
/// </summary>
public class TaskConfiguration : IEntityTypeConfiguration<AppTask>
{
    public void Configure(EntityTypeBuilder<AppTask> builder)
    {
        builder.ToTable("Task");
        builder
            .HasOne<TaskGroup>()
            .WithMany();
        builder.HasMany(g => g.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}