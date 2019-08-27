using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity.ModelConfiguration;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess.Mapping
{
    public class V_SALDONWMSMap : EntityTypeConfiguration<Model.V_SALDONWMS>
    {
        /// <summary>
        /// Classe de mapeamento da classe V_SALDONWMS, utilizada para difinir padrões de BD;
        /// </summary>
        public V_SALDONWMSMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Cód_Produto, t.Cód_Derivação, t.Origem, t.Família });

            // Properties
            this.Property(t => t.Cód_Produto)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Cód_Derivação)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Cód_Integrado)
                .HasMaxLength(100);

            this.Property(t => t.Desc__Produto)
                .HasMaxLength(201);

            this.Property(t => t.Origem)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.Família)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.Situação_Lig_Prod_x_Dep_)
                .IsFixedLength()
                .HasMaxLength(1);

            string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            connectionString = connectionString.Substring(connectionString.Length - 13, 13);

            // Table & Column Mappings
            this.ToTable("V_SALDONWMS", connectionString);
            this.Property(t => t.Cód_Produto).HasColumnName("Cód.Produto");
            this.Property(t => t.Cód_Derivação).HasColumnName("Cód.Derivação");
            this.Property(t => t.Cód_Integrado).HasColumnName("Cód.Integrado");
            this.Property(t => t.Desc__Produto).HasColumnName("Desc. Produto");
            this.Property(t => t.Origem).HasColumnName("Origem");
            this.Property(t => t.Família).HasColumnName("Família");
            this.Property(t => t.Saldo_Embalagens).HasColumnName("Saldo Embalagens");
            this.Property(t => t.Saldo_Unitário).HasColumnName("Saldo Unitário");
            this.Property(t => t.Qtde_Paletes_Ativos).HasColumnName("Qtde Paletes Ativos");
            this.Property(t => t.Capacidade__Paletes_).HasColumnName("Capacidade (Paletes)");
            this.Property(t => t.Saldo_Estoque_ERP).HasColumnName("Saldo Estoque ERP");
            this.Property(t => t.Estoque_Mínimo).HasColumnName("Estoque Mínimo");
            this.Property(t => t.Estoque_Máximo).HasColumnName("Estoque Máximo");
            this.Property(t => t.Estoque_para_Reposição).HasColumnName("Estoque para Reposição");
            this.Property(t => t.Situação_Lig_Prod_x_Dep_).HasColumnName("Situação Lig.Prod x Dep.");
        }
    }
}
