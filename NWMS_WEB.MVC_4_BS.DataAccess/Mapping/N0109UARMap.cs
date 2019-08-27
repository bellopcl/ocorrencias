using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0109UARMap : EntityTypeConfiguration<Model.N0109UAR>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0109UAR, utilizada para difinir padrões de BD;
        /// </summary>
        public N0109UARMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODUAR });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODUAR)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SITUAR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITENT)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITSAI)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.UARGEN)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0109UAR", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODUAR).HasColumnName("CODUAR");
            this.Property(t => t.CODTUA).HasColumnName("CODTUA");
            this.Property(t => t.FILUAR).HasColumnName("FILUAR");
            this.Property(t => t.ARMUAR).HasColumnName("ARMUAR");
            this.Property(t => t.LOCUAR).HasColumnName("LOCUAR");
            this.Property(t => t.ENDUAR).HasColumnName("ENDUAR");
            this.Property(t => t.SITUAR).HasColumnName("SITUAR");
            this.Property(t => t.SITENT).HasColumnName("SITENT");
            this.Property(t => t.SITSAI).HasColumnName("SITSAI");
            this.Property(t => t.UARGEN).HasColumnName("UARGEN");
            this.Property(t => t.DATGER).HasColumnName("DATGER");
            this.Property(t => t.USUGER).HasColumnName("USUGER");
            this.Property(t => t.DATULT).HasColumnName("DATULT");
            this.Property(t => t.USUULT).HasColumnName("USUULT");
            this.Property(t => t.CODCLI).HasColumnName("CODCLI");

            // Relationships
            this.HasRequired(t => t.N0103TUA)
                .WithMany(t => t.N0109UAR)
                .HasForeignKey(d => new { d.CODEMP, d.CODTUA });
            this.HasRequired(t => t.N0106END)
                .WithMany(t => t.N0109UAR)
                .HasForeignKey(d => new { d.CODEMP, d.FILUAR, d.ARMUAR, d.LOCUAR, d.ENDUAR });

        }
    }
}
