using System.Web;
using System.Web.Optimization;

namespace NWORKFLOW_WEB.MVC_4_BS
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/validate").Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/maskedinput").Include("~/Scripts/jquery.maskedinput.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.custom.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include("~/Scripts/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include("~/Scripts/datepicker-pt-BR.js"));

            bundles.Add(new ScriptBundle("~/bundles/maskMoney").Include("~/Scripts/jquery.maskMoney.js"));

            bundles.Add(new ScriptBundle("~/bundles/tree").Include("~/Scripts/jquery.tree.js"));

            bundles.Add(new ScriptBundle("~/bundles/jstree").Include("~/Scripts/jstree.js"));

            bundles.Add(new ScriptBundle("~/bundles/jsmaterialize").Include("~/Scripts/materialize.js"));


            // ESTILOS

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css", "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/situacao").Include("~/Content/situacao.css"));

            bundles.Add(new StyleBundle("~/Content/timeLine").Include("~/Content/timeLine.css"));

            bundles.Add(new StyleBundle("~/Content/materialize").Include("~/Content/materialize.css"));

            bundles.Add(new StyleBundle("~/Content/arvore").Include("~/Content/themes/default/style.css"));
            
            bundles.Add(new StyleBundle("~/Content/sb-admin-2").Include("~/Content/sb-admin-2.css"));

            bundles.Add(new StyleBundle("~/Content/tooltip").Include("~/Content/tooltip.css"));

            bundles.Add(new StyleBundle("~/Content/style").Include("~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/headerside").Include("~/Content/headerside.css"));

            bundles.Add(new StyleBundle("~/Content/styleside").Include("~/Content/styleside.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryui").Include("~/Content/jquery-ui-{version}.custom.css"));

            bundles.Add(new StyleBundle("~/Content/dataTables").Include("~/Content/jquery.dataTables.css", "~/Content/jquery.dataTables_themeroller.css"));

            bundles.Add(new StyleBundle("~/Content/tree").Include("~/Content/jquery.tree.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrapsidebar").Include("~/Content/bootstrapsidebar.css"));
        }
    }
}