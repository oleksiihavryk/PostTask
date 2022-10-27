using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostTask.RestService.Domain;

namespace PostTask.RestService.Data.EntityConfigurations;
/// <summary>
///     State entity configuration class
/// </summary>
public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("TaskState");
        builder.HasMany<Domain.Task>()
            .WithOne(t => t.State)
            .OnDelete(DeleteBehavior.SetNull);
    }
}