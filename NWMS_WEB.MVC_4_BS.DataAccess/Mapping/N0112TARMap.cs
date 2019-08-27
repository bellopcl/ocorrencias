using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0112TARMap : EntityTypeConfiguration<Model.N0112TAR>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0112TAR, utilizada para difinir padrões de BD;
        /// </summary>
        public N0112TARMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.CODTAR });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ETQORI)
                .HasMaxLength(50);

            this.Property(t => t.ETQDES)
                .HasMaxLength(50);

            this.Property(t => t.CODROE)
                .HasMaxLength(50);

            this.Property(t => t.CODPRO)
                .HasMaxLength(50);

            this.Property(t => t.CODDER)
                .HasMaxLength(50);

            this.Property(t => t.UNIMED)
                .HasMaxLength(50);

            this.Property(t => t.SITTAR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CODTAR)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.REPTAR)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INTAID)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0112TAR", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.ARMORI).HasColumnName("ARMORI");
            this.Property(t => t.LOCORI).HasColumnName("LOCORI");
            this.Property(t => t.ENDORI).HasColumnName("ENDORI");
            this.Property(t => t.ETQORI).HasColumnName("ETQORI");
            this.Property(t => t.ARMDES).HasColumnName("ARMDES");
            this.Property(t => t.LOCDES).HasColumnName("LOCDES");
            this.Property(t => t.ENDDES).HasColumnName("ENDDES");
            this.Property(t => t.ETQDES).HasColumnName("ETQDES");
            this.Property(t => t.NUMROM).HasColumnName("NUMROM");
            this.Property(t => t.CODTPT).HasColumnName("CODTPT");
            this.Property(t => t.TEMEST).HasColumnName("TEMEST");
            this.Property(t => t.CODROE).HasColumnName("CODROE");
            this.Property(t => t.SEQROE).HasColumnName("SEQROE");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");
            this.Property(t => t.CODUAR).HasColumnName("CODUAR");
            this.Property(t => t.SEQIUA).HasColumnName("SEQIUA");
            this.Property(t => t.CODTUA).HasColumnName("CODTUA");
            this.Property(t => t.QTDMOV).HasColumnName("QTDMOV");
            this.Property(t => t.PRITAR).HasColumnName("PRITAR");
            this.Property(t => t.SITTAR).HasColumnName("SITTAR");
            this.Property(t => t.DATGER).HasColumnName("DATGER");
            this.Property(t => t.USUGER).HasColumnName("USUGER");
            this.Property(t => t.DATACE).HasColumnName("DATACE");
            this.Property(t => t.USUACE).HasColumnName("USUACE");
            this.Property(t => t.DATBAI).HasColumnName("DATBAI");
            this.Property(t => t.USUBAI).HasColumnName("USUBAI");
            this.Property(t => t.OBSTAR).HasColumnName("OBSTAR");
            this.Property(t => t.CODTAR).HasColumnName("CODTAR");
            this.Property(t => t.REPTAR).HasColumnName("REPTAR");
            this.Property(t => t.CODCLI).HasColumnName("CODCLI");
            this.Property(t => t.INTAID).HasColumnName("INTAID");
            this.Property(t => t.USUAID).HasColumnName("USUAID");
            this.Property(t => t.DATAID).HasColumnName("DATAID");
            this.Property(t => t.CODINV).HasColumnName("CODINV");
            this.Property(t => t.SEQINV).HasColumnName("SEQINV");
            this.Property(t => t.SEQLOT).HasColumnName("SEQLOT");
            this.Property(t => t.CONINV).HasColumnName("CONINV");
            this.Property(t => t.NUMAID).HasColumnName("NUMAID");

            // Relationships
            this.HasRequired(t => t.N0002FIL)
                .WithMany(t => t.N0112TAR)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL });
            this.HasOptional(t => t.N0007UNI)
                .WithMany(t => t.N0112TAR)
                .HasForeignKey(d => d.UNIMED);
            this.HasOptional(t => t.N0103TUA)
                .WithMany(t => t.N0112TAR)
                .HasForeignKey(d => new { d.CODEMP, d.CODTUA });
            this.HasOptional(t => t.N0109UAR)
                .WithMany(t => t.N0112TAR)
                .HasForeignKey(d => new { d.CODEMP, d.CODUAR });
            this.HasRequired(t => t.N0112TPT)
                .WithMany(t => t.N0112TAR)
                .HasForeignKey(d => new { d.CODEMP, d.CODTPT });

        }
    }
}
