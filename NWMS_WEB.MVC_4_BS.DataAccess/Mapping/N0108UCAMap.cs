using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0108UCAMap : EntityTypeConfiguration<Model.N0108UCA>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0108UCA, utilizada para difinir padrões de BD;
        /// </summary>
        public N0108UCAMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODBAR });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODBAR)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OBSTUA)
                .HasMaxLength(100);

            this.Property(t => t.SITUCA)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDMAX)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDMPU)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CODPRO)
                .HasMaxLength(14);

            this.Property(t => t.CODDER)
                .HasMaxLength(7);

            this.Property(t => t.UNIMED)
                .HasMaxLength(3);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0108UCA", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODBAR).HasColumnName("CODBAR");
            this.Property(t => t.QTDLAS).HasColumnName("QTDLAS");
            this.Property(t => t.QTDPLA).HasColumnName("QTDPLA");
            this.Property(t => t.QTDMAX).HasColumnName("QTDMAX");
            this.Property(t => t.OBSTUA).HasColumnName("OBSTUA");
            this.Property(t => t.SITUCA).HasColumnName("SITUCA");
            this.Property(t => t.CODTUA).HasColumnName("CODTUA");
            this.Property(t => t.INDMAX).HasColumnName("INDMAX");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.INDMPU).HasColumnName("INDMPU");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0108UCA)
                .HasForeignKey(d => d.CODEMP);
            this.HasRequired(t => t.N0103TUA)
                .WithMany(t => t.N0108UCA)
                .HasForeignKey(d => new { d.CODEMP, d.CODTUA });
            this.HasRequired(t => t.SYS_USUARIO)
                .WithMany(t => t.N0108UCA)
                .HasForeignKey(d => d.USUALT);
            this.HasRequired(t => t.SYS_USUARIO1)
                .WithMany(t => t.N0108UCA1)
                .HasForeignKey(d => d.USUCAD);

        }
    }
}
