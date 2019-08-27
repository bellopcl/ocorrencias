using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0203REGMap : EntityTypeConfiguration<N0203REG>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0203REG, utilizada para difinir padrões de BD;
        /// </summary>
        public N0203REGMap()
        {
            // Primary Key
            this.HasKey(t => t.NUMREG);

            // Properties
            this.Property(t => t.NUMREG)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OBSREG)
                .IsRequired()
                .HasMaxLength(400);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
            // Table & Column Mappings
            this.ToTable("N0203REG", connectionString);
            this.Property(t => t.NUMREG).HasColumnName("NUMREG");
            this.Property(t => t.SITREG).HasColumnName("SITREG");
            this.Property(t => t.DATGER).HasColumnName("DATGER");
            this.Property(t => t.USUGER).HasColumnName("USUGER");
            this.Property(t => t.TIPATE).HasColumnName("TIPATE");
            this.Property(t => t.ORIOCO).HasColumnName("ORIOCO");
            this.Property(t => t.DATULT).HasColumnName("DATULT");
            this.Property(t => t.USUULT).HasColumnName("USUULT");
            this.Property(t => t.USUFEC).HasColumnName("USUFEC");
            this.Property(t => t.DATFEC).HasColumnName("DATFEC");
            this.Property(t => t.CODCLI).HasColumnName("CODCLI");
            this.Property(t => t.CODMOT).HasColumnName("CODMOT");
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.OBSREG).HasColumnName("OBSREG");
            this.Property(t => t.PLACA).HasColumnName("PLACA");
            this.Property(t => t.APREAP).HasColumnName("APREAP");
        }
    }
}
