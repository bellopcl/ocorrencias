using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0008CNVMap : EntityTypeConfiguration<Model.N0008CNV>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0008CNV, utilizada para difinir padrões de BD;
        /// </summary>
        public N0008CNVMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UNIMED, t.UNIME2 });

            // Properties
            this.Property(t => t.UNIMED)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UNIME2)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TIPCNV)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0008CNV", connectionString);
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");
            this.Property(t => t.UNIME2).HasColumnName("UNIME2");
            this.Property(t => t.TIPCNV).HasColumnName("TIPCNV");
            this.Property(t => t.VLRCNV).HasColumnName("VLRCNV");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");

            // Relationships
            this.HasRequired(t => t.N0007UNI)
                .WithMany(t => t.N0008CNV)
                .HasForeignKey(d => d.UNIMED);
            this.HasRequired(t => t.N0007UNI1)
                .WithMany(t => t.N0008CNV1)
                .HasForeignKey(d => d.UNIME2);

        }
    }
}
