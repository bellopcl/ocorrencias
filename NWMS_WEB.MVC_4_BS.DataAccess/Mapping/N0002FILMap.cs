using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0002FILMap : EntityTypeConfiguration<Model.N0002FIL>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0002FIL, utilizada para difinir padrões de BD;
        /// </summary>
        public N0002FILMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOMFIL)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SITFIL)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ABRUSU)
                .HasMaxLength(500);

            this.Property(t => t.INDOBS)
                .IsFixedLength()
                .HasMaxLength(1);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N0002FIL", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.NOMFIL).HasColumnName("NOMFIL");
            this.Property(t => t.SITFIL).HasColumnName("SITFIL");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.DIADEV).HasColumnName("DIADEV");
            this.Property(t => t.ABRUSU).HasColumnName("ABRUSU");
            this.Property(t => t.INDOBS).HasColumnName("INDOBS");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0002FIL)
                .HasForeignKey(d => d.CODEMP);

        }
    }
}
