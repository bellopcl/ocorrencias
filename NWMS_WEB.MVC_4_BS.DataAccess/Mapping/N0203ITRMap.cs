using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0203ITRMap : EntityTypeConfiguration<Model.N0203ITR>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0203ITR, utilizada para difinir padrões de BD;
        /// </summary>
        public N0203ITRMap()
        {
            // Primary Key
            this.HasKey(t => new { t.NUMREG, t.SEQITR });

            // Properties
            this.Property(t => t.NUMREG)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODTPR)
                .HasMaxLength(50);

            this.Property(t => t.CODPRO)
                .HasMaxLength(50);

            this.Property(t => t.CODDER)
                .HasMaxLength(50);

            this.Property(t => t.SEQITR)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0203ITR", connectionString);
            this.Property(t => t.NUMREG).HasColumnName("NUMREG");
            this.Property(t => t.CODTPR).HasColumnName("CODTPR");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.QTDPRO).HasColumnName("QTDPRO");
            this.Property(t => t.VLRUNI).HasColumnName("VLRUNI");
            this.Property(t => t.VLRLIQ).HasColumnName("VLRLIQ");
            this.Property(t => t.DATGER).HasColumnName("DATGER");
            this.Property(t => t.USUGER).HasColumnName("USUGER");
            this.Property(t => t.SEQITR).HasColumnName("SEQITR");
            this.Property(t => t.PEROFE).HasColumnName("PEROFE");
            this.Property(t => t.PERIPI).HasColumnName("PERIPI");
            this.Property(t => t.VLRIPI).HasColumnName("VLRIPI");

            // Relationships
            this.HasRequired(t => t.N0203REG)
                .WithMany(t => t.N0203ITR)
                .HasForeignKey(d => d.NUMREG);

        }
    }
}
