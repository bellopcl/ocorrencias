using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0204AORMap : EntityTypeConfiguration<N0204AOR>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0204AOR, utilizada para difinir padrões de BD;
        /// </summary>
        public N0204AORMap()
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
            this.ToTable("N0204AOR", connectionString);
            this.Property(t => t.CODATD).HasColumnName("CODATD");
            this.Property(t => t.CODORI).HasColumnName("CODORI");
            this.Property(t => t.IDROW).HasColumnName("IDROW");
            this.Property(t => t.SITREL).HasColumnName("SITREL");

            // Relationships
            this.HasRequired(t => t.N0204ATD)
                .WithMany(t => t.N0204AOR)
                .HasForeignKey(d => d.CODATD);
            this.HasRequired(t => t.N0204ORI)
                .WithMany(t => t.N0204AOR)
                .HasForeignKey(d => d.CODORI);

        }
    }
}
