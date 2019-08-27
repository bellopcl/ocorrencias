using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_CAIXAMap : EntityTypeConfiguration<Model.SYS_CAIXA>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_CAIXA, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_CAIXAMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODCAI, t.CODUSU });

            // Properties
            this.Property(t => t.CODCAI)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODUSU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESCRICAO)
                .IsRequired()
                .HasMaxLength(100);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_CAIXA", connectionString);
            this.Property(t => t.CODCAI).HasColumnName("CODCAI");
            this.Property(t => t.CODCAI_1).HasColumnName("CODCAI_1");
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");

            // Relationships
            this.HasRequired(t => t.SYS_USUARIO)
                .WithMany(t => t.SYS_CAIXA)
                .HasForeignKey(d => d.CODUSU);

        }
    }
}
