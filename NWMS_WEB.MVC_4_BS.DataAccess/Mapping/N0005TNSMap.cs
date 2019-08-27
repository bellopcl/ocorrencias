using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0005TNSMap : EntityTypeConfiguration<Model.N0005TNS>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0005TNS, utilizada para difinir padrões de BD;
        /// </summary>
        public N0005TNSMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODTNS });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODTNS)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.DESTNS)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LISMOD)
                .HasMaxLength(10);

            this.Property(t => t.ESTEOS)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITTNS)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0005TNS", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODTNS).HasColumnName("CODTNS");
            this.Property(t => t.DESTNS).HasColumnName("DESTNS");
            this.Property(t => t.LISMOD).HasColumnName("LISMOD");
            this.Property(t => t.ESTEOS).HasColumnName("ESTEOS");
            this.Property(t => t.SITTNS).HasColumnName("SITTNS");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0005TNS)
                .HasForeignKey(d => d.CODEMP);

        }
    }
}
