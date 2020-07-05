using System.Data.Entity.ModelConfiguration;
namespace JobLibrary.Models.Mapping
{
    public class StatisticsHalfYearMap : EntityTypeConfiguration<StatisticsHalfYear>
    {
        public StatisticsHalfYearMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("STATISTICS_HALFYEAR");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.NGId).HasColumnName("NG_ID");
            this.Property(t => t.RankingId).HasColumnName("RANKINGID");
            this.Property(t => t.Frequency).HasColumnName("FREQUENCY");
            this.Property(t => t.Number).HasColumnName("NUMBER");
        }
    }
}