using System.Web.Optimization;
using AssessmentApplication.Optimization;

namespace AssessmentApplication
{
    public class BundleConfig
    {
        #region Public Methods

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new NonOrderingScriptBundle("~/bundles/angularjs").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-messages.js",
                        "~/Scripts/angular-resource.js",
                        "~/Scripts/angular-route.js",
                        "~/Scripts/angular-sanitize.js",
                        "~/Scripts/angular-animate.js",
                        "~/Scripts/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootswatch/yeti/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/site-yeti.css",
                      "~/Content/font-awesome.css"));

            bundles.Add(new NonOrderingScriptBundle("~/bundles/salesOrder").Include(
                    "~/ngApps/SalesOrder/Models/Address.js",
                    "~/ngApps/SalesOrder/Models/DateModel.js",
                    "~/ngApps/SalesOrder/Models/DetailFormModel.js",
                    "~/ngApps/SalesOrder/Models/SearchFormModel.js",
                    "~/ngApps/SalesOrder/Models/Person.js",
                    "~/ngApps/SalesOrder/Models/ShipMethod.js",
                    "~/ngApps/SalesOrder/Models/SalesOrderDetail.js",
                    "~/ngApps/SalesOrder/Models/SalesOrderHeader.js",
                    "~/ngApps/SalesOrder/salesOrder.module.js",
                    "~/ngApps/SalesOrder/salesOrder.controller.js",
                    "~/ngApps/SalesOrder/salesOrder.service.js",
                    "~/ngApps/SalesOrder/modal.service.js"));
        }

        #endregion Public Methods
    }
}
