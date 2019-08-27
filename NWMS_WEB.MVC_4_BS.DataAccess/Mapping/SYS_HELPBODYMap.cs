using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_HELPBODYMap : EntityTypeConfiguration<Model.SYS_HELPBODY>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_HELPBODY, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_HELPBODYMap()
        {
            // Primary Key
            this.HasKey(t => t.CODHELP);

            // Properties
            this.Property(t => t.CODHELP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_HELPBODY", connectionString);
            this.Property(t => t.CODHELP).HasColumnName("CODHELP");
            this.Property(t => t.CONTEUDOHTML_ENG).HasColumnName("CONTEUDOHTML_ENG");
            this.Property(t => t.CONTEUDOHTML_ESP).HasColumnName("CONTEUDOHTML_ESP");
            this.Property(t => t.CONTEUDOHTML_PTB).HasColumnName("CONTEUDOHTML_PTB");
            this.Property(t => t.CONTEUDOTXT_ENG).HasColumnName("CONTEUDOTXT_ENG");
            this.Property(t => t.CONTEUDOTXT_ESP).HasColumnName("CONTEUDOTXT_ESP");
            this.Property(t => t.CONTEUDOTXT_PTB).HasColumnName("CONTEUDOTXT_PTB");
            this.Property(t => t.DATALT).HasColumnName("DATALT");

            // Relationships
            this.HasRequired(t => t.SYS_HELP)
                .WithOptional(t => t.SYS_HELPBODY);

        }
    }
}
