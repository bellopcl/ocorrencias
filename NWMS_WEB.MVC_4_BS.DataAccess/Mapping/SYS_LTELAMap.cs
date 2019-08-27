using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_LTELAMap : EntityTypeConfiguration<Model.SYS_LTELA>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_LTELA, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_LTELAMap()
        {
            // Primary Key
            this.HasKey(t => t.CODLTELA);

            // Properties
            this.Property(t => t.CODLTELA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOMECOMPONENTE)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.NOMEFORM)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PROPCOMPONENTE)
                .IsRequired();

            this.Property(t => t.CATEGORIA_FT)
                .HasMaxLength(100);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_LTELA", connectionString);
            this.Property(t => t.CODLTELA).HasColumnName("CODLTELA");
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.NOMECOMPONENTE).HasColumnName("NOMECOMPONENTE");
            this.Property(t => t.NOMEFORM).HasColumnName("NOMEFORM");
            this.Property(t => t.PROPCOMPONENTE).HasColumnName("PROPCOMPONENTE");
            this.Property(t => t.CATEGORIA_FT).HasColumnName("CATEGORIA_FT");

            // Relationships
            this.HasOptional(t => t.SYS_USUARIO)
                .WithMany(t => t.SYS_LTELA)
                .HasForeignKey(d => d.CODUSU);

        }
    }
}
