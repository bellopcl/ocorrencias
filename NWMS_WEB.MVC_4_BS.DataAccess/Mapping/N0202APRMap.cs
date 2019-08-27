using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0202APRMap : EntityTypeConfiguration<Model.N0202APR>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0202APR, utilizada para difinir padrões de BD;
        /// </summary>
        public N0202APRMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SEQAPR, t.CODEMP, t.NUMREQ });

            // Properties
            this.Property(t => t.SEQAPR)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NUMREQ)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESNIV)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OBSAPR)
                .HasMaxLength(500);

            this.Property(t => t.SITAPR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.VIAAPR)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0202APR", connectionString);
            this.Property(t => t.SEQAPR).HasColumnName("SEQAPR");
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.NUMREQ).HasColumnName("NUMREQ");
            this.Property(t => t.CODROT).HasColumnName("CODROT");
            this.Property(t => t.NIVAPR).HasColumnName("NIVAPR");
            this.Property(t => t.DESNIV).HasColumnName("DESNIV");
            this.Property(t => t.USUAPR).HasColumnName("USUAPR");
            this.Property(t => t.DATAPR).HasColumnName("DATAPR");
            this.Property(t => t.OBSAPR).HasColumnName("OBSAPR");
            this.Property(t => t.USUREJ).HasColumnName("USUREJ");
            this.Property(t => t.DATREJ).HasColumnName("DATREJ");
            this.Property(t => t.SITAPR).HasColumnName("SITAPR");
            this.Property(t => t.CODMOT).HasColumnName("CODMOT");
            this.Property(t => t.VIAAPR).HasColumnName("VIAAPR");

            // Relationships
            this.HasOptional(t => t.N0202MOT)
                .WithMany(t => t.N0202APR)
                .HasForeignKey(d => d.CODMOT);
            this.HasRequired(t => t.N0202REQ)
                .WithMany(t => t.N0202APR)
                .HasForeignKey(d => new { d.NUMREQ, d.CODEMP });
            this.HasOptional(t => t.SYS_USUARIO)
                .WithMany(t => t.N0202APR)
                .HasForeignKey(d => d.USUAPR);
            this.HasOptional(t => t.SYS_USUARIO1)
                .WithMany(t => t.N0202APR1)
                .HasForeignKey(d => d.USUREJ);

        }
    }
}
