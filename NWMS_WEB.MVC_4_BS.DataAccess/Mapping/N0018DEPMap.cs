using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0018DEPMap : EntityTypeConfiguration<Model.N0018DEP>
    {
        /// <summary>
        /// Classe de mapeamento da classe N0018DEP, utilizada para difinir padrões de BD;
        /// </summary>
        public N0018DEPMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CODEMP, t.CODDEP });

            // Properties
            this.Property(t => t.CODEMP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CODDEP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DESDEP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ABRDEP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TIPDEP)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITDEP)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0018DEP", connectionString);
            this.Property(t => t.CODEMP).HasColumnName("CODEMP");
            this.Property(t => t.CODDEP).HasColumnName("CODDEP");
            this.Property(t => t.DESDEP).HasColumnName("DESDEP");
            this.Property(t => t.ABRDEP).HasColumnName("ABRDEP");
            this.Property(t => t.TIPDEP).HasColumnName("TIPDEP");
            this.Property(t => t.CODFIL).HasColumnName("CODFIL");
            this.Property(t => t.SITDEP).HasColumnName("SITDEP");
            this.Property(t => t.DATCAD).HasColumnName("DATCAD");
            this.Property(t => t.USUCAD).HasColumnName("USUCAD");
            this.Property(t => t.DATALT).HasColumnName("DATALT");
            this.Property(t => t.USUALT).HasColumnName("USUALT");

            // Relationships
            this.HasRequired(t => t.N0001EMP)
                .WithMany(t => t.N0018DEP)
                .HasForeignKey(d => d.CODEMP);

        }
    }
}
