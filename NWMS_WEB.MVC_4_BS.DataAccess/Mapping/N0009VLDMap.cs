using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0009VLDMap : EntityTypeConfiguration<Model.N0009VLD>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0009VLD, utilizada para difinir padrões de BD;
        /// </summary>
        public N0009VLDMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODTPR, t.DATINI });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODTPR)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.SITVLD)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0009VLD", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODTPR).HasColumnName("CODTPR");
            this.Property(t => t.DATINI).HasColumnName("DATINI");
            this.Property(t => t.DATFIM).HasColumnName("DATFIM");
            this.Property(t => t.SITVLD).HasColumnName("SITVLD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");

            // Relationships
            this.HasRequired(t => t.N0009TAB)
                .WithMany(t => t.N0009VLD)
                .HasForeignKey(d => new { d.CODEMP, d.CODTPR });

        }
    }
}
