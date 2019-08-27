using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0001EMP_HISMap : EntityTypeConfiguration<Model.N0001EMP_HIS>
    {
        /// <summary>
        /// Classe de mapeamento para a classe N0001EMP_HIS, utilizada para criar padrões de tabelas BD;
        /// </summary>
        public N0001EMP_HISMap()
        {
            // Primary Key
            this.HasKey(t => t.SEQHIS);

            // Properties
            this.Property(t => t.SEQHIS)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TIPHIS)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RAZSOC)
                .HasMaxLength(50);

            this.Property(t => t.NOMABR)
                .HasMaxLength(50);

            this.Property(t => t.SITEMP)
                .IsFixedLength()
                .HasMaxLength(1);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
            // Table & Column Mappings
            this.ToTable("N0001EMP_HIS", connectionString);
            this.Property(t => t.SEQHIS).HasColumnName("SEQHIS");
            this.Property(t => t.DATHIS).HasColumnName("DATHIS");
            this.Property(t => t.TIPHIS).HasColumnName("TIPHIS");
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.RAZSOC).HasColumnName("RAZSOC");
            this.Property(t => t.NOMABR).HasColumnName("NOMABR");
            this.Property(t => t.CGCEMP).HasColumnName("CGCEMP");
            this.Property(t => t.LOGEMP).HasColumnName("LOGEMP");
            this.Property(t => t.SITEMP).HasColumnName("SITEMP");
            this.Property(t => t.USUHIS).HasColumnName("USUHIS");
        }
    }
}
