using DevExpress.Web.Internal;
using System.Web.Mvc;

namespace DevExpress.Web.Mvc.UI
{
    public static class ExtensionsFactoryExtension
    {
        public static MvcHtmlString GetScriptsEx(this ExtensionsFactory factory, params Script[] scriptItems)
        {
            try
            {
                return factory.GetScripts(scriptItems);
            }
            finally
            {
                MvcUtils.RenderScriptsCalled = false;
            }
        }
    }
}