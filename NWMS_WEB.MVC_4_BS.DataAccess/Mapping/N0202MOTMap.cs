using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0202MOTMap : EntityTypeConfiguration<Model.N0202MOT>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0202MOT, utilizada para difinir padrões de BD;
        /// </summary>
        public N0202MOTMap()
        {
            // Primary Key
            this.HasKey(t => t.CODMOT);

            // Properties
            this.Property(t => t.CODMOT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESMOT)
                .IsRequired()
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0202MOT", connectionString);
            this.Property(t => t.CODMOT).HasColumnName("CODMOT");
            this.Property(t => t.DESMOT).HasColumnName("DESMOT");
        }
    }
}
