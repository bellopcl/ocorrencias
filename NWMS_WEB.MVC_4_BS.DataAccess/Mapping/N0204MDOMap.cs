using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0204MDOMap : EntityTypeConfiguration<N0204MDO>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0204MDO, utilizada para difinir padrões de BD;
        /// </summary>
        public N0204MDOMap()
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
            this.ToTable("N0204MDO", connectionString);
            this.Property(t => t.CODMDV).HasColumnName("CODMDV");
            this.Property(t => t.CODORI).HasColumnName("CODORI");
            this.Property(t => t.IDROW).HasColumnName("IDROW");
            this.Property(t => t.SITREL).HasColumnName("SITREL");

            // Relationships
            this.HasRequired(t => t.N0204MDV)
                .WithMany(t => t.N0204MDO)
                .HasForeignKey(d => d.CODMDV);
            this.HasRequired(t => t.N0204ORI)
                .WithMany(t => t.N0204MDO)
                .HasForeignKey(d => d.CODORI);

        }
    }
}
