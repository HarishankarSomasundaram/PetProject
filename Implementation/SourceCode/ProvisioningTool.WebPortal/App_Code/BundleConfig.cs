using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


/// <summary>
/// Summary description for BundleConfig
/// </summary>
public class BundleConfig
{
    public static void RegisterBundle(BundleCollection bundle)
    {

        ////bundle all common js files, required in every page  
        //bundle.Add(new ScriptBundle("~/bundles/login")
        //    .Include(
        //        "~/includes/ui/js/jquery.min.js",
        //        "~/includes/ui/js/jquery.cookie.js",
        //        "~/includes/ui/js/jquery.colorbox.js"
        //));

        ////bundle all common js files, required in every page  
        //bundle.Add(new ScriptBundle("~/bundles/jquery")
        //    .Include(
        //        "~/includes/ui/js/jquery.min.js"
        //));

        ////bundle all common js files, required in every page  
        //bundle.Add(new ScriptBundle("~/bundles/js")
        //    .Include("~/includes/ui/js/jquery.dcjqaccordion.2.7.min.js",
        //        "~/includes/common/select2/select2.js",
        //        "~/includes/customJQGrid/jquery.jqGrid-3.7.2/src/i18n/grid.locale-en.js",
        //        "~/includes/customJQGrid/jquery.jqGrid-3.7.2/src/jquery.jqGrid.src.js",
        //        "~/includes/customJQGrid/jquery.jqGrid-3.7.2/js/jqGrid.gen.js",
        //        "~/includes/customJQGrid/jquery.jqGrid-3.7.2/js/jqGrid.gen_forInternetWeb.js",
        //        "~/includes/ui/js/json2.js",
        //        "~/includes/ui/js/jquery-ui.js",
        //        "~/includes/ui/js/chosen.jquery.js",
        //        "~/includes/common/jspdfPlugins/dist/jspdf.debug.js",
        //        "~/includes/common/jspdfPlugins/basic.js",
        //        "~/includes/common/jquery.form-validator/jquery.form-validator.js",
        //        "~/includes/ui/js/jquery.cookie.js",
        //        "~/includes/ui/js/menuStyle.js",
        //        "~/includes/ui/js/jquery.colorbox.js",
        //        "~/includes/ui/js/jquery.history.js",
        //        "~/includes/ui/js/jquery.hoverIntent.minified.js",
        //        "~/includes/ui/js/jquery.mask.js",
        //        "~/includes/ui/js/jquery.tagsinput.min.js",
        //        "~/includes/ui/js/jquery.watermark.js",
        //        "~/includes/ui/js/customerinfo.js",
        //        "~/includes/ui/js/custom.js",
        //        "~/includes/ui/js/checkable.js"
        //));



        //wrapup all css in a bundle  
        bundle.Add(new StyleBundle("~/Content/css")
        .Include(
        
                "~/includes/UI/css/jquery-ui-framework.css",
                "~/includes/customJQGrid/jquery.jqGrid-3.7.2/css/ui.jqgrid.css",
                "~/includes/UI/css/theme.css",
                "~/includes/UI/fonts/fonts.css",
                "~/includes/UI/css/style.css",
                "~/includes/UI/css/admin.css",
                "~/includes/UI/css/jquery-ui.css",
                "~/includes/UI/css/chosen.css",
                "~/includes/UI/css/checkble.css",
                "~/includes/common/select2/select2.css",
                "~/includes/UI/css/colorbox.css"));
        BundleTable.EnableOptimizations = true;
    }

}