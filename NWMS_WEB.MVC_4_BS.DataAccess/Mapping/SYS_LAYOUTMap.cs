using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_LAYOUTMap : EntityTypeConfiguration<Model.SYS_LAYOUT>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_LAYOUT, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_LAYOUTMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOME)
                .HasMaxLength(50);

            this.Property(t => t.IMPEXP)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DIRPADRAO)
                .HasMaxLength(250);

            this.Property(t => t.EXTENSAO)
                .HasMaxLength(10);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_LAYOUT", connectionString);
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.COD).HasColumnName("COD");
            this.Property(t => t.NOME).HasColumnName("NOME");
            this.Property(t => t.IMPEXP).HasColumnName("IMPEXP");
            this.Property(t => t.DIRPADRAO).HasColumnName("DIRPADRAO");
            this.Property(t => t.IDUNICO).HasColumnName("IDUNICO");
            this.Property(t => t.REGRAS).HasColumnName("REGRAS");
            this.Property(t => t.LAYOUT).HasColumnName("LAYOUT");
            this.Property(t => t.EXTENSAO).HasColumnName("EXTENSAO");
            this.Property(t => t.FILTRO).HasColumnName("FILTRO");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
        }
    }
}
