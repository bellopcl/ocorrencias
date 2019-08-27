using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class SYS_USRCOMPONENTMap : EntityTypeConfiguration<Model.SYS_USRCOMPONENT>
    {
        /// <summary>
        /// Classe de mapeamento da classe SYS_USRCOMPONENT, utilizada para difinir padrões de BD;
        /// </summary>
        public SYS_USRCOMPONENTMap()
        {
            // Primary Key
            this.HasKey(t => t.CODCOMPONENT);

            // Properties
            this.Property(t => t.CODCOMPONENT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DATAOBJECT)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.DESCRICAO)
                .HasMaxLength(150);

            this.Property(t => t.NOME)
                .HasMaxLength(150);

            this.Property(t => t.PARENT)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.TIPO)
                .IsRequired()
                .HasMaxLength(300);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("SYS_USRCOMPONENT", connectionString);
            this.Property(t => t.CODCOMPONENT).HasColumnName("CODCOMPONENT");
            this.Property(t => t.CODFORM).HasColumnName("CODFORM");
            this.Property(t => t.CODUSRCOLUMN).HasColumnName("CODUSRCOLUMN");
            this.Property(t => t.CONFCOMPONENT).HasColumnName("CONFCOMPONENT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.DATAOBJECT).HasColumnName("DATAOBJECT");
            this.Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
            this.Property(t => t.NOME).HasColumnName("NOME");
            this.Property(t => t.PARENT).HasColumnName("PARENT");
            this.Property(t => t.TIPO).HasColumnName("TIPO");

            // Relationships
            this.HasOptional(t => t.SYS_USRCOLUMN)
                .WithMany(t => t.SYS_USRCOMPONENT)
                .HasForeignKey(d => d.CODUSRCOLUMN);
            this.HasRequired(t => t.SYS_USRFORM)
                .WithMany(t => t.SYS_USRCOMPONENT)
                .HasForeignKey(d => d.CODFORM);

        }
    }
}
