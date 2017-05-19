using EasyLOB.Library.Mvc;
using System.Web.Optimization;

namespace EasyLOB.Mvc
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            Bundle bundle;

            // Force Bundles in Debug Mode
            //BundleTable.EnableOptimizations = true;

            // CSS

            bundles.Add(new StyleBundle("~/content/z-css.css")
                .Include("~/Content/bootstrap.css") // .css + .min.css
                .Include("~/Content/bootstrap-theme.css") // .css + .min.css
                .Include("~/Content/site.css")
            );

            // Responsive Layout
            // https://help.syncfusion.com/aspnetmvc/menu/responsive-layout

            bundles.Add(new StyleBundle("~/content/ej/web/z-syncfusion.css")
                .Include("~/Content/ej/web/ej.widgets.core.min.css") // .min.css
                .Include("~/Content/ej/web/responsive-css/ej.responsive.css") // .css
                .Include("~/Content/ej/web/responsive-css/ejgrid.responsive.css") // .css
            );

            bundles.Add(new StyleBundle("~/content/ej/web/bootstrap-theme/z-syncfusion-theme.css")
                .Include("~/Content/ej/web/ej.widgets.core.bootstrap.min.css") // .min.css
                .Include("~/Content/ej/web/bootstrap-theme/ej.theme.min.css") // .min.css
            );

            bundles.Add(new StyleBundle("~/content/easylob/z-easylob.css")
                .Include("~/Content/easylob/easylob.css")
            );

            // JavaScript

            // Bootstrap

            bundle = new Bundle("~/scripts/z-bootstrap.js");
            bundle.Orderer = new AsIsBundleOrderer();
            bundle
                .Include("~/Scripts/bootstrap.js") // .js + .min.js
                .Include("~/Scripts/respond.js") // .js + .min.js
                .Include("~/Scripts/respond.matchmedia.addListener.js"); // .js + .min.js
            bundles.Add(bundle);

            // jQuery

            bundle = new Bundle("~/scripts/z-jquery.js");
            bundle.Orderer = new AsIsBundleOrderer();
            bundle
                // 1st
                .Include("~/Scripts/jquery-{version}.js") // .js + .min.js
                .Include("~/Scripts/jquery.easing.{version}.js")
                // 2nd
                .Include("~/Scripts/jquery.validate.js") // .js + .min.js
                .Include("~/Scripts/jquery.validate.unobtrusive.js") // .js + .min.js
                                                                     // 3rd
                .Include("~/Scripts/globalize/globalize.js")
                .Include("~/Scripts/globalize/cultures/globalize.culture.en-US.js")
                .Include("~/Scripts/globalize/cultures/globalize.culture.pt-BR.js")
                // Readonly.js
                // https://github.com/haggen/readonly
                .Include("~/Scripts/easylob/readonly.js");
            bundles.Add(bundle);

            // jQuery AJAX

            bundles.Add(new ScriptBundle("~/scripts/z-jquery-ajax.js")
                .Include("~/Scripts/jquery.unobtrusive-ajax.js") // .js + .min.js
            );

            // Modernizr

            bundles.Add(new ScriptBundle("~/scripts/z-modernizr.js")
                .Include("~/Scripts/modernizr-{version}.js")
            );

            // Syncfusion
            // Syncfusion Essential JS Custom Script Generator
            // http://csg.syncfusion.com
            // ej.widget.easylob.js 
            // ej.widget.easylob.min.js
            // + ? ejDatePicker ?
            // + ? ejDateTimePicker ?
            // + ejGrid
            // + ejMenu
            // + ejReportViewer
            // + ejTab
            // + ejUploadBox

            bundle = new Bundle("~/scripts/z-syncfusion.js");
            bundle.Orderer = new AsIsBundleOrderer();
            bundle
                // http://www.jsviews.com/#download
                .Include("~/Scripts/ej/jsrender.js") // .js + .min.js
                //
                //.Include("~/Scripts/ej/ej.web.all.js") // .js + .min.js
                .Include("~/Scripts/ej/ej.widget.easylob.js") // .js + .min.js
                .Include("~/Scripts/ej/ej.globalize.js") // .js + .min.js
                .Include("~/Scripts/ej/ej.unobtrusive.js") // .js + .min.js
                // en-US
                .Include("~/Scripts/ej/i18n/ej.culture.en-US.js") // .js + .min.js
                .Include("~/Scripts/ej/l10n/ej.datepicker.localetexts.en-US.js")
                .Include("~/Scripts/ej/l10n/ej.datetimepicker.localetexts.en-US.js")
                .Include("~/Scripts/ej/l10n/ej.excelfilter.localetexts.en-US.js")
                .Include("~/Scripts/ej/l10n/ej.grid.localetexts.en-US.js")
                .Include("~/Scripts/ej/l10n/ej.pager.localetexts.en-US.js")
                .Include("~/Scripts/ej/l10n/ej.reportviewer.localetexts.en-US.js")
                .Include("~/Scripts/ej/l10n/ej.uploadbox.localetexts.en-US.js")
                // pt-BR
                .Include("~/Scripts/ej/i18n/ej.culture.pt-BR.js") // .js + .min.js
                .Include("~/Scripts/ej/l10n/ej.datepicker.localetexts.pt-BR.js")
                .Include("~/Scripts/ej/l10n/ej.datetimepicker.localetexts.pt-BR.js")
                .Include("~/Scripts/ej/l10n/ej.excelfilter.localetexts.pt-BR.js")
                .Include("~/Scripts/ej/l10n/ej.grid.localetexts.pt-BR.js")
                .Include("~/Scripts/ej/l10n/ej.pager.localetexts.pt-BR.js")
                .Include("~/Scripts/ej/l10n/ej.reportviewer.localetexts.pt-BR.js")               
                .Include("~/Scripts/ej/l10n/ej.uploadbox.localetexts.pt-BR.js");
            bundles.Add(bundle);

            // EasyLOB

            bundle = new Bundle("~/scripts/easylob/z-easylob.js");
            bundle.Orderer = new AsIsBundleOrderer();
            bundle
                .Include("~/Scripts/easylob/easylob.js")
                .Include("~/Scripts/easylob/easylob.dictionary.js")
                .Include("~/Scripts/easylob/jquery.validate.globalize.js");
            bundles.Add(bundle);
        }
    }
}
