using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0112TPTMap : EntityTypeConfiguration<Model.N0112TPT>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0112TPT, utilizada para difinir padrões de BD;
        /// </summary>
        public N0112TPTMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODTPT });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODTPT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESTPT)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TNSTPT)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SITTPT)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDBES)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDAID)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0112TPT", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODTPT).HasColumnName("CODTPT");
            this.Property(t => t.DESTPT).HasColumnName("DESTPT");
            this.Property(t => t.TEMEST).HasColumnName("TEMEST");
            this.Property(t => t.TNSTPT).HasColumnName("TNSTPT");
            this.Property(t => t.SITTPT).HasColumnName("SITTPT");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUULT).HasColumnName("USUULT");
            this.Property(t => t.DATULT).HasColumnName("DATULT");
            this.Property(t => t.INDBES).HasColumnName("INDBES");
            this.Property(t => t.INDAID).HasColumnName("INDAID");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0112TPT)
                .HasForeignKey(d => d.CODEMP);
            this.HasRequired(t => t.N0005TNS)
                .WithMany(t => t.N0112TPT)
                .HasForeignKey(d => new { d.CODEMP, d.TNSTPT });

        }
    }
}
