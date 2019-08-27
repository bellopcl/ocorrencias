using System.Web;
using System.Web.Mvc;

namespace NWORKFLOW_WEB.MVC_4_BS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}