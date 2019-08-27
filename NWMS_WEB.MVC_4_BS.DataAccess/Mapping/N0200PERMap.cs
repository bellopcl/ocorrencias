using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0200PERMap : EntityTypeConfiguration<Model.N0200PER>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0200PER, utilizada para difinir padrões de BD;
        /// </summary>
        public N0200PERMap()
        {
            // Primary Key
            this.HasKey(t => t.CODPER);

            // Properties
            this.Property(t => t.CODPER)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESPER)
                .IsRequired()
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0200PER", connectionString);
            this.Property(t => t.CODPER).HasColumnName("CODPER");
            this.Property(t => t.DESPER).HasColumnName("DESPER");
        }
    }
}
