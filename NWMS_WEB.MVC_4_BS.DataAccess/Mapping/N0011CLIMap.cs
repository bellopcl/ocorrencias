using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0011CLIMap : EntityTypeConfiguration<Model.N0011CLI>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0011CLI, utilizada para difinir padrões de BD;
        /// </summary>
        public N0011CLIMap()
        {
            // Primary Key
            this.HasKey(t => t.CODCLI);

            // Properties
            this.Property(t => t.CODCLI)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOMCLI)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TIPCLI)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CODROE)
                .HasMaxLength(50);

            this.Property(t => t.CODSRO)
                .HasMaxLength(50);

            this.Property(t => t.SITCLI)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.NOMFAN)
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0011CLI", connectionString);
            this.Property(t => t.CODCLI).HasColumnName("CODCLI");
            this.Property(t => t.NOMCLI).HasColumnName("NOMCLI");
            this.Property(t => t.TIPCLI).HasColumnName("TIPCLI");
            this.Property(t => t.CGCCPF).HasColumnName("CGCCPF");
            this.Property(t => t.CODROE).HasColumnName("CODROE");
            this.Property(t => t.SEQROE).HasColumnName("SEQROE");
            this.Property(t => t.CODSRO).HasColumnName("CODSRO");
            this.Property(t => t.SITCLI).HasColumnName("SITCLI");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.NOMFAN).HasColumnName("NOMFAN");
        }
    }
}
