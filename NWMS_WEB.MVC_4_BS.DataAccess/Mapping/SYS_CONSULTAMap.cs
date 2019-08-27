using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_CONSULTAMap : EntityTypeConfiguration<Model.SYS_CONSULTA>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_CONSULTA, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_CONSULTAMap()
        {
            // Primary Key
            this.HasKey(t => t.CODCONSULTA);

            // Properties
            this.Property(t => t.CODCONSULTA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESCRICAO)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TIPODIAGR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_CONSULTA", connectionString);
            this.Property(t => t.CODCONSULTA).HasColumnName("CODCONSULTA");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
            this.Property(t => t.ESTRDIAGRAMA).HasColumnName("ESTRDIAGRAMA");
            this.Property(t => t.TIPODIAGR).HasColumnName("TIPODIAGR");
        }
    }
}
