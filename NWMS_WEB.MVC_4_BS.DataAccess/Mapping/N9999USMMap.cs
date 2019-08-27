using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N9999USMMap : EntityTypeConfiguration<N9999USM>
    {
        /// <summary>
        /// Classe de mapeamento da classe N9999USM, utilizada para difinir padrões de BD;
        /// </summary>
        public N9999USMMap()
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
            this.ToTable("N9999USM", connectionString);
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.CODSIS).HasColumnName("CODSIS");
            this.Property(t => t.CODMEN).HasColumnName("CODMEN");
            this.Property(t => t.PERMEN).HasColumnName("PERMEN");
            this.Property(t => t.INSMEN).HasColumnName("INSMEN");
            this.Property(t => t.ALTMEN).HasColumnName("ALTMEN");
            this.Property(t => t.EXCMEN).HasColumnName("EXCMEN");

            // Relationships
            this.HasRequired(t => t.N9999MEN)
                .WithMany(t => t.N9999USM)
                .HasForeignKey(d => new { d.CODSIS, d.CODMEN });
            this.HasRequired(t => t.N9999USU)
                .WithMany(t => t.N9999USM)
                .HasForeignKey(d => d.CODUSU);

        }
    }
}
