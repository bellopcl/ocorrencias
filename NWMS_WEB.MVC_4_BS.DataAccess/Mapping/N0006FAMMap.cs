using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0006FAMMap : EntityTypeConfiguration<Model.N0006FAM>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0006FAM, utilizada para difinir padrões de BD;
        /// </summary>
        public N0006FAMMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFAM });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFAM)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DESFAM)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODORI)
                .HasMaxLength(3);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0006FAM", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFAM).HasColumnName("CODFAM");
            this.Property(t => t.DESFAM).HasColumnName("DESFAM");
            this.Property(t => t.CODORI).HasColumnName("CODORI");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0006FAM)
                .HasForeignKey(d => d.CODEMP);

        }
    }
}
