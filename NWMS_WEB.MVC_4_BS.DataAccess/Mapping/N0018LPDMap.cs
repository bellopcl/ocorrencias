using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0018LPDMap : EntityTypeConfiguration<Model.N0018LPD>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0018LPD, utilizada para difinir padrões de BD;
        /// </summary>
        public N0018LPDMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODPRO, t.CODDER, t.CODDEP });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODPRO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODDER)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODDEP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UNIMED)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ESTNEG)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITLPD)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0018LPD", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.CODDEP).HasColumnName("CODDEP");
            this.Property(t => t.DATINI).HasColumnName("DATINI");
            this.Property(t => t.SALINI).HasColumnName("SALINI");
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");
            this.Property(t => t.ESTNEG).HasColumnName("ESTNEG");
            this.Property(t => t.QTDEST).HasColumnName("QTDEST");
            this.Property(t => t.QTDBLO).HasColumnName("QTDBLO");
            this.Property(t => t.ESTREP).HasColumnName("ESTREP");
            this.Property(t => t.ESTMIN).HasColumnName("ESTMIN");
            this.Property(t => t.ESTMAX).HasColumnName("ESTMAX");
            this.Property(t => t.ESTMID).HasColumnName("ESTMID");
            this.Property(t => t.ESTMAD).HasColumnName("ESTMAD");
            this.Property(t => t.SITLPD).HasColumnName("SITLPD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");

            // Relationships
            this.HasRequired(t => t.N0006DER)
                .WithMany(t => t.N0018LPD)
                .HasForeignKey(d => new { d.CODEMP, d.CODPRO, d.CODDER });
            this.HasRequired(t => t.N0007UNI)
                .WithMany(t => t.N0018LPD)
                .HasForeignKey(d => d.UNIMED);
            this.HasRequired(t => t.N0018DEP)
                .WithMany(t => t.N0018LPD)
                .HasForeignKey(d => new { d.CODEMP, d.CODDEP });

        }
    }
}
