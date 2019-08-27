using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_COMSQLMap : EntityTypeConfiguration<Model.SYS_COMSQL>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_COMSQL, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_COMSQLMap()
        {
            // Primary Key
            this.HasKey(t => t.CODCOMANDOSQL);

            // Properties
            this.Property(t => t.CODCOMANDOSQL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESCRICAO)
                .IsRequired()
                .HasMaxLength(100);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_COMSQL", connectionString);
            this.Property(t => t.CODCOMANDOSQL).HasColumnName("CODCOMANDOSQL");
            this.Property(t => t.CONSULTA).HasColumnName("CONSULTA");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
        }
    }
}
