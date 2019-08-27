using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0006DERMap : EntityTypeConfiguration<Model.N0006DER>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0006DER, utilizada para difinir padrões de BD;
        /// </summary>
        public N0006DERMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODPRO, t.CODDER });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODPRO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODDER)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DESDER)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODAGT)
                .HasMaxLength(50);

            this.Property(t => t.EXPPAL)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FORLIN)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITDER)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BLOINV)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CURABC)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0006DER", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.DESDER).HasColumnName("DESDER");
            this.Property(t => t.CODBAR).HasColumnName("CODBAR");
            this.Property(t => t.CODAGT).HasColumnName("CODAGT");
            this.Property(t => t.DIAVLD).HasColumnName("DIAVLD");
            this.Property(t => t.PRECUS).HasColumnName("PRECUS");
            this.Property(t => t.PREMED).HasColumnName("PREMED");
            this.Property(t => t.PESBRU).HasColumnName("PESBRU");
            this.Property(t => t.PESLIQ).HasColumnName("PESLIQ");
            this.Property(t => t.EXPPAL).HasColumnName("EXPPAL");
            this.Property(t => t.FORLIN).HasColumnName("FORLIN");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.IMGDER).HasColumnName("IMGDER");
            this.Property(t => t.SITDER).HasColumnName("SITDER");
            this.Property(t => t.BLOINV).HasColumnName("BLOINV");
            this.Property(t => t.CURABC).HasColumnName("CURABC");

            // Relationships
            this.HasRequired(t => t.N0006PRO)
                .WithMany(t => t.N0006DER)
                .HasForeignKey(d => new { d.CODEMP, d.CODPRO });

        }
    }
}
