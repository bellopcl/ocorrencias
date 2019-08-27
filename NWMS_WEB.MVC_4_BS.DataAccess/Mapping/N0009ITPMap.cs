using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0009ITPMap : EntityTypeConfiguration<Model.N0009ITP>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0009ITP, utilizada para difinir padrões de BD;
        /// </summary>
        public N0009ITPMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODTPR, t.DATINI, t.CODPRO, t.CODDER, t.QTDMAX });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODTPR)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.CODPRO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODDER)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.QTDMAX)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SITITP)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0009ITP", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODTPR).HasColumnName("CODTPR");
            this.Property(t => t.DATINI).HasColumnName("DATINI");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.QTDMAX).HasColumnName("QTDMAX");
            this.Property(t => t.PREBAS).HasColumnName("PREBAS");
            this.Property(t => t.TOLMAI).HasColumnName("TOLMAI");
            this.Property(t => t.TOLMEN).HasColumnName("TOLMEN");
            this.Property(t => t.SITITP).HasColumnName("SITITP");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");

            // Relationships
            this.HasRequired(t => t.N0006DER)
                .WithMany(t => t.N0009ITP)
                .HasForeignKey(d => new { d.CODEMP, d.CODPRO, d.CODDER });
            this.HasRequired(t => t.N0009VLD)
                .WithMany(t => t.N0009ITP)
                .HasForeignKey(d => new { d.CODEMP, d.CODTPR, d.DATINI });

        }
    }
}
