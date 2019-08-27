using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0044CCUMap : EntityTypeConfiguration<Model.N0044CCU>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0044CCU, utilizada para difinir padrões de BD;
        /// </summary>
        public N0044CCUMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODCCU });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODCCU)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DESCCU)
                .HasMaxLength(50);

            this.Property(t => t.ABRCCU)
                .HasMaxLength(50);

            this.Property(t => t.MSKCCU)
                .HasMaxLength(50);

            this.Property(t => t.CLACCU)
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0044CCU", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODCCU).HasColumnName("CODCCU");
            this.Property(t => t.DESCCU).HasColumnName("DESCCU");
            this.Property(t => t.ABRCCU).HasColumnName("ABRCCU");
            this.Property(t => t.MSKCCU).HasColumnName("MSKCCU");
            this.Property(t => t.CLACCU).HasColumnName("CLACCU");
            this.Property(t => t.NIVCCU).HasColumnName("NIVCCU");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0044CCU)
                .HasForeignKey(d => d.CODEMP);

        }
    }
}
