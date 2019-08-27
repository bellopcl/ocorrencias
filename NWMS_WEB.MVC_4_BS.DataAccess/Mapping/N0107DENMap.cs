using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0107DENMap : EntityTypeConfiguration<Model.N0107DEN>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0107DEN, utilizada para difinir padrões de BD;
        /// </summary>
        public N0107DENMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.CODARM, t.CODLOC, t.CODEND, t.CODPRO, t.CODDER, t.UNIMED });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODARM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODLOC)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODEND)
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

            this.Property(t => t.SITDEN)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0107DEN", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.CODARM).HasColumnName("CODARM");
            this.Property(t => t.CODLOC).HasColumnName("CODLOC");
            this.Property(t => t.CODEND).HasColumnName("CODEND");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");
            this.Property(t => t.CODTLE).HasColumnName("CODTLE");
            this.Property(t => t.SITDEN).HasColumnName("SITDEN");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.QTDPRO).HasColumnName("QTDPRO");

            // Relationships
            this.HasRequired(t => t.N0006DER)
                .WithMany(t => t.N0107DEN)
                .HasForeignKey(d => new { d.CODEMP, d.CODPRO, d.CODDER });
            this.HasRequired(t => t.N0007UNI)
                .WithMany(t => t.N0107DEN)
                .HasForeignKey(d => d.UNIMED);
            this.HasRequired(t => t.N0105TLE)
                .WithMany(t => t.N0107DEN)
                .HasForeignKey(d => new { d.CODEMP, d.CODTLE });
            this.HasRequired(t => t.N0106END)
                .WithMany(t => t.N0107DEN)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL, d.CODARM, d.CODLOC, d.CODEND });

        }
    }
}
