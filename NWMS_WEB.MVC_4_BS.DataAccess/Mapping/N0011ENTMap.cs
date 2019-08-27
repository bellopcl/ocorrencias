using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0011ENTMap : EntityTypeConfiguration<Model.N0011ENT>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0011ENT, utilizada para difinir padrões de BD;
        /// </summary>
        public N0011ENTMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODCLI, t.SEQENT });

            // Properties
            this.Property(t => t.CODCLI)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQENT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ENDENT)
                .HasMaxLength(500);

            this.Property(t => t.CPLENT)
                .HasMaxLength(500);

            this.Property(t => t.CIDENT)
                .HasMaxLength(50);

            this.Property(t => t.ESTENT)
                .HasMaxLength(50);

            this.Property(t => t.BAIENT)
                .HasMaxLength(50);

            this.Property(t => t.NEMENT)
                .HasMaxLength(150);

            this.Property(t => t.LATENT)
                .HasMaxLength(50);

            this.Property(t => t.LOGENT)
                .HasMaxLength(50);

            this.Property(t => t.SITREG)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0011ENT", connectionString);
            this.Property(t => t.CODCLI).HasColumnName("CODCLI");
            this.Property(t => t.SEQENT).HasColumnName("SEQENT");
            this.Property(t => t.ENDENT).HasColumnName("ENDENT");
            this.Property(t => t.CPLENT).HasColumnName("CPLENT");
            this.Property(t => t.CEPENT).HasColumnName("CEPENT");
            this.Property(t => t.CIDENT).HasColumnName("CIDENT");
            this.Property(t => t.ESTENT).HasColumnName("ESTENT");
            this.Property(t => t.BAIENT).HasColumnName("BAIENT");
            this.Property(t => t.NEMENT).HasColumnName("NEMENT");
            this.Property(t => t.LATENT).HasColumnName("LATENT");
            this.Property(t => t.LOGENT).HasColumnName("LOGENT");
            this.Property(t => t.SITREG).HasColumnName("SITREG");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
        }
    }
}
