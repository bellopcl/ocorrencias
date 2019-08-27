using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0023LEDMap : EntityTypeConfiguration<Model.N0023LED>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0023LED, utilizada para difinir padrões de BD;
        /// </summary>
        public N0023LEDMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.CODARM, t.CODLOC, t.CODEND, t.CODORI, t.NUMDOC });

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

            this.Property(t => t.CODORI)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NUMDOC)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODPRO)
                .HasMaxLength(50);

            this.Property(t => t.CODDER)
                .HasMaxLength(50);

            this.Property(t => t.UNIMED)
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0023LED", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.CODARM).HasColumnName("CODARM");
            this.Property(t => t.CODLOC).HasColumnName("CODLOC");
            this.Property(t => t.CODEND).HasColumnName("CODEND");
            this.Property(t => t.CODORI).HasColumnName("CODORI");
            this.Property(t => t.NUMDOC).HasColumnName("NUMDOC");
            this.Property(t => t.DATINI).HasColumnName("DATINI");
            this.Property(t => t.DATFIM).HasColumnName("DATFIM");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");
            this.Property(t => t.QTDPRO).HasColumnName("QTDPRO");
            this.Property(t => t.USULED).HasColumnName("USULED");
            this.Property(t => t.DATLED).HasColumnName("DATLED");
        }
    }
}
