using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0202TRAMap : EntityTypeConfiguration<Model.N0202TRA>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0202TRA, utilizada para difinir padrões de BD;
        /// </summary>
        public N0202TRAMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.NUMREQ, t.SEQTRA });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NUMREQ)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQTRA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESTRA)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OBSTRA)
                .HasMaxLength(500);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0202TRA", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.NUMREQ).HasColumnName("NUMREQ");
            this.Property(t => t.SEQTRA).HasColumnName("SEQTRA");
            this.Property(t => t.DESTRA).HasColumnName("DESTRA");
            this.Property(t => t.USUGER).HasColumnName("USUGER");
            this.Property(t => t.DATGER).HasColumnName("DATGER");
            this.Property(t => t.OBSTRA).HasColumnName("OBSTRA");

            // Relationships
            this.HasRequired(t => t.N0202REQ)
                .WithMany(t => t.N0202TRA)
                .HasForeignKey(d => new { d.NUMREQ, d.CODEMP });

        }
    }
}
