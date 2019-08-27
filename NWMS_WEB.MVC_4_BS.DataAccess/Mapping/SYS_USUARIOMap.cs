using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_USUARIOMap : EntityTypeConfiguration<Model.SYS_USUARIO>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_USUARIO, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_USUARIOMap()
        {
            // Primary Key
            this.HasKey(t => t.CODUSU);

            // Properties
            this.Property(t => t.CODUSU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOME)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.LOGIN)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SENHA)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TIPO)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PROLAYOUT)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LOGINWIN)
                .HasMaxLength(50);

            this.Property(t => t.REMETENTE)
                .HasMaxLength(100);

            this.Property(t => t.NOMEEXIB)
                .HasMaxLength(100);

            this.Property(t => t.AUTENTICACAO)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LOGINMAIL)
                .HasMaxLength(100);

            this.Property(t => t.SENHAMAIL)
                .HasMaxLength(100);

            this.Property(t => t.SMTP)
                .HasMaxLength(100);

            this.Property(t => t.POP)
                .HasMaxLength(100);

            this.Property(t => t.CONEXSEGURA)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DEIXARCOPIAMSG)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.MARCARLIDA)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TROCARSENHA)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TROCARSENHAEXP)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TODOSRELATORIOS)
                .HasMaxLength(15);

            this.Property(t => t.CFGMAIL)
                .HasMaxLength(15);

            this.Property(t => t.ENTERTOTAB)
                .HasMaxLength(15);

            this.Property(t => t.UPDATERULES)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GRAVARALTERACOES)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GRAVARNAVEGACAO)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GRAVARPERSONALIZACOES)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.USU_SITUSU)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.USU_CARUSU)
                .HasMaxLength(200);

            this.Property(t => t.USU_DEPUSU)
                .HasMaxLength(100);

            this.Property(t => t.USU_NUMCRX)
                .HasMaxLength(50);

            this.Property(t => t.USU_SITTAR)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RELATSIMULTANEO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SENWEB)
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_USUARIO", connectionString);
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.NOME).HasColumnName("NOME");
            this.Property(t => t.LOGIN).HasColumnName("LOGIN");
            this.Property(t => t.SENHA).HasColumnName("SENHA");
            this.Property(t => t.MENUEDITADO).HasColumnName("MENUEDITADO");
            this.Property(t => t.TOOLBAREDITADO).HasColumnName("TOOLBAREDITADO");
            this.Property(t => t.CTRLACESSO).HasColumnName("CTRLACESSO");
            this.Property(t => t.TIPO).HasColumnName("TIPO");
            this.Property(t => t.PROLAYOUT).HasColumnName("PROLAYOUT");
            this.Property(t => t.LOGINWIN).HasColumnName("LOGINWIN");
            this.Property(t => t.CODUSU_GRU).HasColumnName("CODUSU_GRU");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.REMETENTE).HasColumnName("REMETENTE");
            this.Property(t => t.NOMEEXIB).HasColumnName("NOMEEXIB");
            this.Property(t => t.AUTENTICACAO).HasColumnName("AUTENTICACAO");
            this.Property(t => t.LOGINMAIL).HasColumnName("LOGINMAIL");
            this.Property(t => t.SENHAMAIL).HasColumnName("SENHAMAIL");
            this.Property(t => t.SMTP).HasColumnName("SMTP");
            this.Property(t => t.POP).HasColumnName("POP");
            this.Property(t => t.PORTASMTP).HasColumnName("PORTASMTP");
            this.Property(t => t.PORTAPOP).HasColumnName("PORTAPOP");
            this.Property(t => t.CONEXSEGURA).HasColumnName("CONEXSEGURA");
            this.Property(t => t.DEIXARCOPIAMSG).HasColumnName("DEIXARCOPIAMSG");
            this.Property(t => t.MARCARLIDA).HasColumnName("MARCARLIDA");
            this.Property(t => t.DATALTSENHA).HasColumnName("DATALTSENHA");
            this.Property(t => t.EXPSENHA).HasColumnName("EXPSENHA");
            this.Property(t => t.TROCARSENHA).HasColumnName("TROCARSENHA");
            this.Property(t => t.TROCARSENHAEXP).HasColumnName("TROCARSENHAEXP");
            this.Property(t => t.TODOSRELATORIOS).HasColumnName("TODOSRELATORIOS");
            this.Property(t => t.CFGMAIL).HasColumnName("CFGMAIL");
            this.Property(t => t.ASSINATURA).HasColumnName("ASSINATURA");
            this.Property(t => t.ENTERTOTAB).HasColumnName("ENTERTOTAB");
            this.Property(t => t.UPDATERULES).HasColumnName("UPDATERULES");
            this.Property(t => t.TIMEROC).HasColumnName("TIMEROC");
            this.Property(t => t.GRAVARALTERACOES).HasColumnName("GRAVARALTERACOES");
            this.Property(t => t.GRAVARNAVEGACAO).HasColumnName("GRAVARNAVEGACAO");
            this.Property(t => t.GRAVARPERSONALIZACOES).HasColumnName("GRAVARPERSONALIZACOES");
            this.Property(t => t.USU_SITUSU).HasColumnName("USU_SITUSU");
            this.Property(t => t.USU_CARUSU).HasColumnName("USU_CARUSU");
            this.Property(t => t.USU_DEPUSU).HasColumnName("USU_DEPUSU");
            this.Property(t => t.USU_NUMCRX).HasColumnName("USU_NUMCRX");
            this.Property(t => t.USU_USUALT).HasColumnName("USU_USUALT");
            this.Property(t => t.USU_DATALT).HasColumnName("USU_DATALT");
            this.Property(t => t.USU_SITTAR).HasColumnName("USU_SITTAR");
            this.Property(t => t.RELATSIMULTANEO).HasColumnName("RELATSIMULTANEO");
            this.Property(t => t.SENWEB).HasColumnName("SENWEB");

            // Relationships
            this.HasOptional(t => t.SYS_USUARIO_MODEL)
                .WithMany(t => t.SYS_USUARIO_LISTA)
                .HasForeignKey(d => d.CODUSU_GRU);

        }
    }
}
