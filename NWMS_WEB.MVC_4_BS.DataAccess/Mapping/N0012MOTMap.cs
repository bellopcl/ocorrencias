using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0012MOTMap : EntityTypeConfiguration<Model.N0012MOT>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0012MOT, utilizada para difinir padrões de BD;
        /// </summary>
        public N0012MOTMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TRAMOT, t.CODMOT });

            // Properties
            this.Property(t => t.TRAMOT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODMOT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOMMOT)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CGCCPF)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SITMOT)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0012MOT", connectionString);
            this.Property(t => t.TRAMOT).HasColumnName("TRAMOT");
            this.Property(t => t.CODMOT).HasColumnName("CODMOT");
            this.Property(t => t.NOMMOT).HasColumnName("NOMMOT");
            this.Property(t => t.CGCCPF).HasColumnName("CGCCPF");
            this.Property(t => t.SITMOT).HasColumnName("SITMOT");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");

            // Relationships
            this.HasRequired(t => t.N0012TRA)
                .WithMany(t => t.N0012MOT)
                .HasForeignKey(d => d.TRAMOT);

        }
    }
}
