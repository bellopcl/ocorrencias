using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_USRFORMMap : EntityTypeConfiguration<Model.SYS_USRFORM>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_USRFORM, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_USRFORMMap()
        {
            // Primary Key
            this.HasKey(t => t.CODFORM);

            // Properties
            this.Property(t => t.CODFORM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FORMPADRAO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.NOME)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.TIPOFORM)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TITULO)
                .IsRequired()
                .HasMaxLength(300);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_USRFORM", connectionString);
            this.Property(t => t.CODFORM).HasColumnName("CODFORM");
            this.Property(t => t.CODUSRTABLE).HasColumnName("CODUSRTABLE");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.FORMPADRAO).HasColumnName("FORMPADRAO");
            this.Property(t => t.ICONE).HasColumnName("ICONE");
            this.Property(t => t.NOME).HasColumnName("NOME");
            this.Property(t => t.TIPOFORM).HasColumnName("TIPOFORM");
            this.Property(t => t.TITULO).HasColumnName("TITULO");

            // Relationships
            this.HasRequired(t => t.SYS_USRTABLE)
                .WithMany(t => t.SYS_USRFORM)
                .HasForeignKey(d => d.CODUSRTABLE);

        }
    }
}
