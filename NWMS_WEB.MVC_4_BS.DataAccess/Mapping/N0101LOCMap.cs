using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0101LOCMap : EntityTypeConfiguration<Model.N0101LOC>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0101LOC, utilizada para difinir padrões de BD;
        /// </summary>
        public N0101LOCMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.CODARM, t.CODLOC });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODARM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODLOC)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESLOC)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MSKEND)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.INDCRD)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TIPLOC)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITLOC)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDDOC)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0101LOC", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.CODARM).HasColumnName("CODARM");
            this.Property(t => t.CODLOC).HasColumnName("CODLOC");
            this.Property(t => t.DESLOC).HasColumnName("DESLOC");
            this.Property(t => t.MSKEND).HasColumnName("MSKEND");
            this.Property(t => t.INDCRD).HasColumnName("INDCRD");
            this.Property(t => t.TIPLOC).HasColumnName("TIPLOC");
            this.Property(t => t.SITLOC).HasColumnName("SITLOC");
            this.Property(t => t.INDDOC).HasColumnName("INDDOC");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");

            // Relationships
            this.HasRequired(t => t.N0003ARM)
                .WithMany(t => t.N0101LOC)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL, d.CODARM });

        }
    }
}
