using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0109MOVMap : EntityTypeConfiguration<Model.N0109MOV>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0109MOV, utilizada para difinir padrões de BD;
        /// </summary>
        public N0109MOVMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.CODARM, t.CODPRO, t.CODDER, t.UNIMED, t.DATMOV, t.SEQMOV });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODARM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODPRO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODDER)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UNIMED)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.SEQMOV)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODTNS)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.CODROE)
                .HasMaxLength(50);

            this.Property(t => t.OBSMOV)
                .HasMaxLength(400);

            this.Property(t => t.ESTMOV)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0109MOV", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.CODARM).HasColumnName("CODARM");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.CODDER).HasColumnName("CODDER");
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");
            this.Property(t => t.DATMOV).HasColumnName("DATMOV");
            this.Property(t => t.SEQMOV).HasColumnName("SEQMOV");
            this.Property(t => t.CODTNS).HasColumnName("CODTNS");
            this.Property(t => t.CODUAR).HasColumnName("CODUAR");
            this.Property(t => t.SEQUAR).HasColumnName("SEQUAR");
            this.Property(t => t.SEQLOT).HasColumnName("SEQLOT");
            this.Property(t => t.LOCORI).HasColumnName("LOCORI");
            this.Property(t => t.ENDORI).HasColumnName("ENDORI");
            this.Property(t => t.FILDES).HasColumnName("FILDES");
            this.Property(t => t.ARMDES).HasColumnName("ARMDES");
            this.Property(t => t.LOCDES).HasColumnName("LOCDES");
            this.Property(t => t.ENDDES).HasColumnName("ENDDES");
            this.Property(t => t.QTDEST).HasColumnName("QTDEST");
            this.Property(t => t.QTDMOV).HasColumnName("QTDMOV");
            this.Property(t => t.NUMROM).HasColumnName("NUMROM");
            this.Property(t => t.DOCROM).HasColumnName("DOCROM");
            this.Property(t => t.ITPROM).HasColumnName("ITPROM");
            this.Property(t => t.CODROE).HasColumnName("CODROE");
            this.Property(t => t.SEQROE).HasColumnName("SEQROE");
            this.Property(t => t.HORMOV).HasColumnName("HORMOV");
            this.Property(t => t.USUMOV).HasColumnName("USUMOV");
            this.Property(t => t.VLRMOV).HasColumnName("VLRMOV");
            this.Property(t => t.PESMOV).HasColumnName("PESMOV");
            this.Property(t => t.CUBMOV).HasColumnName("CUBMOV");
            this.Property(t => t.CODTAR).HasColumnName("CODTAR");
            this.Property(t => t.CODCLI).HasColumnName("CODCLI");
            this.Property(t => t.OBSMOV).HasColumnName("OBSMOV");
            this.Property(t => t.LIQMOV).HasColumnName("LIQMOV");
            this.Property(t => t.ESTMOV).HasColumnName("ESTMOV");
            this.Property(t => t.USUINT).HasColumnName("USUINT");
            this.Property(t => t.DATINT).HasColumnName("DATINT");
            this.Property(t => t.HORINT).HasColumnName("HORINT");
            this.Property(t => t.CODLIG).HasColumnName("CODLIG");
            this.Property(t => t.SEQINT).HasColumnName("SEQINT");

            // Relationships
            this.HasRequired(t => t.N0003ARM)
                .WithMany(t => t.N0109MOV)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL, d.CODARM });
            this.HasRequired(t => t.N0005TNS)
                .WithMany(t => t.N0109MOV)
                .HasForeignKey(d => new { d.CODEMP, d.CODTNS });
            this.HasRequired(t => t.N0006DER)
                .WithMany(t => t.N0109MOV)
                .HasForeignKey(d => new { d.CODEMP, d.CODPRO, d.CODDER });
            this.HasRequired(t => t.N0007UNI)
                .WithMany(t => t.N0109MOV)
                .HasForeignKey(d => d.UNIMED);
            this.HasOptional(t => t.N0109LOT)
                .WithMany(t => t.N0109MOV)
                .HasForeignKey(d => new { d.CODEMP, d.CODUAR, d.SEQUAR, d.SEQLOT });

        }
    }
}
