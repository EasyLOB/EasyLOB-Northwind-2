#region Copyright

// Copyright Syncfusion Inc. 2001 - 2015. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws.

#endregion Copyright

using Syncfusion.XlsIO;
using System.Web;

//string file = "Excel.xlsx";
//return excelEngine.SaveAsActionResult(workbook, file, HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog, ExcelHttpContentType.Excel2013);
//return excelEngine.SaveAsActionResult(excelEngine.Excel.Workbooks[0], file, HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog, ExcelHttpContentType.Excel2013);

namespace EasyLOB.Library.Syncfusion
{
    public static class XlsIOExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="_workbook"></param>
        /// <param name="filename"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static XlsResult SaveAsActionResult(this ExcelEngine _engine, IWorkbook _workbook, string filename, HttpResponse response)
        {
            ExcelHttpContentType contentType = ExcelHttpContentType.Excel2007;
            if (_workbook.Version == ExcelVersion.Excel2007)
                contentType = ExcelHttpContentType.Excel2007;
            else if (_workbook.Version == ExcelVersion.Excel97to2003)
                contentType = ExcelHttpContentType.Excel2000;

            return new XlsResult(_engine, _workbook, filename, response, ExcelDownloadType.PromptDialog, contentType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_workbook"></param>
        /// <param name="filename"></param>
        /// <param name="response"></param>
        /// <param name="DownloadType"></param>
        /// <returns></returns>
        public static XlsResult SaveAsActionResult(this ExcelEngine _engine, IWorkbook _workbook, string filename, HttpResponse response, ExcelDownloadType DownloadType)
        {
            ExcelHttpContentType contentType = ExcelHttpContentType.Excel2007;
            if (_workbook.Version == ExcelVersion.Excel2007)
                contentType = ExcelHttpContentType.Excel2007;
            else if (_workbook.Version == ExcelVersion.Excel97to2003)
                contentType = ExcelHttpContentType.Excel2000;
            return new XlsResult(_engine, _workbook, filename, response, DownloadType, contentType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_workbook"></param>
        /// <param name="filename"></param>
        /// <param name="response"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static XlsResult SaveAsActionResult(this ExcelEngine _engine, IWorkbook _workbook, string filename, HttpResponse response, ExcelHttpContentType contentType)
        {
            return new XlsResult(_engine, _workbook, filename, response, ExcelDownloadType.PromptDialog, contentType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_workbook"></param>
        /// <param name="filename"></param>
        /// <param name="response"></param>
        /// <param name="DownloadType"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static XlsResult SaveAsActionResult(this ExcelEngine _engine, IWorkbook _workbook, string filename, HttpResponse response, ExcelDownloadType DownloadType, ExcelHttpContentType contentType)
        {
            return new XlsResult(_engine, _workbook, filename, response, DownloadType, contentType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_workbook"></param>
        /// <param name="filename"></param>
        /// <param name="saveType"></param>
        /// <param name="response"></param>
        /// <param name="DownloadType"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static XlsResult SaveAsActionResult(this ExcelEngine _engine, IWorkbook _workbook, string filename, ExcelSaveType saveType, HttpResponse response, ExcelDownloadType DownloadType, ExcelHttpContentType contentType)
        {
            return new XlsResult(_engine, _workbook, filename, response, DownloadType, contentType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_workbook"></param>
        /// <param name="filename"></param>
        /// <param name="saveType"></param>
        /// <param name="response"></param>
        /// <param name="DownloadType"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static XlsResult SaveAsActionResult(this ExcelEngine _engine, IWorkbook _workbook, string filename, string separator, HttpResponse response, ExcelDownloadType DownloadType, ExcelHttpContentType contentType)
        {
            return new XlsResult(_engine, _workbook, filename, separator, response, DownloadType, contentType);
        }
    }
}