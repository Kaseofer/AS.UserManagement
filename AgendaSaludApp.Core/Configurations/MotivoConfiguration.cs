using AgendaSaludApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaSaludApp.Core.Configurations
{
    public class MotivoConfiguration : IEntityTypeConfiguration<MotivoCita>
    {
        public void Configure(EntityTypeBuilder<MotivoCita> builder)
        {
            builder.ToTable("motivo", "agendasalud");

            builder.HasKey(m => m.Id);


        }
    }
}