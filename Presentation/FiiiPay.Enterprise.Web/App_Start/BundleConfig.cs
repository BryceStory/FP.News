using System.Web;
using System.Web.Optimization;

namespace FiiiPay.Enterprise.Web
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/basefunction").Include(
                         "~/Plugins/fiiipay/basefunction.js"));
            bundles.Add(new ScriptBundle("~/bundles/fiiiextend").Include(
                         "~/Plugins/fiiipay/fiiiextend.js",
                        "~/Plugins/fiiipay/gridpager.js",
                        "~/Plugins/fiiipay/bo.control.js",
                        "~/Plugins/sweetalert/sweetalert.min.js",
                        "~/Plugins/ueditor-net/ueditor.config.js",
                        "~/Plugins/ueditor-net/ueditor.all.js",
                        "~/Plugins/ueditor-net/lang/zh-cn/zh-cn.js",
                        "~/Plugins/webuploader/webuploader.min.js"
                       
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/basecss").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/resource").Include(
                         "~/Plugins/fiiipay/resources.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Plugins/bootstrap/js/bootstrap.js",
                        "~/Scripts/respond.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                        "~/Plugins/metismenu/metisMenu.js",
                        "~/Plugins/elektron/onoffcanvas.js",
                        "~/Plugins/elektron/elektron.js",
                        "~/Plugins/sweetalert/sweetalert.min.js",
                        "~/Plugins/daterangepicker/moment.min.js",
                        "~/Plugins/daterangepicker/daterangepicker.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Plugins/bootstrap/css/bootstrap.css",
                      "~/Plugins/font-awesome/css/font-awesome.css",
                      "~/Plugins/metismenu/metisMenu.css",
                      "~/Plugins/elektron/onoffcanvas.css",
                      "~/Plugins/elektron/elektron.css",
                      "~/Plugins/elektron/elektron.fiiipaymerchant.css",
                      "~/Plugins/sweetalert/sweetalert.css",
                      "~/Plugins/sweetalert/sweetalert.fiiienterprise.css",
                      "~/Plugins/daterangepicker/daterangepicker.css",
                      "~/Plugins/fiiipay/gridpager.css",
                      "~/Content/page.css",
                      "~/Content/site.css",
                      "~/Plugins/webuploader/webuploader.css",
                      "~/Content/plugin.css",
                      "~/Plugins/ueditor-net/themes/default/css/ueditor.css"
                      ));
        }
    }
}
