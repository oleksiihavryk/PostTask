using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostTask.RestService.Domain;

namespace PostTask.RestService.Data.EntityConfigurations;
/// <summary>
///     Task group entity configuration class
/// </summary>
public class TaskGroupConfiguration : IEntityTypeConfiguration<TaskGroup>
{
    public void Configure(EntityTypeBuilder<TaskGroup> builder)
    {
        builder.ToTable("TaskGroup");
        builder.HasMany(g => g.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}