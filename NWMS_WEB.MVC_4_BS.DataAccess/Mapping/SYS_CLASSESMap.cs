using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_CLASSESMap : EntityTypeConfiguration<Model.SYS_CLASSES>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_CLASSES, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_CLASSESMap()
        {
            // Primary Key
            this.HasKey(t => t.CLASSE);

            // Properties
            this.Property(t => t.CLASSE)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.PUBLICADO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.COMPILADO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_CLASSES", connectionString);
            this.Property(t => t.CLASSE).HasColumnName("CLASSE");
            this.Property(t => t.PUBLICADO).HasColumnName("PUBLICADO");
            this.Property(t => t.COMPILADO).HasColumnName("COMPILADO");
            this.Property(t => t.SOURCE).HasColumnName("SOURCE");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
        }
    }
}
