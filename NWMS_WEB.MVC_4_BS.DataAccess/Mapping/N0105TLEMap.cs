using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0105TLEMap : EntityTypeConfiguration<Model.N0105TLE>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0105TLE, utilizada para difinir padrões de BD;
        /// </summary>
        public N0105TLEMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODTLE });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODTLE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESTLE)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ENDFLE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITTPE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0105TLE", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODTLE).HasColumnName("CODTLE");
            this.Property(t => t.DESTLE).HasColumnName("DESTLE");
            this.Property(t => t.ENDFLE).HasColumnName("ENDFLE");
            this.Property(t => t.SITTPE).HasColumnName("SITTPE");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0105TLE)
                .HasForeignKey(d => d.CODEMP);

        }
    }
}
