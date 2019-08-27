using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;
namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0111CURMap : EntityTypeConfiguration<Model.N0111CURModel>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0111CURModel, utilizada para difinir padrões de BD;
        /// </summary>
        public N0111CURMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CURABC });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //this.Property(t => t.CURABC)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0111CUR", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CURABC).HasColumnName("CURABC");
            this.Property(t => t.QTDDER).HasColumnName("QTDDER");

        }
    }
}
