using System.Data.Entity.ModelConfiguration;
namespace JobLibrary.Models.Mapping
{
    public class ContinuityNumberMap : EntityTypeConfiguration<ContinuityNumber>
    {
        public ContinuityNumberMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("CONTINUITY_NUMBER");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RankingId).HasColumnName("RANKINGID");
            this.Property(t => t.Number).HasColumnName("NUMBER");
        }
    }
}