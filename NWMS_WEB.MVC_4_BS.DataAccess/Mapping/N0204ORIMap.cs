using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0204ORIMap : EntityTypeConfiguration<N0204ORI>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0204ORI, utilizada para difinir padrões de BD;
        /// </summary>
        public N0204ORIMap()
        {
            // Primary Key
            this.HasKey(t => t.CODORI);

            // Properties
            this.Property(t => t.CODORI)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESCORI)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SITORI)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N0204ORI", connectionString);
            this.Property(t => t.CODORI).HasColumnName("CODORI");
            this.Property(t => t.DESCORI).HasColumnName("DESCORI");
            this.Property(t => t.SITORI).HasColumnName("SITORI");
            this.Property(t => t.CODORI_SAPIENS).HasColumnName("CODORI_SAPIENS");
        }
    }
}
