using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0007UNIMap : EntityTypeConfiguration<Model.N0007UNI>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0007UNI, utilizada para difinir padrões de BD;
        /// </summary>
        public N0007UNIMap()
        {
            // Primary Key
            this.HasKey(t => t.UNIMED);

            // Properties
            this.Property(t => t.UNIMED)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.DESMED)
                .IsRequired()
                .HasMaxLength(20);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0007UNI", connectionString);
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");
            this.Property(t => t.DESMED).HasColumnName("DESMED");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
        }
    }
}
