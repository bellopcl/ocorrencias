using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0003ARMMap : EntityTypeConfiguration<Model.N0003ARM>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0003ARM, utilizada para difinir padrões de BD;
        /// </summary>
        public N0003ARMMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.CODARM });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODARM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESARM)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MOVEST)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDBOP)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DEPERP)
                .HasMaxLength(50);

            this.Property(t => t.SITARM)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.LIMPIC)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDEND)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDMPK)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TNSEXT)
                .HasMaxLength(10);

            this.Property(t => t.TNSEIN)
                .HasMaxLength(10);

            this.Property(t => t.TNSSIN)
                .HasMaxLength(10);

            this.Property(t => t.TNSTRF)
                .HasMaxLength(50);

            this.Property(t => t.INDBLC)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDMDC)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDBEP)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GERLOG)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ARQLOG)
                .HasMaxLength(150);

            this.Property(t => t.INDBLK)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TNSEND)
                .HasMaxLength(10);

            this.Property(t => t.TNSIUA)
                .HasMaxLength(10);

            this.Property(t => t.TNSIUE)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.INDBAS)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDBAR)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDINV)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DEPDEV)
                .HasMaxLength(50);

            this.Property(t => t.INDPRO)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TNSBPR)
                .HasMaxLength(50);

            this.Property(t => t.TNSBPT)
                .HasMaxLength(50);

            this.Property(t => t.INDPCC)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDPRV)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDBLP)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDTFD)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDTRD)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TNSTRI)
                .HasMaxLength(50);

            this.Property(t => t.INDQTD)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ENTAID)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDPIC)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.INDFOR)
                .IsFixedLength()
                .HasMaxLength(1);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
            // Table & Column Mappings
            this.ToTable("N0003ARM", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.CODARM).HasColumnName("CODARM");
            this.Property(t => t.DESARM).HasColumnName("DESARM");
            this.Property(t => t.MOVEST).HasColumnName("MOVEST");
            this.Property(t => t.INDBOP).HasColumnName("INDBOP");
            this.Property(t => t.DEPERP).HasColumnName("DEPERP");
            this.Property(t => t.EMAREQ).HasColumnName("EMAREQ");
            this.Property(t => t.SITARM).HasColumnName("SITARM");
            this.Property(t => t.ARMENT).HasColumnName("ARMENT");
            this.Property(t => t.LOCENT).HasColumnName("LOCENT");
            this.Property(t => t.ENDENT).HasColumnName("ENDENT");
            this.Property(t => t.LIMPIC).HasColumnName("LIMPIC");
            this.Property(t => t.INDEND).HasColumnName("INDEND");
            this.Property(t => t.TMPACE).HasColumnName("TMPACE");
            this.Property(t => t.INDMPK).HasColumnName("INDMPK");
            this.Property(t => t.TNSEXT).HasColumnName("TNSEXT");
            this.Property(t => t.TNSEIN).HasColumnName("TNSEIN");
            this.Property(t => t.TNSSIN).HasColumnName("TNSSIN");
            this.Property(t => t.TNSTRF).HasColumnName("TNSTRF");
            this.Property(t => t.INDBLC).HasColumnName("INDBLC");
            this.Property(t => t.INDMDC).HasColumnName("INDMDC");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.INDBEP).HasColumnName("INDBEP");
            this.Property(t => t.GERLOG).HasColumnName("GERLOG");
            this.Property(t => t.ARQLOG).HasColumnName("ARQLOG");
            this.Property(t => t.INDBLK).HasColumnName("INDBLK");
            this.Property(t => t.TNSEND).HasColumnName("TNSEND");
            this.Property(t => t.TNSIUA).HasColumnName("TNSIUA");
            this.Property(t => t.TNSIUE).HasColumnName("TNSIUE");
            this.Property(t => t.INDBAS).HasColumnName("INDBAS");
            this.Property(t => t.INDBAR).HasColumnName("INDBAR");
            this.Property(t => t.INDINV).HasColumnName("INDINV");
            this.Property(t => t.DEPDEV).HasColumnName("DEPDEV");
            this.Property(t => t.INDPRO).HasColumnName("INDPRO");
            this.Property(t => t.TNSBPR).HasColumnName("TNSBPR");
            this.Property(t => t.TNSBPT).HasColumnName("TNSBPT");
            this.Property(t => t.INDPCC).HasColumnName("INDPCC");
            this.Property(t => t.INDPRV).HasColumnName("INDPRV");
            this.Property(t => t.INDBLP).HasColumnName("INDBLP");
            this.Property(t => t.INDTFD).HasColumnName("INDTFD");
            this.Property(t => t.INDTRD).HasColumnName("INDTRD");
            this.Property(t => t.TNSTRI).HasColumnName("TNSTRI");
            this.Property(t => t.INDQTD).HasColumnName("INDQTD");
            this.Property(t => t.ENTAID).HasColumnName("ENTAID");
            this.Property(t => t.INDPIC).HasColumnName("INDPIC");
            this.Property(t => t.INDFOR).HasColumnName("INDFOR");

            // Relationships
            this.HasRequired(t => t.N0002FIL)
                .WithMany(t => t.N0003ARM)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL });

        }
    }
}
