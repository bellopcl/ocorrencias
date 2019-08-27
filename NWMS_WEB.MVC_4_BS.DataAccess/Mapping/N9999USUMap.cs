using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N9999USUMap : EntityTypeConfiguration<N9999USU>
    {
        /// <summary>
        /// Classe de mapeamento da classe N9999USU, utilizada para difinir padrões de BD;
        /// </summary>
        public N9999USUMap()
        {
            // Primary Key
            this.HasKey(t => t.CODUSU);

            // Properties
            this.Property(t => t.CODUSU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LOGIN)
                .IsRequired()
                .HasMaxLength(50);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N9999USU", connectionString);
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.LOGIN).HasColumnName("LOGIN");
        }
    }
}
