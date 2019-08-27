using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0203UOFMap : EntityTypeConfiguration<N0203UOF>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0203UOF, utilizada para difinir padrões de BD;
        /// </summary>
        public N0203UOFMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODUSU, t.CODOPE, t.CODATD });

            // Properties
            this.Property(t => t.CODUSU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODOPE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODATD)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N0203UOF", connectionString);
            this.Property(t => t.CODOPE).HasColumnName("CODOPE");
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.CODATD).HasColumnName("CODATD");

            // Relationships
            this.HasRequired(t => t.N0203OPE)
                .WithMany(t => t.N0203UOF)
                .HasForeignKey(d => d.CODOPE);
            this.HasRequired(t => t.N0204ATD)
                .WithMany(t => t.N0203UOF)
                .HasForeignKey(d => d.CODATD);
            this.HasRequired(t => t.N9999USU)
                .WithMany(t => t.N0203UOF)
                .HasForeignKey(d => d.CODUSU);
        }
    }
}
