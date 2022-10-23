using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostTask.RestService.Domain;

namespace PostTask.RestService.Data.EntityConfigurations;
/// <summary>
///     Group folder entity configuration class
/// </summary>
public class GroupFolderConfiguration : IEntityTypeConfiguration<GroupFolder>
{
    public void Configure(EntityTypeBuilder<GroupFolder> builder)
    {
        builder.ToTable("TaskGroupFolder");
        builder.HasMany(g => g.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}