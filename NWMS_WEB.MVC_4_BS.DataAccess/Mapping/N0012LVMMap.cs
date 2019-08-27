using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0012LVMMap : EntityTypeConfiguration<Model.N0012LVM>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0012LVM, utilizada para difinir padrões de BD;
        /// </summary>
        public N0012LVMMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODTRA, t.PLAVEI, t.TRAMOT, t.CODMOT });

            // Properties
            this.Property(t => t.CODTRA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PLAVEI)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TRAMOT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODMOT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SITLVM)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0012LVM", connectionString);
            this.Property(t => t.CODTRA).HasColumnName("CODTRA");
            this.Property(t => t.PLAVEI).HasColumnName("PLAVEI");
            this.Property(t => t.TRAMOT).HasColumnName("TRAMOT");
            this.Property(t => t.CODMOT).HasColumnName("CODMOT");
            this.Property(t => t.SITLVM).HasColumnName("SITLVM");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");

            // Relationships
            this.HasRequired(t => t.N0012MOT)
                .WithMany(t => t.N0012LVM)
                .HasForeignKey(d => new { d.TRAMOT, d.CODMOT });
            this.HasRequired(t => t.N0012VEI)
                .WithMany(t => t.N0012LVM)
                .HasForeignKey(d => new { d.CODTRA, d.PLAVEI });

        }
    }
}
