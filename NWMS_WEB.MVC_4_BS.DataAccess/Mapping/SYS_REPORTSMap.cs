using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_REPORTSMap : EntityTypeConfiguration<Model.SYS_REPORTS>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_REPORTS, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_REPORTSMap()
        {
            // Primary Key
            this.HasKey(t => t.CODREL);

            // Properties
            this.Property(t => t.CODREL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESCRICAO)
                .HasMaxLength(100);

            this.Property(t => t.ID_DEV)
                .HasMaxLength(50);

            this.Property(t => t.TIPO)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EDITAVEL)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_REPORTS", connectionString);
            this.Property(t => t.CODREL).HasColumnName("CODREL");
            this.Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
            this.Property(t => t.ID_DEV).HasColumnName("ID_DEV");
            this.Property(t => t.TIPO).HasColumnName("TIPO");
            this.Property(t => t.COMANDOSQL).HasColumnName("COMANDOSQL");
            this.Property(t => t.DIAGRAMA).HasColumnName("DIAGRAMA");
            this.Property(t => t.FILTROEXTERNO).HasColumnName("FILTROEXTERNO");
            this.Property(t => t.HELP).HasColumnName("HELP");
            this.Property(t => t.ESTRUTURA).HasColumnName("ESTRUTURA");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.EDITAVEL).HasColumnName("EDITAVEL");
        }
    }
}
