using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0111LOTMap : EntityTypeConfiguration<Model.N0111LOT>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0111LOT, utilizada para difinir padrões de BD;
        /// </summary>
        public N0111LOTMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.CODARM, t.CODINV, t.SEQIUA, t.SEQLOT, t.SEQINV });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODARM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODINV)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQIUA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQLOT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQINV)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0111LOT", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.CODARM).HasColumnName("CODARM");
            this.Property(t => t.CODINV).HasColumnName("CODINV");
            this.Property(t => t.SEQIUA).HasColumnName("SEQIUA");
            this.Property(t => t.SEQLOT).HasColumnName("SEQLOT");
            this.Property(t => t.NUMSER).HasColumnName("NUMSER");
            this.Property(t => t.NUMLOT).HasColumnName("NUMLOT");
            this.Property(t => t.DATVLD).HasColumnName("DATVLD");
            this.Property(t => t.SEQINV).HasColumnName("SEQINV");

            // Relationships
            this.HasRequired(t => t.N0111ITV)
                .WithMany(t => t.N0111LOT)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL, d.CODARM, d.CODINV, d.SEQINV, d.SEQIUA });

        }
    }
}
