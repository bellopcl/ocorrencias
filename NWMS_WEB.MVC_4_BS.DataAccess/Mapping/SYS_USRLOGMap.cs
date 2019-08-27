using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_USRLOGMap : EntityTypeConfiguration<Model.SYS_USRLOG>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_USRLOG, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_USRLOGMap()
        {
            // Primary Key
            this.HasKey(t => t.CODUSU);

            // Properties
            this.Property(t => t.CODUSU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_USRLOG", connectionString);
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");

            // Relationships
            this.HasRequired(t => t.SYS_USUARIO)
                .WithOptional(t => t.SYS_USRLOG);

        }
    }
}
