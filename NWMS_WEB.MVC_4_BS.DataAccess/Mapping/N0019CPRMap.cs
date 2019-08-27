using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0019CPRMap : EntityTypeConfiguration<Model.N0019CPR>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0019CPR, utilizada para difinir padrões de BD;
        /// </summary>
        public N0019CPRMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODPRO, t.CODCTE, t.SEQCCP });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODPRO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODCTE)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.SEQCCP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESLIV)
                .HasMaxLength(250);

            this.Property(t => t.OBSLIV)
                .HasMaxLength(300);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0019CPR", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODCTE).HasColumnName("CODCTE");
            this.Property(t => t.SEQCCP).HasColumnName("SEQCCP");
            this.Property(t => t.DESLIV).HasColumnName("DESLIV");
            this.Property(t => t.OBSLIV).HasColumnName("OBSLIV");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");

            // Relationships
            this.HasRequired(t => t.N0006PRO)
                .WithMany(t => t.N0019CPR)
                .HasForeignKey(d => new { d.CODEMP, d.CODPRO });
            this.HasRequired(t => t.N0019CTE)
                .WithMany(t => t.N0019CPR)
                .HasForeignKey(d => new { d.CODEMP, d.CODCTE });

        }
    }
}
