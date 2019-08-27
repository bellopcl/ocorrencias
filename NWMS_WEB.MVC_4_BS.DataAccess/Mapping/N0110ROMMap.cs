using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0110ROMMap : EntityTypeConfiguration<Model.N0110ROM>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0110ROM, utilizada para difinir padrões de BD;
        /// </summary>
        public N0110ROMMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.NUMROM });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NUMROM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SITREG)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TIPROM)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITROM)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TIPAGR)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDPRO)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDENT)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ROMAGR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDBLK)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ESTROM)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LIMPIC)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0110ROM", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.NUMROM).HasColumnName("NUMROM");
            this.Property(t => t.SITREG).HasColumnName("SITREG");
            this.Property(t => t.FILREC).HasColumnName("FILREC");
            this.Property(t => t.ARMREC).HasColumnName("ARMREC");
            this.Property(t => t.LOCREC).HasColumnName("LOCREC");
            this.Property(t => t.ENDREC).HasColumnName("ENDREC");
            this.Property(t => t.PRIROM).HasColumnName("PRIROM");
            this.Property(t => t.DATINI).HasColumnName("DATINI");
            this.Property(t => t.DATFIM).HasColumnName("DATFIM");
            this.Property(t => t.FILANE).HasColumnName("FILANE");
            this.Property(t => t.NUMANE).HasColumnName("NUMANE");
            this.Property(t => t.FILREL).HasColumnName("FILREL");
            this.Property(t => t.NUMREL).HasColumnName("NUMREL");
            this.Property(t => t.TIPROM).HasColumnName("TIPROM");
            this.Property(t => t.USUIMP).HasColumnName("USUIMP");
            this.Property(t => t.DATIMP).HasColumnName("DATIMP");
            this.Property(t => t.SITROM).HasColumnName("SITROM");
            this.Property(t => t.TIPAGR).HasColumnName("TIPAGR");
            this.Property(t => t.INDPRO).HasColumnName("INDPRO");
            this.Property(t => t.INDENT).HasColumnName("INDENT");
            this.Property(t => t.ROMAGR).HasColumnName("ROMAGR");
            this.Property(t => t.INDBLK).HasColumnName("INDBLK");
            this.Property(t => t.ESTROM).HasColumnName("ESTROM");
            this.Property(t => t.LIMPIC).HasColumnName("LIMPIC");
            this.Property(t => t.NUMREG).HasColumnName("NUMREG");

            // Relationships
            this.HasRequired(t => t.N0002FIL)
                .WithMany(t => t.N0110ROM)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL });

        }
    }
}
