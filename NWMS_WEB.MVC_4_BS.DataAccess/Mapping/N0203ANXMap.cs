using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0203ANXMap : EntityTypeConfiguration<N0203ANX>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0203ANX, utilizada para difinir padrões de BD;
        /// </summary>
        public N0203ANXMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDROW, t.NUMREG });

            // Properties
            this.Property(t => t.IDROW)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NUMREG)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ANEXO)
                .IsRequired();

            this.Property(t => t.NOMANX)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.EXTANX)
                .IsRequired()
                .HasMaxLength(200);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0203ANX", connectionString);
            this.Property(t => t.IDROW).HasColumnName("IDROW");
            this.Property(t => t.NUMREG).HasColumnName("NUMREG");
            this.Property(t => t.ANEXO).HasColumnName("ANEXO");
            this.Property(t => t.NOMANX).HasColumnName("NOMANX");
            this.Property(t => t.EXTANX).HasColumnName("EXTANX");

            // Relationships
            this.HasRequired(t => t.N0203REG)
                .WithMany(t => t.N0203ANX)
                .HasForeignKey(d => d.NUMREG);

        }
    }
}
