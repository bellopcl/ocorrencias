using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0009TABMap : EntityTypeConfiguration<Model.N0009TAB>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0009TAB, utilizada para difinir padrões de BD;
        /// </summary>
        public N0009TABMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODTPR });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODTPR)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.DESTPR)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.ABRTPR)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SITTPR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0009TAB", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODTPR).HasColumnName("CODTPR");
            this.Property(t => t.DESTPR).HasColumnName("DESTPR");
            this.Property(t => t.ABRTPR).HasColumnName("ABRTPR");
            this.Property(t => t.SITTPR).HasColumnName("SITTPR");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0009TAB)
                .HasForeignKey(d => d.CODEMP);

        }
    }
}
