using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0102ESEMap : EntityTypeConfiguration<Model.N0102ESE>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0102ESE, utilizada para difinir padrões de BD;
        /// </summary>
        public N0102ESEMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODESE });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODESE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESESE)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SITESE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0102ESE", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODESE).HasColumnName("CODESE");
            this.Property(t => t.DESESE).HasColumnName("DESESE");
            this.Property(t => t.IMGESE).HasColumnName("IMGESE");
            this.Property(t => t.SITESE).HasColumnName("SITESE");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0102ESE)
                .HasForeignKey(d => d.CODEMP);

        }
    }
}
