using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0103TUAMap : EntityTypeConfiguration<Model.N0103TUA>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0103TUA, utilizada para difinir padrões de BD;
        /// </summary>
        public N0103TUAMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODTUA });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODTUA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESTUA)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SITTUA)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.OBRTUA)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0103TUA", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODTUA).HasColumnName("CODTUA");
            this.Property(t => t.DESTUA).HasColumnName("DESTUA");
            this.Property(t => t.IMGTUA).HasColumnName("IMGTUA");
            this.Property(t => t.LARTUA).HasColumnName("LARTUA");
            this.Property(t => t.COMTUA).HasColumnName("COMTUA");
            this.Property(t => t.ALTTUA).HasColumnName("ALTTUA");
            this.Property(t => t.CUBMAX).HasColumnName("CUBMAX");
            this.Property(t => t.DIAMAX).HasColumnName("DIAMAX");
            this.Property(t => t.PESMAX).HasColumnName("PESMAX");
            this.Property(t => t.SITTUA).HasColumnName("SITTUA");
            this.Property(t => t.OBRTUA).HasColumnName("OBRTUA");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0103TUA)
                .HasForeignKey(d => d.CODEMP);

        }
    }
}
