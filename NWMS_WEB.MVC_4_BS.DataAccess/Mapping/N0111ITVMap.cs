using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0111ITVMap : EntityTypeConfiguration<Model.N0111ITV>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0111ITV, utilizada para difinir padrões de BD;
        /// </summary>
        public N0111ITVMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.CODARM, t.CODINV, t.SEQINV, t.SEQIUA });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODARM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODINV)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQINV)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQIUA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODPRO)
                .HasMaxLength(50);

            this.Property(t => t.CODDER)
                .HasMaxLength(50);

            this.Property(t => t.UNIMED)
                .HasMaxLength(50);

            this.Property(t => t.PRONOV)
                .HasMaxLength(50);

            this.Property(t => t.DERNOV)
                .HasMaxLength(50);

            this.Property(t => t.UNINOV)
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0111ITV", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.CODARM).HasColumnName("CODARM");
            this.Property(t => t.CODINV).HasColumnName("CODINV");
            this.Property(t => t.SEQINV).HasColumnName("SEQINV");
            this.Property(t => t.SEQIUA).HasColumnName("SEQIUA");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");
            this.Property(t => t.QTDDSP).HasColumnName("QTDDSP");
            this.Property(t => t.QTDRES).HasColumnName("QTDRES");
            this.Property(t => t.QTDORI).HasColumnName("QTDORI");
            this.Property(t => t.QTDPAD).HasColumnName("QTDPAD");
            this.Property(t => t.QTDAC1).HasColumnName("QTDAC1");
            this.Property(t => t.USUAC1).HasColumnName("USUAC1");
            this.Property(t => t.QTDAC2).HasColumnName("QTDAC2");
            this.Property(t => t.USUAC2).HasColumnName("USUAC2");
            this.Property(t => t.QTDAC3).HasColumnName("QTDAC3");
            this.Property(t => t.USUAC3).HasColumnName("USUAC3");
            this.Property(t => t.QTDAC4).HasColumnName("QTDAC4");
            this.Property(t => t.USUAC4).HasColumnName("USUAC4");
            this.Property(t => t.QTDAPL).HasColumnName("QTDAPL");
            this.Property(t => t.PRONOV).HasColumnName("PRONOV");
            this.Property(t => t.DERNOV).HasColumnName("DERNOV");
            this.Property(t => t.UNINOV).HasColumnName("UNINOV");

            // Relationships
            this.HasOptional(t => t.N0006DER)
                .WithMany(t => t.N0111ITV)
                .HasForeignKey(d => new { d.CODEMP, d.CODPRO, d.CODDER });
            this.HasOptional(t => t.N0007UNI)
                .WithMany(t => t.N0111ITV)
                .HasForeignKey(d => d.UNIMED);
            this.HasRequired(t => t.N0111UAV)
                .WithMany(t => t.N0111ITV)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL, d.CODARM, d.CODINV, d.SEQINV });

        }
    }
}
