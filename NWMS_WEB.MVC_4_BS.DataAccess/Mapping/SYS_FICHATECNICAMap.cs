using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_FICHATECNICAMap : EntityTypeConfiguration<Model.SYS_FICHATECNICA>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_FICHATECNICA, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_FICHATECNICAMap()
        {
            // Primary Key
            this.HasKey(t => t.CODCATEGORIA);

            // Properties
            this.Property(t => t.CODCATEGORIA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOMECATEGORIA)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NOMETELA)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NOMETABELA)
                .IsRequired()
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_FICHATECNICA", connectionString);
            this.Property(t => t.CODCATEGORIA).HasColumnName("CODCATEGORIA");
            this.Property(t => t.NOMECATEGORIA).HasColumnName("NOMECATEGORIA");
            this.Property(t => t.NOMETELA).HasColumnName("NOMETELA");
            this.Property(t => t.NOMETABELA).HasColumnName("NOMETABELA");
        }
    }
}
