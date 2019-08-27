using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N9999UXMMap : EntityTypeConfiguration<Model.N9999UXM>
    {
        /// <summary>
        /// Classe de mapeamento da classe N9999UXM, utilizada para difinir padrões de BD;
        /// </summary>
        public N9999UXMMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODUSU, t.CODSIS, t.CODMEN });

            // Properties
            this.Property(t => t.CODUSU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODSIS)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODMEN)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PERMEN)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INSMEN)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ALTMEN)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EXCMEN)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N9999UXM", connectionString);
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.CODSIS).HasColumnName("CODSIS");
            this.Property(t => t.CODMEN).HasColumnName("CODMEN");
            this.Property(t => t.PERMEN).HasColumnName("PERMEN");
            this.Property(t => t.INSMEN).HasColumnName("INSMEN");
            this.Property(t => t.ALTMEN).HasColumnName("ALTMEN");
            this.Property(t => t.EXCMEN).HasColumnName("EXCMEN");

            // Relationships
            this.HasRequired(t => t.N9999MEN)
                .WithMany(t => t.N9999UXM)
                .HasForeignKey(d => new { d.CODSIS, d.CODMEN });
            this.HasRequired(t => t.SYS_USUARIO)
                .WithMany(t => t.N9999UXM)
                .HasForeignKey(d => d.CODUSU);

        }
    }
}
