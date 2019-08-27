using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0110ITDMap : EntityTypeConfiguration<Model.N0110ITD>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0110ITD, utilizada para difinir padrões de BD;
        /// </summary>
        public N0110ITDMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.NUMROM, t.NUMDOC, t.SEQITD });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NUMROM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NUMDOC)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEQITD)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODTNS)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODPRO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODDER)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UNIMED)
                .IsRequired()
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0110ITD", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.NUMROM).HasColumnName("NUMROM");
            this.Property(t => t.NUMDOC).HasColumnName("NUMDOC");
            this.Property(t => t.SEQITD).HasColumnName("SEQITD");
            this.Property(t => t.CODTNS).HasColumnName("CODTNS");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");
            this.Property(t => t.QTDPED).HasColumnName("QTDPED");
            this.Property(t => t.PREUNI).HasColumnName("PREUNI");
            this.Property(t => t.SITITD).HasColumnName("SITITD");
            this.Property(t => t.QTDSEP).HasColumnName("QTDSEP");
            this.Property(t => t.CODMOT).HasColumnName("CODMOT");
            this.Property(t => t.QTDCAN).HasColumnName("QTDCAN");
            this.Property(t => t.FILPFA).HasColumnName("FILPFA");
            this.Property(t => t.NUMANE).HasColumnName("NUMANE");
            this.Property(t => t.NUMPFA).HasColumnName("NUMPFA");
            this.Property(t => t.SEQPES).HasColumnName("SEQPES");
            this.Property(t => t.NUMPED).HasColumnName("NUMPED");
            this.Property(t => t.VLRPES).HasColumnName("VLRPES");

            // Relationships
            this.HasRequired(t => t.N0005TNS)
                .WithMany(t => t.N0110ITD)
                .HasForeignKey(d => new { d.CODEMP, d.CODTNS });
            this.HasRequired(t => t.N0006DER)
                .WithMany(t => t.N0110ITD)
                .HasForeignKey(d => new { d.CODEMP, d.CODPRO, d.CODDER });
            this.HasRequired(t => t.N0006PRO)
                .WithMany(t => t.N0110ITD)
                .HasForeignKey(d => new { d.CODEMP, d.CODPRO });
            this.HasRequired(t => t.N0007UNI)
                .WithMany(t => t.N0110ITD)
                .HasForeignKey(d => d.UNIMED);
            this.HasOptional(t => t.N0013MCP)
                .WithMany(t => t.N0110ITD)
                .HasForeignKey(d => d.CODMOT);
            this.HasRequired(t => t.N0110DOC)
                .WithMany(t => t.N0110ITD)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL, d.NUMROM, d.NUMDOC });

        }
    }
}
