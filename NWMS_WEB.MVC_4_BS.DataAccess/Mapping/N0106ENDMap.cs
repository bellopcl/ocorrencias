using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0106ENDMap : EntityTypeConfiguration<Model.N0106END>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0106END, utilizada para difinir padrões de BD;
        /// </summary>
        public N0106ENDMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODFIL, t.CODARM, t.CODLOC, t.CODEND });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODFIL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODARM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODLOC)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODEND)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RUAEND)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ETQEND)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SITEND)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITSAI)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITENT)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITMOV)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ENDVIR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.MOVEST)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RECANE)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.COLEND)
                .HasMaxLength(50);

            this.Property(t => t.DIVEND)
                .HasMaxLength(50);

            this.Property(t => t.PROEND)
                .HasMaxLength(50);

            this.Property(t => t.ALCEND)
                .HasMaxLength(50);

            this.Property(t => t.PARIMP)
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0106END", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.CODARM).HasColumnName("CODARM");
            this.Property(t => t.CODLOC).HasColumnName("CODLOC");
            this.Property(t => t.CODEND).HasColumnName("CODEND");
            this.Property(t => t.RUAEND).HasColumnName("RUAEND");
            this.Property(t => t.ETQEND).HasColumnName("ETQEND");
            this.Property(t => t.CODTPE).HasColumnName("CODTPE");
            this.Property(t => t.CODESE).HasColumnName("CODESE");
            this.Property(t => t.SITEND).HasColumnName("SITEND");
            this.Property(t => t.SITSAI).HasColumnName("SITSAI");
            this.Property(t => t.SITENT).HasColumnName("SITENT");
            this.Property(t => t.SITMOV).HasColumnName("SITMOV");
            this.Property(t => t.ENDVIR).HasColumnName("ENDVIR");
            this.Property(t => t.PESMAX).HasColumnName("PESMAX");
            this.Property(t => t.ALTEND).HasColumnName("ALTEND");
            this.Property(t => t.COMEND).HasColumnName("COMEND");
            this.Property(t => t.LAREND).HasColumnName("LAREND");
            this.Property(t => t.CUBMAX).HasColumnName("CUBMAX");
            this.Property(t => t.PRILOC).HasColumnName("PRILOC");
            this.Property(t => t.MAXUAR).HasColumnName("MAXUAR");
            this.Property(t => t.DATULT).HasColumnName("DATULT");
            this.Property(t => t.MOVEST).HasColumnName("MOVEST");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUALT).HasColumnName("USUALT");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.RECANE).HasColumnName("RECANE");
            this.Property(t => t.COLEND).HasColumnName("COLEND");
            this.Property(t => t.DIVEND).HasColumnName("DIVEND");
            this.Property(t => t.PROEND).HasColumnName("PROEND");
            this.Property(t => t.ALCEND).HasColumnName("ALCEND");
            this.Property(t => t.PARIMP).HasColumnName("PARIMP");

            // Relationships
            this.HasRequired(t => t.N0101LOC)
                .WithMany(t => t.N0106END)
                .HasForeignKey(d => new { d.CODEMP, d.CODFIL, d.CODARM, d.CODLOC });
            this.HasRequired(t => t.N0102ESE)
                .WithMany(t => t.N0106END)
                .HasForeignKey(d => new { d.CODEMP, d.CODESE });
            this.HasRequired(t => t.N0104TPE)
                .WithMany(t => t.N0106END)
                .HasForeignKey(d => new { d.CODEMP, d.CODTPE });

        }
    }
}
