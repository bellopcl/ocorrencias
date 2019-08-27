using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0104TPEMap : EntityTypeConfiguration<Model.N0104TPE>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0104TPE, utilizada para difinir padrões de BD;
        /// </summary>
        public N0104TPEMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODTPE });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODTPE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESTPE)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MOVUNI)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITTPE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0104TPE", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODTPE).HasColumnName("CODTPE");
            this.Property(t => t.DESTPE).HasColumnName("DESTPE");
            this.Property(t => t.IMGTPE).HasColumnName("IMGTPE");
            this.Property(t => t.MOVUNI).HasColumnName("MOVUNI");
            this.Property(t => t.SITTPE).HasColumnName("SITTPE");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0104TPE)
                .HasForeignKey(d => d.CODEMP);

        }
    }
}
