using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0203OPEMap : EntityTypeConfiguration<N0203OPE>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0203OPE, utilizada para difinir padrões de BD;
        /// </summary>
        public N0203OPEMap()
        {
            // Primary Key
            this.HasKey(t => t.CODOPE);

            // Properties
            this.Property(t => t.CODOPE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DSCOPE)
                .IsRequired()
                .HasMaxLength(50);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N0203OPE", connectionString);
            this.Property(t => t.CODOPE).HasColumnName("CODOPE");
            this.Property(t => t.DSCOPE).HasColumnName("DSCOPE");
        }
    }
}
