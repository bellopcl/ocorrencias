using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0006PROMap : EntityTypeConfiguration<Model.N0006PROModel>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0006PROModel, utilizada para difinir padrões de BD;
        /// </summary>
        public N0006PROMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODPRO });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODPRO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DESPRO)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.CODORI)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.CODFAM)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.UNIMED)
                .HasMaxLength(50);

            this.Property(t => t.TIPPRO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CODAGP)
                .HasMaxLength(5);

            this.Property(t => t.CODAGE)
                .HasMaxLength(5);

            this.Property(t => t.DEPPRD)
                .HasMaxLength(10);

            this.Property(t => t.INDMIS)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDORP)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EXPPAL)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.FORLIN)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITPRO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BLOINV)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDRET)
                .IsRequired()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0006PRO", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODPRO).HasColumnName("CODPRO");
            this.Property(t => t.DESPRO).HasColumnName("DESPRO");
            this.Property(t => t.CODORI).HasColumnName("CODORI");
            this.Property(t => t.CODFAM).HasColumnName("CODFAM");
            this.Property(t => t.UNIMED).HasColumnName("UNIMED");
            this.Property(t => t.TIPPRO).HasColumnName("TIPPRO");
            this.Property(t => t.CODAGP).HasColumnName("CODAGP");
            this.Property(t => t.CODAGE).HasColumnName("CODAGE");
            this.Property(t => t.DEPPRD).HasColumnName("DEPPRD");
            this.Property(t => t.INDMIS).HasColumnName("INDMIS");
            this.Property(t => t.INDORP).HasColumnName("INDORP");
            this.Property(t => t.EXPPAL).HasColumnName("EXPPAL");
            this.Property(t => t.FORLIN).HasColumnName("FORLIN");
            this.Property(t => t.SITPRO).HasColumnName("SITPRO");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.BLOINV).HasColumnName("BLOINV");
            this.Property(t => t.INDRET).HasColumnName("INDRET");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0006PRO)
                .HasForeignKey(d => d.CODEMP);
            this.HasRequired(t => t.N0006FAM)
                .WithMany(t => t.N0006PRO)
                .HasForeignKey(d => new { d.CODEMP, d.CODFAM });
            this.HasRequired(t => t.N0006ORI)
                .WithMany(t => t.N0006PRO)
                .HasForeignKey(d => new { d.CODEMP, d.CODORI });
            this.HasOptional(t => t.N0007UNI)
                .WithMany(t => t.N0006PRO)
                .HasForeignKey(d => d.UNIMED);

        }
    }
}
