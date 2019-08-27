using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0013MCPMap : EntityTypeConfiguration<Model.N0013MCP>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0013MCP, utilizada para difinir padrões de BD;
        /// </summary>
        public N0013MCPMap()
        {
            // Primary Key
            this.HasKey(t => t.CODMCP);

            // Properties
            this.Property(t => t.CODMCP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESMCP)
                .IsRequired()
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0013MCP", connectionString);
            this.Property(t => t.CODMCP).HasColumnName("CODMCP");
            this.Property(t => t.DESMCP).HasColumnName("DESMCP");
        }
    }
}
