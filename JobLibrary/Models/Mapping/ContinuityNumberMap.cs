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
            this.Property(t => t.NGId).HasColumnName("NG_ID");
            this.Property(t => t.RankingId).HasColumnName("RANKINGID");
            this.Property(t => t.TenNumber).HasColumnName("TEN_NUMBER");
            this.Property(t => t.MonthNumber).HasColumnName("MONTH_NUMBER");
            this.Property(t => t.ThreeMonthNumber).HasColumnName("THREE_MONTH_NUMBER");
            this.Property(t => t.HalfYearNumber).HasColumnName("HALF_YEAR_NUMBER");
            this.Property(t => t.YearNumber).HasColumnName("YEAR_NUMBER");
        }
    }
}