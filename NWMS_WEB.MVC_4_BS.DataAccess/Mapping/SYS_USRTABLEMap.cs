using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_USRTABLEMap : EntityTypeConfiguration<Model.SYS_USRTABLE>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_USRTABLE, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_USRTABLEMap()
        {
            // Primary Key
            this.HasKey(t => t.CODUSRTABLE);

            // Properties
            this.Property(t => t.CODUSRTABLE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESCRICAO)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.NOME)
                .IsRequired()
                .HasMaxLength(150);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_USRTABLE", connectionString);
            this.Property(t => t.CODUSRTABLE).HasColumnName("CODUSRTABLE");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
            this.Property(t => t.NOME).HasColumnName("NOME");
        }
    }
}
