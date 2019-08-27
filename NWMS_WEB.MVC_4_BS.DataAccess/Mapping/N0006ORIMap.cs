using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0006ORIMap : EntityTypeConfiguration<Model.N0006ORI>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0006ORI, utilizada para difinir padrões de BD;
        /// </summary>
        public N0006ORIMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODORI });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODORI)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DESORI)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.INDQTD)
                .IsFixedLength()
                .HasMaxLength(1);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
            // Table & Column Mappings
            this.ToTable("N0006ORI", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODORI).HasColumnName("CODORI");
            this.Property(t => t.DESORI).HasColumnName("DESORI");
            this.Property(t => t.INDQTD).HasColumnName("INDQTD");
        }
    }
}
