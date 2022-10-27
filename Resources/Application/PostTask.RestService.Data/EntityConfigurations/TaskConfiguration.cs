using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostTask.RestService.Domain;

namespace PostTask.RestService.Data.EntityConfigurations;
/// <summary>
///     Task entity configuration class
/// </summary>
public class TaskConfiguration : IEntityTypeConfiguration<Domain.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Task> builder)
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