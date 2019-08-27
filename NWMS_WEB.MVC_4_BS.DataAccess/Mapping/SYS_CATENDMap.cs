using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_CATENDMap : EntityTypeConfiguration<Model.SYS_CATEND>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_CATEND, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_CATENDMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODUSU, t.EMAIL });

            // Properties
            this.Property(t => t.CODUSU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EMAIL)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.NOME)
                .IsRequired()
                .HasMaxLength(100);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_CATEND", connectionString);
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.EMAIL).HasColumnName("EMAIL");
            this.Property(t => t.NOME).HasColumnName("NOME");

            // Relationships
            this.HasRequired(t => t.SYS_USUARIO)
                .WithMany(t => t.SYS_CATEND)
                .HasForeignKey(d => d.CODUSU);

        }
    }
}
