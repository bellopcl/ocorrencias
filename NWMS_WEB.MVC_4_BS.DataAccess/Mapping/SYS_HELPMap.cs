using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_HELPMap : EntityTypeConfiguration<Model.SYS_HELP>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_HELP, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_HELPMap()
        {
            // Primary Key
            this.HasKey(t => t.CODHELP);

            // Properties
            this.Property(t => t.ALTERADO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CODHELP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CONTEXTO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DESCRICAO)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PALAVRAS_CHAVE)
                .HasMaxLength(500);

            this.Property(t => t.TOPICO_PAI)
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_HELP", connectionString);
            this.Property(t => t.ALTERADO).HasColumnName("ALTERADO");
            this.Property(t => t.CODHELP).HasColumnName("CODHELP");
            this.Property(t => t.CONTEXTO).HasColumnName("CONTEXTO");
            this.Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
            this.Property(t => t.PALAVRAS_CHAVE).HasColumnName("PALAVRAS_CHAVE");
            this.Property(t => t.TOPICO_IDX).HasColumnName("TOPICO_IDX");
            this.Property(t => t.TOPICO_PAI).HasColumnName("TOPICO_PAI");
        }
    }
}
