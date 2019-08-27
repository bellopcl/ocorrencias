using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0000AGEMap : EntityTypeConfiguration<Model.N0000AGE>
    {
        /// <summary>
        /// Classe de mapeamento da tabela N0000AGE, utilizada para definir padrões de tabela
        /// </summary>
        public N0000AGEMap()
        {
            // Primary Key
            this.HasKey(t => t.CODAGE);

            // Properties
            this.Property(t => t.CODAGE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESAGE)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TIPAGE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SITAGE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PERAGE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IDNREL)
                .HasMaxLength(50);

            this.Property(t => t.FILREL)
                .HasMaxLength(250);

            this.Property(t => t.URLREL)
                .HasMaxLength(250);

            this.Property(t => t.EMADES)
                .HasMaxLength(250);

            this.Property(t => t.EMACCC)
                .HasMaxLength(250);

            this.Property(t => t.EMACCU)
                .HasMaxLength(250);

            this.Property(t => t.TITEMA)
                .HasMaxLength(250);

            this.Property(t => t.EMAORI)
                .HasMaxLength(50);

            this.Property(t => t.SENORI)
                .HasMaxLength(50);

            this.Property(t => t.SERORI)
                .HasMaxLength(50);

            this.Property(t => t.DIAUTI)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EMAFOR)
                .IsFixedLength()
                .HasMaxLength(1);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("N0000AGE", connectionString);
            this.Property(t => t.CODAGE).HasColumnName("CODAGE");
            this.Property(t => t.DESAGE).HasColumnName("DESAGE");
            this.Property(t => t.TIPAGE).HasColumnName("TIPAGE");
            this.Property(t => t.SITAGE).HasColumnName("SITAGE");
            this.Property(t => t.PERAGE).HasColumnName("PERAGE");
            this.Property(t => t.DATAGE).HasColumnName("DATAGE");
            this.Property(t => t.DATPRX).HasColumnName("DATPRX");
            this.Property(t => t.COMSQL).HasColumnName("COMSQL");
            this.Property(t => t.IDNREL).HasColumnName("IDNREL");
            this.Property(t => t.FILREL).HasColumnName("FILREL");
            this.Property(t => t.URLREL).HasColumnName("URLREL");
            this.Property(t => t.EMADES).HasColumnName("EMADES");
            this.Property(t => t.EMACCC).HasColumnName("EMACCC");
            this.Property(t => t.EMACCU).HasColumnName("EMACCU");
            this.Property(t => t.TITEMA).HasColumnName("TITEMA");
            this.Property(t => t.COREMA).HasColumnName("COREMA");
            this.Property(t => t.EMAORI).HasColumnName("EMAORI");
            this.Property(t => t.SENORI).HasColumnName("SENORI");
            this.Property(t => t.SERORI).HasColumnName("SERORI");
            this.Property(t => t.DIAUTI).HasColumnName("DIAUTI");
            this.Property(t => t.SQLVAL).HasColumnName("SQLVAL");
            this.Property(t => t.EMAFOR).HasColumnName("EMAFOR");
        }
    }
}
