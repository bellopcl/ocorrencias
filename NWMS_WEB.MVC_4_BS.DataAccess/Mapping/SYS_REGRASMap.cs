using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_REGRASMap : EntityTypeConfiguration<Model.SYS_REGRAS>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_REGRAS, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_REGRASMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EVENTO, t.FORM, t.OBJETO });

            // Properties
            this.Property(t => t.ASSINATURA)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.COMPILADO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EVENTO)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.EXECUCAO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FORM)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.OBJETO)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.PUBLICADO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TIPEVENTO)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.TIPOBJETO)
                .IsRequired()
                .HasMaxLength(250);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_REGRAS", connectionString);
            this.Property(t => t.ASSINATURA).HasColumnName("ASSINATURA");
            this.Property(t => t.COMPILADO).HasColumnName("COMPILADO");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.EVENTO).HasColumnName("EVENTO");
            this.Property(t => t.EXECUCAO).HasColumnName("EXECUCAO");
            this.Property(t => t.FORM).HasColumnName("FORM");
            this.Property(t => t.OBJETO).HasColumnName("OBJETO");
            this.Property(t => t.PUBLICADO).HasColumnName("PUBLICADO");
            this.Property(t => t.SOURCE).HasColumnName("SOURCE");
            this.Property(t => t.TIPEVENTO).HasColumnName("TIPEVENTO");
            this.Property(t => t.TIPOBJETO).HasColumnName("TIPOBJETO");
        }
    }
}
