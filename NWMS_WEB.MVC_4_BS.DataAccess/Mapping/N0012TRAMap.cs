using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0012TRAMap : EntityTypeConfiguration<Model.N0012TRA>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0012TRA, utilizada para difinir padrões de BD;
        /// </summary>
        public N0012TRAMap()
        {
            // Primary Key
            this.HasKey(t => t.CODTRA);

            // Properties
            this.Property(t => t.CODTRA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOMTRA)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.APETRA)
                .HasMaxLength(50);

            this.Property(t => t.TIPTRA)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INSEST)
                .HasMaxLength(50);

            this.Property(t => t.INSMUN)
                .HasMaxLength(50);

            this.Property(t => t.SITTRA)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0012TRA", connectionString);
            this.Property(t => t.CODTRA).HasColumnName("CODTRA");
            this.Property(t => t.NOMTRA).HasColumnName("NOMTRA");
            this.Property(t => t.APETRA).HasColumnName("APETRA");
            this.Property(t => t.TIPTRA).HasColumnName("TIPTRA");
            this.Property(t => t.INSEST).HasColumnName("INSEST");
            this.Property(t => t.INSMUN).HasColumnName("INSMUN");
            this.Property(t => t.CGCCPF).HasColumnName("CGCCPF");
            this.Property(t => t.SITTRA).HasColumnName("SITTRA");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
        }
    }
}
