using System.Web.Mvc;
using System.Xml.Linq;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    internal class XmlActionResult : ActionResult
    {
        private XDocument xDocument;

        public XmlActionResult(XDocument xDocument)
        {
            this.xDocument = xDocument;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}