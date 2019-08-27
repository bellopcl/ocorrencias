using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_REGINIMap : EntityTypeConfiguration<Model.SYS_REGINI>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_REGINI, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_REGINIMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODUSU, t.TOPICO, t.VARIAVEL });

            // Properties
            this.Property(t => t.CODUSU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CONTEUDO_BLN)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CONTEUDO_STR)
                .HasMaxLength(250);

            this.Property(t => t.TIPO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TOPICO)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.VARIAVEL)
                .IsRequired()
                .HasMaxLength(100);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_REGINI", connectionString);
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.CONTEUDO_BLN).HasColumnName("CONTEUDO_BLN");
            this.Property(t => t.CONTEUDO_DTE).HasColumnName("CONTEUDO_DTE");
            this.Property(t => t.CONTEUDO_FLT).HasColumnName("CONTEUDO_FLT");
            this.Property(t => t.CONTEUDO_STR).HasColumnName("CONTEUDO_STR");
            this.Property(t => t.CONTEUDO_TXT).HasColumnName("CONTEUDO_TXT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.TIPO).HasColumnName("TIPO");
            this.Property(t => t.TOPICO).HasColumnName("TOPICO");
            this.Property(t => t.VARIAVEL).HasColumnName("VARIAVEL");
        }
    }
}
