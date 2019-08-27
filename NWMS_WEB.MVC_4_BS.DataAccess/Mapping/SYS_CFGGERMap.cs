using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_CFGGERMap : EntityTypeConfiguration<Model.SYS_CFGGER>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_CFGGER, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_CFGGERMap()
        {
            // Primary Key
            this.HasKey(t => t.VERSAO);

            // Properties
            this.Property(t => t.LOCKDBNAME)
                .HasMaxLength(100);

            this.Property(t => t.LOCKDBACTION)
                .HasMaxLength(250);

            this.Property(t => t.VERSAO)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.ULTIMOACESSO)
                .HasMaxLength(255);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_CFGGER", connectionString);
            this.Property(t => t.CHAVE).HasColumnName("CHAVE");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.BRADLL).HasColumnName("BRADLL");
            this.Property(t => t.BRDLL).HasColumnName("BRDLL");
            this.Property(t => t.BRCLASSESDLL).HasColumnName("BRCLASSESDLL");
            this.Property(t => t.LOCKDBNAME).HasColumnName("LOCKDBNAME");
            this.Property(t => t.LOCKDBACTION).HasColumnName("LOCKDBACTION");
            this.Property(t => t.DATADLL).HasColumnName("DATADLL");
            this.Property(t => t.VERSAO).HasColumnName("VERSAO");
            this.Property(t => t.IMGPRINC).HasColumnName("IMGPRINC");
            this.Property(t => t.ULTIMOACESSO).HasColumnName("ULTIMOACESSO");
        }
    }
}
