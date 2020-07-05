using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLibrary.Models.Mapping
{
    public class NoteUsersMap : EntityTypeConfiguration<NoteUsers>
    {
        public NoteUsersMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("NOTEUSERS");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.UserName).HasColumnName("USERNAME");
            this.Property(t => t.PassWord).HasColumnName("PASSWORD");
            this.Property(t => t.Cookie).HasColumnName("COOKIE");
            this.Property(t => t.Oid).HasColumnName("OID");
            this.Property(t => t.PublishTime).HasColumnName("PUBLISHTIME");

        }
    }
}
