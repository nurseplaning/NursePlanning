using Microsoft.EntityFrameworkCore;

namespace DomainModel.ModelBuilders
{
    public static class DirectorModelBuilder
    {
        public static void DirectorModel(this ModelBuilder builder)
        {
            builder.Entity<Director>()
                .ToTable("Directors");

            builder.Entity<Director>()
                .Property(d => d.SiretNumber)
                .IsRequired();
        }
    }
}
