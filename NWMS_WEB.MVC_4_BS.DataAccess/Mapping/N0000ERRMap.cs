using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration; using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class N0000ERRMap : EntityTypeConfiguration<Model.N0000ERR>
    {
        /// <summary>
        /// Classe de mapeamento da tabela N0000ERR, utilizada para definir padrões de criação de tabela
        /// </summary>
        public N0000ERRMap()
        {
            // Primary Key
            this.HasKey(t => t.CODERR);

            // Properties
            this.Property(t => t.CODERR)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DESSIS)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TELSIS)
                .HasMaxLength(50);

            this.Property(t => t.CRIERR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.OBSUSU)
                .HasMaxLength(250);

            this.Property(t => t.SITANA)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.VERSIS)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TITMSG)
                .HasMaxLength(900);

                        string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);
// Table & Column Mappings
            this.ToTable("N0000ERR", connectionString);
            this.Property(t => t.CODERR).HasColumnName("CODERR");
            this.Property(t => t.DESSIS).HasColumnName("DESSIS");
            this.Property(t => t.TELSIS).HasColumnName("TELSIS");
            this.Property(t => t.CRIERR).HasColumnName("CRIERR");
            this.Property(t => t.OBSUSU).HasColumnName("OBSUSU");
            this.Property(t => t.SITANA).HasColumnName("SITANA");
            this.Property(t => t.DATGER).HasColumnName("DATGER");
            this.Property(t => t.USUGER).HasColumnName("USUGER");
            this.Property(t => t.DATPAR).HasColumnName("DATPAR");
            this.Property(t => t.USUPAR).HasColumnName("USUPAR");
            this.Property(t => t.VERSIS).HasColumnName("VERSIS");
            this.Property(t => t.COMSQL).HasColumnName("COMSQL");
            this.Property(t => t.IMGERR).HasColumnName("IMGERR");
            this.Property(t => t.TITMSG).HasColumnName("TITMSG");
            this.Property(t => t.MSGERR).HasColumnName("MSGERR");
        }
    }
}
