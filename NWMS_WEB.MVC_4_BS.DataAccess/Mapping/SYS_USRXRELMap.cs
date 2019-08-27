using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_USRXRELMap : EntityTypeConfiguration<Model.SYS_USRXREL>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_USRXREL, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_USRXRELMap()
        {
            // Primary Key
            this.HasKey(t => t.CODUSU);

            // Properties
            this.Property(t => t.CODUSU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_USRXREL", connectionString);
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.CTRLACESSOREL).HasColumnName("CTRLACESSOREL");

            // Relationships
            this.HasRequired(t => t.SYS_USUARIO)
                .WithOptional(t => t.SYS_USRXREL);

        }
    }
}
