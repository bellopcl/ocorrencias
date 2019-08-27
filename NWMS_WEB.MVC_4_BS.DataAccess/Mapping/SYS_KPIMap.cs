using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_KPIMap : EntityTypeConfiguration<Model.SYS_KPI>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_KPI, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_KPIMap()
        {
            // Primary Key
            this.HasKey(t => t.CODKPI);

            // Properties
            this.Property(t => t.CODKPI)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOMEFORM)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.DESCRICAO)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TIPO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.USAFILTRO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_KPI", connectionString);
            this.Property(t => t.CODKPI).HasColumnName("CODKPI");
            this.Property(t => t.NOMEFORM).HasColumnName("NOMEFORM");
            this.Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
            this.Property(t => t.TIPO).HasColumnName("TIPO");
            this.Property(t => t.USAFILTRO).HasColumnName("USAFILTRO");
            this.Property(t => t.PARAMETROS).HasColumnName("PARAMETROS");
            this.Property(t => t.CODREL).HasColumnName("CODREL");
            this.Property(t => t.DATALT).HasColumnName("DATALT");

            // Relationships
            this.HasOptional(t => t.SYS_REPORTS)
                .WithMany(t => t.SYS_KPI)
                .HasForeignKey(d => d.CODREL);

        }
    }
}
