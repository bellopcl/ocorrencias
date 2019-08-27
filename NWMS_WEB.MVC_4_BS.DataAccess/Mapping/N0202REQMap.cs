using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0202REQMap : EntityTypeConfiguration<Model.N0202REQ>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0202REQ, utilizada para difinir padrões de BD;
        /// </summary>
        public N0202REQMap()
        {
            // Primary Key
            this.HasKey(t => new { t.NUMREQ, t.CODEMP });

            // Properties
            this.Property(t => t.NUMREQ)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODORI)
                .HasMaxLength(50);

            this.Property(t => t.CODFAM)
                .HasMaxLength(50);

            this.Property(t => t.CODAGE)
                .HasMaxLength(50);

            this.Property(t => t.DESPRO)
                .HasMaxLength(50);

            this.Property(t => t.UNIMED)
                .HasMaxLength(50);

            this.Property(t => t.CODCCU)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NUMREF)
                .HasMaxLength(50);

            this.Property(t => t.CODBEM)
                .HasMaxLength(50);

            this.Property(t => t.PROEQU)
                .HasMaxLength(50);

            this.Property(t => t.DEREQU)
                .HasMaxLength(50);

            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OBSREQ)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.TIPCAD)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DESDER)
                .HasMaxLength(50);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0202REQ", connectionString);
            this.Property(t => t.NUMREQ).HasColumnName("NUMREQ");
            this.Property(t => t.CODUSU).HasColumnName("CODUSU");
            this.Property(t => t.CODSIT).HasColumnName("CODSIT");
            this.Property(t => t.CODPER).HasColumnName("CODPER");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.CODORI).HasColumnName("CODORI");
            this.Property(t => t.CODFAM).HasColumnName("CODFAM");
            this.Property(t => t.CODAGE).HasColumnName("CODAGE");
            this.Property(t => t.DESPRO).HasColumnName("DESPRO");
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");
            this.Property(t => t.CODCCU).HasColumnName("CODCCU");
            this.Property(t => t.NUMREF).HasColumnName("NUMREF");
            this.Property(t => t.QTDCON).HasColumnName("QTDCON");
            this.Property(t => t.ANEPRO).HasColumnName("ANEPRO");
            this.Property(t => t.CODBEM).HasColumnName("CODBEM");
            this.Property(t => t.PROEQU).HasColumnName("PROEQU");
            this.Property(t => t.DEREQU).HasColumnName("DEREQU");
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.OBSREQ).HasColumnName("OBSREQ");
            this.Property(t => t.TIPCAD).HasColumnName("TIPCAD");
            this.Property(t => t.DESDER).HasColumnName("DESDER");
            this.Property(t => t.R_N0000ERR_CODERR).HasColumnName("R_N0000ERR_CODERR");

            // Relationships
            this.HasOptional(t => t.N0000ERR)
                .WithMany(t => t.N0202REQ)
                .HasForeignKey(d => d.R_N0000ERR_CODERR);
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0202REQ)
                .HasForeignKey(d => d.CODEMP);
            this.HasOptional(t => t.N0006FAM)
                .WithMany(t => t.N0202REQ)
                .HasForeignKey(d => new { d.CODEMP, d.CODFAM });
            this.HasOptional(t => t.N0006ORI)
                .WithMany(t => t.N0202REQ)
                .HasForeignKey(d => new { d.CODEMP, d.CODORI });
            this.HasOptional(t => t.N0006PRO)
                .WithMany(t => t.N0202REQ)
                .HasForeignKey(d => new { d.CODEMP, d.PROEQU });
            this.HasRequired(t => t.N0044CCU)
                .WithMany(t => t.N0202REQ)
                .HasForeignKey(d => new { d.CODEMP, d.CODCCU });
            this.HasOptional(t => t.N0200PER)
                .WithMany(t => t.N0202REQ)
                .HasForeignKey(d => d.CODPER);
            this.HasRequired(t => t.N0201SIT)
                .WithMany(t => t.N0202REQ)
                .HasForeignKey(d => d.CODSIT);
            this.HasRequired(t => t.SYS_USUARIO)
                .WithMany(t => t.N0202REQ)
                .HasForeignKey(d => d.CODUSU);

        }
    }
}
