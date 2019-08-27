using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0001EMPMap : EntityTypeConfiguration<Model.N0001EMPModel>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0001EMPModel, utilizada para criar padrões de BD;
        /// </summary>
        public N0001EMPMap()
        {
            // Primary Key
            this.HasKey(t => t.CODEMP);

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RAZSOC)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NOMABR)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SITEMP)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.USUSID)
                .HasMaxLength(50);

            this.Property(t => t.SENSID)
                .HasMaxLength(50);

            this.Property(t => t.ENDSID)
                .HasMaxLength(50);

            this.Property(t => t.USUDOM)
                .HasMaxLength(50);

            this.Property(t => t.SENDOM)
                .HasMaxLength(50);

            this.Property(t => t.NOMDOM)
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0001EMP", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.RAZSOC).HasColumnName("RAZSOC");
            this.Property(t => t.NOMABR).HasColumnName("NOMABR");
            this.Property(t => t.CGCEMP).HasColumnName("CGCEMP");
            this.Property(t => t.LOGEMP).HasColumnName("LOGEMP");
            this.Property(t => t.SITEMP).HasColumnName("SITEMP");
            this.Property(t => t.DATGER).HasColumnName("DATGER");
            this.Property(t => t.USUGER).HasColumnName("USUGER");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.USUSID).HasColumnName("USUSID");
            this.Property(t => t.SENSID).HasColumnName("SENSID");
            this.Property(t => t.ENDSID).HasColumnName("ENDSID");
            this.Property(t => t.USUDOM).HasColumnName("USUDOM");
            this.Property(t => t.SENDOM).HasColumnName("SENDOM");
            this.Property(t => t.NOMDOM).HasColumnName("NOMDOM");

            // Relationships
            this.HasOptional(t => t.SYS_USUARIO)
                .WithMany(t => t.N0001EMP)
                .HasForeignKey(d => d.USUGER);
            this.HasOptional(t => t.SYS_USUARIO1)
                .WithMany(t => t.N0001EMP1)
                .HasForeignKey(d => d.USUALT);

        }
    }
}
