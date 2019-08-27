using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_USRONMap : EntityTypeConfiguration<Model.SYS_USRON>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_USRON, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_USRONMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODUSU, t.IP });

            // Properties
            this.Property(t => t.CODUSU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IP)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.VERSAO)
                .IsRequired()
                .HasMaxLength(16);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_USRON", connectionString);
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.DATA).HasColumnName("DATA");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.VERSAO).HasColumnName("VERSAO");

            // Relationships
            this.HasRequired(t => t.SYS_USUARIO)
                .WithMany(t => t.SYS_USRON)
                .HasForeignKey(d => d.CODUSU);

        }
    }
}
