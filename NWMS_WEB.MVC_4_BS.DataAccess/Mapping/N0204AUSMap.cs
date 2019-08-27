using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0204AUSMap : EntityTypeConfiguration<N0204AUS>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0204AUS, utilizada para difinir padrões de BD;
        /// </summary>
        public N0204AUSMap()
        {
            // Primary Key
            this.HasKey(t => t.IDROW);

            // Properties
            this.Property(t => t.IDROW)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SITREL)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N0204AUS", connectionString);
            this.Property(t => t.CODATD).HasColumnName("CODATD");
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.IDROW).HasColumnName("IDROW");
            this.Property(t => t.SITREL).HasColumnName("SITREL");

            // Relationships
            this.HasRequired(t => t.N0204ATD)
                .WithMany(t => t.N0204AUS)
                .HasForeignKey(d => d.CODATD);
            this.HasRequired(t => t.N9999USU)
                .WithMany(t => t.N0204AUS)
                .HasForeignKey(d => d.CODUSU);

        }
    }
}
