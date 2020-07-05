using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLibrary.Models.Mapping
{
    public class OmissionsMap : EntityTypeConfiguration<Omissions>
    {
        public OmissionsMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("OMISSIONS");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.Ranking).HasColumnName("RANKING");
            this.Property(t => t.One).HasColumnName("ONE");
            this.Property(t => t.Two).HasColumnName("TWO");
            this.Property(t => t.Three).HasColumnName("THREE");
            this.Property(t => t.Four).HasColumnName("FOUR");
            this.Property(t => t.Five).HasColumnName("FIVE");
            this.Property(t => t.Six).HasColumnName("SIX");
            this.Property(t => t.Seven).HasColumnName("SEVEN");
            this.Property(t => t.Eight).HasColumnName("EIGHT");
            this.Property(t => t.Nine).HasColumnName("NINE");
            this.Property(t => t.Ten).HasColumnName("TEN");
        }
    }
}
