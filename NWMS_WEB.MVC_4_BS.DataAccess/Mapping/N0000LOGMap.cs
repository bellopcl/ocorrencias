using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0000LOGMap : EntityTypeConfiguration<Model.N0000LOG>
    {
        /// <summary>
        /// Classe de mapeamento da tabela N0000LOG, utilizada para definir padrões de criação de tabela;
        /// </summary>
        public N0000LOGMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODAGE, t.SEQAGE });

            // Properties
            this.Property(t => t.CODAGE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQAGE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESLOG)
                .IsRequired()
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0000LOG", connectionString);
            this.Property(t => t.CODAGE).HasColumnName("CODAGE");
            this.Property(t => t.SEQAGE).HasColumnName("SEQAGE");
            this.Property(t => t.DESLOG).HasColumnName("DESLOG");
            this.Property(t => t.DATGER).HasColumnName("DATGER");

            // Relationships
            this.HasRequired(t => t.N0000AGE)
                .WithMany(t => t.N0000LOG)
                .HasForeignKey(d => d.CODAGE);

        }
    }
}
