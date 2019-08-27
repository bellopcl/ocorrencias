using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_MAILMap : EntityTypeConfiguration<Model.SYS_MAIL>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_MAIL, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_MAILMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODCAI, t.CODMAI, t.CODUSU });

            // Properties
            this.Property(t => t.ASSUNTO)
                .HasMaxLength(250);

            this.Property(t => t.CODCAI)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODMAI)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODUSU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESTCC)
                .HasMaxLength(250);

            this.Property(t => t.DESTPARA)
                .HasMaxLength(250);

            this.Property(t => t.EMAILLIDO)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.MESSAGEID)
                .HasMaxLength(250);

            this.Property(t => t.REMETENTE)
                .HasMaxLength(250);

            this.Property(t => t.TEMANEXOS)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_MAIL", connectionString);
            this.Property(t => t.ASSUNTO).HasColumnName("ASSUNTO");
            this.Property(t => t.CODCAI).HasColumnName("CODCAI");
            this.Property(t => t.CODMAI).HasColumnName("CODMAI");
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.DATCRIA).HasColumnName("DATCRIA");
            this.Property(t => t.DATSENT).HasColumnName("DATSENT");
            this.Property(t => t.DATVIEW).HasColumnName("DATVIEW");
            this.Property(t => t.DESTCC).HasColumnName("DESTCC");
            this.Property(t => t.DESTPARA).HasColumnName("DESTPARA");
            this.Property(t => t.EMAILLIDO).HasColumnName("EMAILLIDO");
            this.Property(t => t.MESSAGEID).HasColumnName("MESSAGEID");
            this.Property(t => t.REMETENTE).HasColumnName("REMETENTE");
            this.Property(t => t.SOURCE).HasColumnName("SOURCE");
            this.Property(t => t.TAMANHO).HasColumnName("TAMANHO");
            this.Property(t => t.TEMANEXOS).HasColumnName("TEMANEXOS");

            // Relationships
            this.HasRequired(t => t.SYS_CAIXA)
                .WithMany(t => t.SYS_MAIL)
                .HasForeignKey(d => new { d.CODUSU, d.CODCAI });

        }
    }
}
