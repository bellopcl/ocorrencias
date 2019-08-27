using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0203TRAMap : EntityTypeConfiguration<Model.N0203TRA>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0203TRA, utilizada para difinir padrões de BD;
        /// </summary>
        public N0203TRAMap()
        {
            // Primary Key
            this.HasKey(t => new { t.NUMREG, t.SEQTRA });

            // Properties
            this.Property(t => t.NUMREG)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQTRA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESTRA)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OBSTRA)
                .HasMaxLength(1000);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N0203TRA", connectionString);
            this.Property(t => t.NUMREG).HasColumnName("NUMREG");
            this.Property(t => t.SEQTRA).HasColumnName("SEQTRA");
            this.Property(t => t.DESTRA).HasColumnName("DESTRA");
            this.Property(t => t.USUTRA).HasColumnName("USUTRA");
            this.Property(t => t.DATTRA).HasColumnName("DATTRA");
            this.Property(t => t.OBSTRA).HasColumnName("OBSTRA");

            // Relationships
            this.HasRequired(t => t.N0203REG)
                .WithMany(t => t.N0203TRA)
                .HasForeignKey(d => d.NUMREG);

        }
    }
}
