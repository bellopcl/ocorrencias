using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    public class N0203UAPMap : EntityTypeConfiguration<N0203UAP>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0203UAP, utilizada para difinir padrões de BD;
        /// </summary>
        public N0203UAPMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODUSU, t.CODORI, t.CODATD });

            // Properties
            this.Property(t => t.CODUSU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODORI)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODATD)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N0203UAP", connectionString);
            this.Property(t => t.CODORI).HasColumnName("CODORI");
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.CODATD).HasColumnName("CODATD");

            // Relationships
            this.HasRequired(t => t.N0204ORI)
                .WithMany(t => t.N0203UAP)
                .HasForeignKey(d => d.CODORI);
            this.HasRequired(t => t.N0204ATD)
                .WithMany(t => t.N0203UAP)
                .HasForeignKey(d => d.CODATD);
            this.HasRequired(t => t.N9999USU)
                .WithMany(t => t.N0203UAP)
                .HasForeignKey(d => d.CODUSU);
        }
    }
}
