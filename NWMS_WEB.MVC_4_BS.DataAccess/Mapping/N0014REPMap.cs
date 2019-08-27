using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0014REPMap : EntityTypeConfiguration<Model.N0014REP>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0014REP, utilizada para difinir padrões de BD;
        /// </summary>
        public N0014REPMap()
        {
            // Primary Key
            this.HasKey(t => t.CODREP);

            // Properties
            this.Property(t => t.CODREP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOMREP)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.SIGUFS)
                .HasMaxLength(2);

            this.Property(t => t.TIPREP)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0014REP", connectionString);
            this.Property(t => t.CODREP).HasColumnName("CODREP");
            this.Property(t => t.NOMREP).HasColumnName("NOMREP");
            this.Property(t => t.SIGUFS).HasColumnName("SIGUFS");
            this.Property(t => t.TIPREP).HasColumnName("TIPREP");
        }
    }
}
