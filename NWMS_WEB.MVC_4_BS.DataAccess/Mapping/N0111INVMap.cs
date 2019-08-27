using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0111INVMap : EntityTypeConfiguration<Model.N0111INVModel>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0111INVModel, utilizada para difinir padrões de BD;
        /// </summary>
        public N0111INVMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.CODARM, t.CODINV });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODARM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODINV)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SITINV)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.MODINV)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TIPCON)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.OBRCTP)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.OBRALL)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDAID)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TIPINV)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0111INV", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.CODARM).HasColumnName("CODARM");
            this.Property(t => t.CODINV).HasColumnName("CODINV");
            this.Property(t => t.DATINI).HasColumnName("DATINI");
            this.Property(t => t.DATFIM).HasColumnName("DATFIM");
            this.Property(t => t.SITINV).HasColumnName("SITINV");
            this.Property(t => t.MODINV).HasColumnName("MODINV");
            this.Property(t => t.TIPCON).HasColumnName("TIPCON");
            this.Property(t => t.QTDCON).HasColumnName("QTDCON");
            this.Property(t => t.QTDEMB).HasColumnName("QTDEMB");
            this.Property(t => t.QTDUNI).HasColumnName("QTDUNI");
            this.Property(t => t.QTDPAL).HasColumnName("QTDPAL");
            this.Property(t => t.ESTVEN).HasColumnName("ESTVEN");
            this.Property(t => t.ESTCUS).HasColumnName("ESTCUS");
            this.Property(t => t.DATGER).HasColumnName("DATGER");
            this.Property(t => t.USUGER).HasColumnName("USUGER");
            this.Property(t => t.EMBATU).HasColumnName("EMBATU");
            this.Property(t => t.UNIATU).HasColumnName("UNIATU");
            this.Property(t => t.PALATU).HasColumnName("PALATU");
            this.Property(t => t.VENATU).HasColumnName("VENATU");
            this.Property(t => t.VLRCUS).HasColumnName("VLRCUS");
            this.Property(t => t.SEQCON).HasColumnName("SEQCON");
            this.Property(t => t.OBRCTP).HasColumnName("OBRCTP");
            this.Property(t => t.OBRALL).HasColumnName("OBRALL");
            this.Property(t => t.CODTPE).HasColumnName("CODTPE");
            this.Property(t => t.INDAID).HasColumnName("INDAID");
            this.Property(t => t.DATINV).HasColumnName("DATINV");
            this.Property(t => t.TIPINV).HasColumnName("TIPINV");

            // Relationships
            this.HasRequired(t => t.N0003ARM)
                .WithMany(t => t.N0111INV)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL, d.CODARM });
            this.HasRequired(t => t.SYS_USUARIO)
                .WithMany(t => t.N0111INV)
                .HasForeignKey(d => d.USUGER);

        }
    }
}
