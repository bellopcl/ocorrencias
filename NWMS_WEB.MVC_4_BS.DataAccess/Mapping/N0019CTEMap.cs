using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0019CTEMap : EntityTypeConfiguration<Model.N0019CTE>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0019CTE, utilizada para difinir padrões de BD;
        /// </summary>
        public N0019CTEMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODCTE });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODCTE)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.DESCTE)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TEMCCP)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TIPCCP)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0019CTE", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODCTE).HasColumnName("CODCTE");
            this.Property(t => t.DESCTE).HasColumnName("DESCTE");
            this.Property(t => t.TEMCCP).HasColumnName("TEMCCP");
            this.Property(t => t.TIPCCP).HasColumnName("TIPCCP");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0019CTE)
                .HasForeignKey(d => d.CODEMP);

        }
    }
}
