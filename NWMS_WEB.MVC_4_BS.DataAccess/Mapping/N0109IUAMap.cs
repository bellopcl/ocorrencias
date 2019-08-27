using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0109IUAMap : EntityTypeConfiguration<Model.N0109IUA>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0109IUA, utilizada para difinir padrões de BD;
        /// </summary>
        public N0109IUAMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODUAR, t.SEQUAR });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODUAR)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQUAR)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODPRO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODDER)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UNIMED)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.SITIUA)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0109IUA", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODUAR).HasColumnName("CODUAR");
            this.Property(t => t.SEQUAR).HasColumnName("SEQUAR");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");
            this.Property(t => t.QTDORI).HasColumnName("QTDORI");
            this.Property(t => t.QTDDSP).HasColumnName("QTDDSP");
            this.Property(t => t.QTDRET).HasColumnName("QTDRET");
            this.Property(t => t.QTDRES).HasColumnName("QTDRES");
            this.Property(t => t.DATULT).HasColumnName("DATULT");
            this.Property(t => t.USUULT).HasColumnName("USUULT");
            this.Property(t => t.SITIUA).HasColumnName("SITIUA");
            this.Property(t => t.QTDPAD).HasColumnName("QTDPAD");

            // Relationships
            this.HasRequired(t => t.N0006DER)
                .WithMany(t => t.N0109IUA)
                .HasForeignKey(d => new { d.CODEMP, d.CODPRO, d.CODDER });
            this.HasRequired(t => t.N0007UNI)
                .WithMany(t => t.N0109IUA)
                .HasForeignKey(d => d.UNIMED);
            this.HasRequired(t => t.N0109UAR)
                .WithMany(t => t.N0109IUA)
                .HasForeignKey(d => new { d.CODEMP, d.CODUAR });

        }
    }
}
