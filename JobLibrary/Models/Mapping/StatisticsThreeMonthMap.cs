using System.Data.Entity.ModelConfiguration;
namespace JobLibrary.Models.Mapping
{
    public class StatisticsThreeMonthMap : EntityTypeConfiguration<StatisticsThreeMonth>
    {
        public StatisticsThreeMonthMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("STATISTICS_THREEMONTH");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.NGId).HasColumnName("NG_ID");
            this.Property(t => t.RankingId).HasColumnName("RANKINGID");
            this.Property(t => t.Frequency).HasColumnName("FREQUENCY");
            this.Property(t => t.Number).HasColumnName("NUMBER");
        }
    }
}