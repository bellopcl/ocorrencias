using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_USRCOLUMNMap : EntityTypeConfiguration<Model.SYS_USRCOLUMN>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_USRCOLUMN, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_USRCOLUMNMap()
        {
            // Primary Key
            this.HasKey(t => t.CODUSRCOLUMN);

            // Properties
            this.Property(t => t.CHAVE_PK)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CODUSRCOLUMN)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESCRICAO)
                .HasMaxLength(200);

            this.Property(t => t.NOME)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.OBRIGATORIO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TIPO)
                .IsRequired()
                .HasMaxLength(300);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_USRCOLUMN", connectionString);
            this.Property(t => t.CHAVE_PK).HasColumnName("CHAVE_PK");
            this.Property(t => t.CODUSRCOLUMN).HasColumnName("CODUSRCOLUMN");
            this.Property(t => t.CODUSRTABLE).HasColumnName("CODUSRTABLE");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.DECIMAIS).HasColumnName("DECIMAIS");
            this.Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
            this.Property(t => t.NOME).HasColumnName("NOME");
            this.Property(t => t.OBRIGATORIO).HasColumnName("OBRIGATORIO");
            this.Property(t => t.POSICAO_PK).HasColumnName("POSICAO_PK");
            this.Property(t => t.TAMANHO).HasColumnName("TAMANHO");
            this.Property(t => t.TIPO).HasColumnName("TIPO");
            this.Property(t => t.VALOR_PADRAO).HasColumnName("VALOR_PADRAO");

            // Relationships
            this.HasMany(t => t.SYS_USUARIO)
                .WithMany(t => t.SYS_USRCOLUMN)
                .Map(m =>
                    {
                        m.ToTable("SYS_USRCOLUMNXSYS_USUARIO", connectionString);
                        m.MapLeftKey("CODUSRCOLUMN");
                        m.MapRightKey("CODUSU");
                    });

            this.HasRequired(t => t.SYS_USRTABLE)
                .WithMany(t => t.SYS_USRCOLUMN)
                .HasForeignKey(d => d.CODUSRTABLE);

        }
    }
}
