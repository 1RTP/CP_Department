using System.Web.Optimization;

namespace MvcRazor
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap/bootstrap.min.css", "~/Content/site.css"));            
        }
    }
}