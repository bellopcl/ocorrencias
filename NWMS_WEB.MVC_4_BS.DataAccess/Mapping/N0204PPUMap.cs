using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0204PPUMap : EntityTypeConfiguration<Model.Models.N0204PPU>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0204PPU, utilizada para difinir padrões de BD;
        /// </summary>
        public N0204PPUMap()
        {
            // Primary Key
            this.HasKey(t => t.IDROW);

            // Properties
            this.Property(t => t.IDROW)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N0204PPU", connectionString);
            this.Property(t => t.QTDDEV).HasColumnName("QTDDEV");
            this.Property(t => t.QTDTRC).HasColumnName("QTDTRC");
            this.Property(t => t.IDROW).HasColumnName("IDROW");

            // Relationships
            this.HasRequired(t => t.N9999USU)
                .WithMany(t => t.N0204PPU)
                .HasForeignKey(d => d.CODUSU);

        }
    }
}
