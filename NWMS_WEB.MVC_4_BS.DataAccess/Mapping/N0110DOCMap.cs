using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0110DOCMap : EntityTypeConfiguration<Model.N0110DOC>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0110DOC, utilizada para difinir padrões de BD;
        /// </summary>
        public N0110DOCMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.NUMROM, t.NUMDOC });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NUMROM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NUMDOC)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODTNS)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODROE)
                .HasMaxLength(50);

            this.Property(t => t.OBSPFA)
                .HasMaxLength(2000);

            this.Property(t => t.PLAVEI)
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0110DOC", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.NUMROM).HasColumnName("NUMROM");
            this.Property(t => t.NUMDOC).HasColumnName("NUMDOC");
            this.Property(t => t.CODTNS).HasColumnName("CODTNS");
            this.Property(t => t.CODCLI).HasColumnName("CODCLI");
            this.Property(t => t.SEQENT).HasColumnName("SEQENT");
            this.Property(t => t.CODROE).HasColumnName("CODROE");
            this.Property(t => t.SEQROE).HasColumnName("SEQROE");
            this.Property(t => t.CODREP).HasColumnName("CODREP");
            this.Property(t => t.CODTRA).HasColumnName("CODTRA");
            this.Property(t => t.OBSPFA).HasColumnName("OBSPFA");
            this.Property(t => t.PESBRU).HasColumnName("PESBRU");
            this.Property(t => t.PESLIQ).HasColumnName("PESLIQ");
            this.Property(t => t.VLRPFA).HasColumnName("VLRPFA");
            this.Property(t => t.VOLPFA).HasColumnName("VOLPFA");
            this.Property(t => t.PLAVEI).HasColumnName("PLAVEI");
            this.Property(t => t.SITPFA).HasColumnName("SITPFA");
            this.Property(t => t.USUIMP).HasColumnName("USUIMP");
            this.Property(t => t.DATIMP).HasColumnName("DATIMP");
            this.Property(t => t.FILPFA).HasColumnName("FILPFA");
            this.Property(t => t.NUMANE).HasColumnName("NUMANE");
            this.Property(t => t.NUMPFA).HasColumnName("NUMPFA");
            this.Property(t => t.CODMOT).HasColumnName("CODMOT");
            this.Property(t => t.REPSUP).HasColumnName("REPSUP");

            // Relationships
            this.HasRequired(t => t.N0005TNS)
                .WithMany(t => t.N0110DOC)
                .HasForeignKey(d => new { d.CODEMP, d.CODTNS });
            this.HasOptional(t => t.N0012TRA)
                .WithMany(t => t.N0110DOC)
                .HasForeignKey(d => d.CODTRA);
            this.HasRequired(t => t.N0110ROM)
                .WithMany(t => t.N0110DOC)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL, d.NUMROM });

        }
    }
}
