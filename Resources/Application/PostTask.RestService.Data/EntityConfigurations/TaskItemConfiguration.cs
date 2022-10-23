using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostTask.RestService.Domain;
using AppTask = PostTask.RestService.Domain.Task;

namespace PostTask.RestService.Data.EntityConfigurations;
/// <summary>
///     Task item entity configuration class
/// </summary>
public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("TaskItem");
        builder.HasOne<AppTask>()
            .WithMany(t => t.Items)
            .IsRequired();
    }
}