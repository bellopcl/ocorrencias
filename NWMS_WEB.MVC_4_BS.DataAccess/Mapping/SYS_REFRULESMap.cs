using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_REFRULESMap : EntityTypeConfiguration<Model.SYS_REFRULES>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_REFRULES, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_REFRULESMap()
        {
            // Primary Key
            this.HasKey(t => t.NOME);

            // Properties
            this.Property(t => t.NOME)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.VERSAO)
                .IsRequired()
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_REFRULES", connectionString);
            this.Property(t => t.NOME).HasColumnName("NOME");
            this.Property(t => t.VERSAO).HasColumnName("VERSAO");
            this.Property(t => t.CONTEUDO).HasColumnName("CONTEUDO");
        }
    }
}
