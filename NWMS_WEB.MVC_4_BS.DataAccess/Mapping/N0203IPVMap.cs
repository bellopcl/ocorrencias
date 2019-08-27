using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0203IPVMap : EntityTypeConfiguration<Model.N0203IPV>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0203IPV, utilizada para difinir padrões de BD;
        /// </summary>
        public N0203IPVMap()
        {
            // Primary Key
            this.HasKey(t => new { t.NUMREG, t.CODEMP, t.CODFIL, t.CODSNF, t.NUMNFV, t.SEQIPV });

            // Properties
            this.Property(t => t.NUMREG)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODSNF)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NUMNFV)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQIPV)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODPRO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODDER)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CPLIPV)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.CODDEP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TNSPRO)
                .IsRequired()
                .HasMaxLength(10);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N0203IPV", connectionString);
            this.Property(t => t.NUMREG).HasColumnName("NUMREG");
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.CODSNF).HasColumnName("CODSNF");
            this.Property(t => t.NUMNFV).HasColumnName("NUMNFV");
            this.Property(t => t.SEQIPV).HasColumnName("SEQIPV");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.CPLIPV).HasColumnName("CPLIPV");
            this.Property(t => t.CODDEP).HasColumnName("CODDEP");
            this.Property(t => t.QTDFAT).HasColumnName("QTDFAT");
            this.Property(t => t.PREUNI).HasColumnName("PREUNI");
            this.Property(t => t.VLRLIQ).HasColumnName("VLRLIQ");
            this.Property(t => t.ORIOCO).HasColumnName("ORIOCO");
            this.Property(t => t.CODMOT).HasColumnName("CODMOT");
            this.Property(t => t.QTDDEV).HasColumnName("QTDDEV");
            this.Property(t => t.TNSPRO).HasColumnName("TNSPRO");
            this.Property(t => t.USUULT).HasColumnName("USUULT");
            this.Property(t => t.DATULT).HasColumnName("DATULT");
            this.Property(t => t.PEROFE).HasColumnName("PEROFE");
            this.Property(t => t.PERIPI).HasColumnName("PERIPI");
            this.Property(t => t.VLRIPI).HasColumnName("VLRIPI");
            this.Property(t => t.NUMANE).HasColumnName("NUMANE");
            this.Property(t => t.NUMANE_REL).HasColumnName("NUMANE_REL");
            this.Property(t => t.DATEMI).HasColumnName("DATEMI");
            this.Property(t => t.VLRST).HasColumnName("VLRST");

            // Relationships
            this.HasRequired(t => t.N0005TNS)
                .WithMany(t => t.N0203IPV)
                .HasForeignKey(d => new { d.CODEMP, d.TNSPRO });
            this.HasRequired(t => t.N0006DER)
                .WithMany(t => t.N0203IPV)
                .HasForeignKey(d => new { d.CODEMP, d.CODPRO, d.CODDER });
            this.HasRequired(t => t.N0203REG)
                .WithMany(t => t.N0203IPV)
                .HasForeignKey(d => d.NUMREG);
        }
    }
}
