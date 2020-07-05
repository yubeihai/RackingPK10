using System.Data.Entity.ModelConfiguration;
namespace JobLibrary.Models.Mapping
{
    public class LotteryDataMap : EntityTypeConfiguration<LotteryData>
    {
        public LotteryDataMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("LOTTERY_DATA");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.IssueId).HasColumnName("ISSUEID");
            this.Property(t => t.Champion).HasColumnName("CHAMPION");
            this.Property(t => t.RunnerUp).HasColumnName("RUNNERUP");
            this.Property(t => t.Third).HasColumnName("THIRD");
            this.Property(t => t.Fourth).HasColumnName("FOURTH");
            this.Property(t => t.Fifth).HasColumnName("FIFTH");
            this.Property(t => t.Sixth).HasColumnName("SIXTH");
            this.Property(t => t.Seventh).HasColumnName("SEVENTH");
            this.Property(t => t.Eighth).HasColumnName("EIGHTH");
            this.Property(t => t.Ninth).HasColumnName("NINTH");
            this.Property(t => t.Tenth).HasColumnName("TENTH");
            this.Property(t => t.Date).HasColumnName("DATE");
            this.Property(t => t.Time).HasColumnName("TIME");
        }
    }
}