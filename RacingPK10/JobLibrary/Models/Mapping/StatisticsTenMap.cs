using System.Data.Entity.ModelConfiguration;
namespace JobLibrary.Models.Mapping
{
    public class StatisticsTenMap : EntityTypeConfiguration<StatisticsTen>
    {
        public StatisticsTenMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("STATISTICS_TEN");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RankingId).HasColumnName("RANKINGID");
            this.Property(t => t.Frequency).HasColumnName("FREQUENCY");
            this.Property(t => t.Number).HasColumnName("NUMBER");

        }
    }
}