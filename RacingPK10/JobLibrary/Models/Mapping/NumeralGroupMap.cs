using System.Data.Entity.ModelConfiguration;

namespace JobLibrary.Models.Mapping
{
    public class NumeralGroupMap : EntityTypeConfiguration<NumeralGroup>
    {
        public NumeralGroupMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("NUMETAL_GROUP");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.Group).HasColumnName("GROUP");
            this.Property(t => t.Ranking).HasColumnName("RANKING");
            this.Property(t => t.PublishTime).HasColumnName("PUBLISHTIME");
        }

    }
}