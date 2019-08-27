using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0203APRMap : EntityTypeConfiguration<Model.N0203APR>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0203APR, utilizada para difinir padrões de BD;
        /// </summary>
        public N0203APRMap()
        {
            // Primary Key
            this.HasKey(t => new { t.NUMREG, t.SEQAPR });

            // Properties
            this.Property(t => t.NUMREG)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQAPR)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESAPR)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.VIAAPR)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITAPR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.NIVAPR)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0203APR", connectionString);
            this.Property(t => t.NUMREG).HasColumnName("NUMREG");
            this.Property(t => t.SEQAPR).HasColumnName("SEQAPR");
            this.Property(t => t.DESAPR).HasColumnName("DESAPR");
            this.Property(t => t.VIAAPR).HasColumnName("VIAAPR");
            this.Property(t => t.SITAPR).HasColumnName("SITAPR");
            this.Property(t => t.DATGER).HasColumnName("DATGER");
            this.Property(t => t.USUGER).HasColumnName("USUGER");
            this.Property(t => t.DATAPR).HasColumnName("DATAPR");
            this.Property(t => t.USUAPR).HasColumnName("USUAPR");
            this.Property(t => t.NIVAPR).HasColumnName("NIVAPR");
            this.Property(t => t.USUCAN).HasColumnName("USUCAN");
            this.Property(t => t.DATCAN).HasColumnName("DATCAN");

            // Relationships
            this.HasRequired(t => t.N0203REG)
                .WithMany(t => t.N0203APR)
                .HasForeignKey(d => d.NUMREG);

        }
    }
}
