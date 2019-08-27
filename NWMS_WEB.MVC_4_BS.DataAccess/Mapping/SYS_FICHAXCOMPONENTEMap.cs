using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_FICHAXCOMPONENTEMap : EntityTypeConfiguration<Model.SYS_FICHAXCOMPONENTE>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_FICHAXCOMPONENTE, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_FICHAXCOMPONENTEMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODCATEGORIA, t.CODCOMPONENTE });

            // Properties
            this.Property(t => t.CODCATEGORIA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODCOMPONENTE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_FICHAXCOMPONENTE", connectionString);
            this.Property(t => t.CODCATEGORIA).HasColumnName("CODCATEGORIA");
            this.Property(t => t.CODCOMPONENTE).HasColumnName("CODCOMPONENTE");
        }
    }
}
