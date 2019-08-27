using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N9999SISMap : EntityTypeConfiguration<Model.N9999SIS>
    {
        /// <summary>
        /// Classe de mapeamento da classe N9999SIS, utilizada para difinir padrões de BD;
        /// </summary>
        public N9999SISMap()
        {
            // Primary Key
            this.HasKey(t => t.CODSIS);

            // Properties
            this.Property(t => t.CODSIS)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESSIS)
                .IsRequired()
                .HasMaxLength(50);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
            // Table & Column Mappings
            this.ToTable("N9999SIS", connectionString);
            this.Property(t => t.CODSIS).HasColumnName("CODSIS");
            this.Property(t => t.DESSIS).HasColumnName("DESSIS");
        }
    }
}
