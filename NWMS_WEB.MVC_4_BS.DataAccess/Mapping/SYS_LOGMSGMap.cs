using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_LOGMSGMap : EntityTypeConfiguration<Model.SYS_LOGMSG>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_LOGMSG, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_LOGMSGMap()
        {
            // Primary Key
            this.HasKey(t => t.CODMSG);

            // Properties
            this.Property(t => t.CODMSG)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MENSAGEM)
                .IsRequired();

            this.Property(t => t.BOTAO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RESPOSTA)
                .HasMaxLength(50);

            this.Property(t => t.ENTREGUE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_LOGMSG", connectionString);
            this.Property(t => t.CODMSG).HasColumnName("CODMSG");
            this.Property(t => t.MENSAGEM).HasColumnName("MENSAGEM");
            this.Property(t => t.CODUSU_REMETENTE).HasColumnName("CODUSU_REMETENTE");
            this.Property(t => t.CODUSU_DESTINATARIO).HasColumnName("CODUSU_DESTINATARIO");
            this.Property(t => t.BOTAO).HasColumnName("BOTAO");
            this.Property(t => t.RESPOSTA).HasColumnName("RESPOSTA");
            this.Property(t => t.ENTREGUE).HasColumnName("ENTREGUE");

            // Relationships
            this.HasRequired(t => t.SYS_USUARIO)
                .WithMany(t => t.SYS_LOGMSG)
                .HasForeignKey(d => d.CODUSU_DESTINATARIO);
            this.HasRequired(t => t.SYS_USUARIO1)
                .WithMany(t => t.SYS_LOGMSG1)
                .HasForeignKey(d => d.CODUSU_REMETENTE);

        }
    }
}
