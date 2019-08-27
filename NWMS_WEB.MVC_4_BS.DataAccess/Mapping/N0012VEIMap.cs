using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0012VEIMap : EntityTypeConfiguration<Model.N0012VEI>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0012VEI, utilizada para difinir padrões de BD;
        /// </summary>
        public N0012VEIMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODTRA, t.PLAVEI });

            // Properties
            this.Property(t => t.CODTRA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PLAVEI)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SITVEI)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0012VEI", connectionString);
            this.Property(t => t.CODTRA).HasColumnName("CODTRA");
            this.Property(t => t.PLAVEI).HasColumnName("PLAVEI");
            this.Property(t => t.ANOVEI).HasColumnName("ANOVEI");
            this.Property(t => t.PESMAX).HasColumnName("PESMAX");
            this.Property(t => t.VOLMAX).HasColumnName("VOLMAX");
            this.Property(t => t.SITVEI).HasColumnName("SITVEI");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");

            // Relationships
            this.HasRequired(t => t.N0012TRA)
                .WithMany(t => t.N0012VEI)
                .HasForeignKey(d => d.CODTRA);

        }
    }
}
