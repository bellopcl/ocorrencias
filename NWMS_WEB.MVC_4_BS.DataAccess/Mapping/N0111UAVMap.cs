using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0111UAVMap : EntityTypeConfiguration<Model.N0111UAV>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0111UAV, utilizada para difinir padrões de BD;
        /// </summary>
        public N0111UAVMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.CODARM, t.CODINV, t.SEQINV });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODARM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODINV)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQINV)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ETQEND)
                .HasMaxLength(50);

            this.Property(t => t.SITREG)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0111UAV", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.CODARM).HasColumnName("CODARM");
            this.Property(t => t.CODINV).HasColumnName("CODINV");
            this.Property(t => t.SEQINV).HasColumnName("SEQINV");
            this.Property(t => t.CODLOC).HasColumnName("CODLOC");
            this.Property(t => t.CODEND).HasColumnName("CODEND");
            this.Property(t => t.ETQEND).HasColumnName("ETQEND");
            this.Property(t => t.CODUAR).HasColumnName("CODUAR");
            this.Property(t => t.CODTUA).HasColumnName("CODTUA");
            this.Property(t => t.DATULT).HasColumnName("DATULT");
            this.Property(t => t.TUANOV).HasColumnName("TUANOV");
            this.Property(t => t.SITREG).HasColumnName("SITREG");

            // Relationships
            this.HasOptional(t => t.N0103TUA)
                .WithMany(t => t.N0111UAV)
                .HasForeignKey(d => new { d.CODEMP, d.CODTUA });
            this.HasRequired(t => t.N0111INV)
                .WithMany(t => t.N0111UAV)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL, d.CODARM, d.CODINV });

        }
    }
}
