using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0109LOTMap : EntityTypeConfiguration<Model.N0109LOT>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0109LOT, utilizada para difinir padrões de BD;
        /// </summary>
        public N0109LOTMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODUAR, t.SEQUAR, t.SEQLOT });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODUAR)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQUAR)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQLOT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0109LOT", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODUAR).HasColumnName("CODUAR");
            this.Property(t => t.SEQUAR).HasColumnName("SEQUAR");
            this.Property(t => t.SEQLOT).HasColumnName("SEQLOT");
            this.Property(t => t.NUMSER).HasColumnName("NUMSER");
            this.Property(t => t.NUMLOT).HasColumnName("NUMLOT");
            this.Property(t => t.DATVLD).HasColumnName("DATVLD");

            // Relationships
            this.HasRequired(t => t.N0109IUA)
                .WithMany(t => t.N0109LOT)
                .HasForeignKey(d => new { d.CODEMP, d.CODUAR, d.SEQUAR });

        }
    }
}
