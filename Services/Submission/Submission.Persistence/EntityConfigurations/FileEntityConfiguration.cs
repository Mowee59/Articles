using Blocks.Core.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Submission.Persistence.EntityConfigurations;

/// <summary>
/// EF Core configuration for the owned <c>File</c> value object, including size,
/// server id, and nested name/extension value objects.
/// </summary>
internal class FileEntityConfiguration
{
    /// <summary>
    /// Configures the file value object properties such as original name, server id,
    /// size, and the nested <c>Extension</c> and <c>Name</c> value objects.
    /// </summary>
    /// <param name="builder">The complex property builder for the file type.</param>
    public void Configure(ComplexPropertyBuilder<Domain.ValueObjects.File> builder)
    {
        builder.Property(e => e.OriginalName)
            .HasMaxLength(MaxLength.C256)
            .HasComment("Original full name of the file, with the extension");

        builder.Property(e => e.FileServerId)
            .HasMaxLength(MaxLength.C64);
        
        builder.Property(e => e.Size)
            .HasComment("Size of the file in kilobytes");

        builder.ComplexProperty(
          o => o.Extension, complexBuilder =>
          {
              complexBuilder.Property(n => n.Value)
                  .HasColumnName($"{builder.Metadata.ClrType.Name}_{complexBuilder.Metadata.PropertyInfo!.Name}")
                  .HasMaxLength(MaxLength.C8);
                  
          });

        builder.ComplexProperty(
            o => o.Name, complexBuilder =>
            {
                complexBuilder.Property(n => n.Value)
                    .HasColumnName($"{builder.Metadata.ClrType.Name}_{complexBuilder.Metadata.PropertyInfo!.Name}")
                    .HasMaxLength(MaxLength.C64)
                    .HasComment("Final name of the file after renaming");
            });
        
    }
}

