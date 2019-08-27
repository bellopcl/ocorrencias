using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_CONCORRENCIAMap : EntityTypeConfiguration<Model.SYS_CONCORRENCIA>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_CONCORRENCIA, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_CONCORRENCIAMap()
        {
            // Primary Key
            this.HasKey(t => t.CODCONC);

            // Properties
            this.Property(t => t.CODCONC)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CHAVEREG)
                .HasMaxLength(1000);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_CONCORRENCIA", connectionString);
            this.Property(t => t.CODCONC).HasColumnName("CODCONC");
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.CHAVEREG).HasColumnName("CHAVEREG");

            // Relationships
            this.HasRequired(t => t.SYS_USUARIO)
                .WithMany(t => t.SYS_CONCORRENCIA)
                .HasForeignKey(d => d.CODUSU);

        }
    }
}
