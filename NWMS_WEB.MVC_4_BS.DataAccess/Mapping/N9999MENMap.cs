using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N9999MENMap : EntityTypeConfiguration<N9999MEN>
    {
        /// <summary>
        /// Classe de mapeamento da classe N9999MEN, utilizada para difinir padrões de BD;
        /// </summary>
        public N9999MENMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODMEN, t.CODSIS });

            // Properties
            this.Property(t => t.CODMEN)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESMEN)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODSIS)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ORDMEN)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ENDPAG)
                .IsRequired()
                .HasMaxLength(50);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N9999MEN", connectionString);
            this.Property(t => t.CODMEN).HasColumnName("CODMEN");
            this.Property(t => t.DESMEN).HasColumnName("DESMEN");
            this.Property(t => t.CODSIS).HasColumnName("CODSIS");
            this.Property(t => t.MENPAI).HasColumnName("MENPAI");
            this.Property(t => t.ORDMEN).HasColumnName("ORDMEN");
            this.Property(t => t.ENDPAG).HasColumnName("ENDPAG");

            // Relationships
            this.HasRequired(t => t.N9999SIS)
                .WithMany(t => t.N9999MEN)
                .HasForeignKey(d => d.CODSIS);

        }
    }
}
