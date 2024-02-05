using Common.Domain.Auditing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.EntityFrameworkCore
{
	public static class CommonEFExtensions
    {
        public static void ConfigureModels<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
        {
            if (typeof(TEntity).IsAssignableTo(typeof(IAudtiableEntity)))
            {
                builder.Property(nameof(IAudtiableEntity.CreatedById))
                   .IsRequired(false)
                   .HasColumnName(nameof(IAudtiableEntity.CreatedById));

                builder.Property(nameof(IAudtiableEntity.UpdatedById))
                  .IsRequired(false)
                  .HasColumnName(nameof(IAudtiableEntity.UpdatedById));

                builder.Property(nameof(IAudtiableEntity.CreatedTime))
                    .IsRequired()
                    .HasColumnName(nameof(IAudtiableEntity.CreatedTime));

                builder.Property(nameof(IAudtiableEntity.UpdatedTime))
                   .IsRequired()
                   .HasColumnName(nameof(IAudtiableEntity.UpdatedTime));
            }

            if (typeof(TEntity).IsAssignableTo(typeof(ISoftDelete)))
            {
                builder.Property(nameof(ISoftDelete.IsDeleted))
                    .IsRequired()
                    .HasColumnName(nameof(ISoftDelete.IsDeleted));
            }
           
        }
    }
}
