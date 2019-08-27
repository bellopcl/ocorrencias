using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0109HUAMap : EntityTypeConfiguration<Model.N0109HUA>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0109HUA, utilizada para difinir padrões de BD;
        /// </summary>
        public N0109HUAMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODUAR, t.SEQHUA });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODUAR)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQHUA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OBSMOV)
                .HasMaxLength(450);

            this.Property(t => t.CODTNS)
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0109HUA", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODUAR).HasColumnName("CODUAR");
            this.Property(t => t.SEQHUA).HasColumnName("SEQHUA");
            this.Property(t => t.FILORI).HasColumnName("FILORI");
            this.Property(t => t.ARMORI).HasColumnName("ARMORI");
            this.Property(t => t.LOCORI).HasColumnName("LOCORI");
            this.Property(t => t.ENDORI).HasColumnName("ENDORI");
            this.Property(t => t.FILDES).HasColumnName("FILDES");
            this.Property(t => t.ARMDES).HasColumnName("ARMDES");
            this.Property(t => t.LOCDES).HasColumnName("LOCDES");
            this.Property(t => t.ENDDES).HasColumnName("ENDDES");
            this.Property(t => t.OBSMOV).HasColumnName("OBSMOV");
            this.Property(t => t.CODTNS).HasColumnName("CODTNS");
            this.Property(t => t.DATHUA).HasColumnName("DATHUA");
            this.Property(t => t.USUHUA).HasColumnName("USUHUA");

            // Relationships
            this.HasOptional(t => t.N0005TNS)
                .WithMany(t => t.N0109HUA)
                .HasForeignKey(d => new { d.CODEMP, d.CODTNS });
            this.HasRequired(t => t.N0106END)
                .WithMany(t => t.N0109HUA)
                .HasForeignKey(d => new { d.CODEMP, d.FILDES, d.ARMDES, d.LOCDES, d.ENDDES });
            this.HasOptional(t => t.N0106END1)
                .WithMany(t => t.N0109HUA1)
                .HasForeignKey(d => new { d.CODEMP, d.FILORI, d.ARMORI, d.LOCORI, d.ENDORI });
            this.HasRequired(t => t.N0109UAR)
                .WithMany(t => t.N0109HUA)
                .HasForeignKey(d => new { d.CODEMP, d.CODUAR });

        }
    }
}
