using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Library.Mvc;
using EasyLOB.Library.Syncfusion;
using EasyLOB.Persistence;
using EasyLOB.Security.Resources;
using Syncfusion.XlsIO;
using System;
using System.Data.Common;
using System.IO;
using System.Web.Mvc;

namespace EasyLOB.Mvc
{
    public partial class TasksController
    {
        // GET: Tasks/ExportSecurity
        [HttpGet]
        public ActionResult ExportSecurity()
        {
            try
            {
                if (IsTask("ExportSecurity", OperationResult))
                {
                    TaskViewModel viewModel = new TaskViewModel("Tasks", "ExportSecurity", SecurityResources.TaskExportSecurity);

                    return View("Task", viewModel);
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            return View("OperationResult", new OperationResultModel(OperationResult));
        }

        // POST: Tasks/ExportSecurity
        [HttpPost]
        public ActionResult ExportSecurity(TaskViewModel viewModel)
        {
            viewModel.OperationResult.Clear();

            try
            {
                if (IsTask("ExportSecurity", viewModel.OperationResult))
                {
                    if (IsValid(viewModel.OperationResult, viewModel))
                    {
                        string templateDirectory = Server.MapPath(ConfigurationHelper.AppSettings<string>("Directory.Template"));
                        string fileDirectory = Server.MapPath(ConfigurationHelper.AppSettings<string>("Directory.Export"));
                        string filePath;

                        if (ExportSecurity(viewModel.OperationResult, templateDirectory, fileDirectory, out filePath))
                        {
                            byte[] file = System.IO.File.ReadAllBytes(filePath);
                            return File(file, LibraryHelper.GetContentType(ZFileTypes.ftXLSX), Path.GetFileName(filePath));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                viewModel.OperationResult.ParseException(exception);
            }

            return View("Task", viewModel);
        }

        private bool ExportSecurity(ZOperationResult operationResult, string templateDirectory, string fileDirectory,
            out string filePath)
        {
            string template = "Security";
            string templatePath = LibraryHelper.AddDirectorySeparator(templateDirectory) + template + ".xlsx";
            string file = template + "." + String.Format("{0:yyyyMMdd.HHmmssfff}", DateTime.Now) + ".xlsx";
            filePath = Path.Combine(fileDirectory, file);

            DbConnection connection = null;

            ExcelEngine excelEngine = null;

            try
            {
                DbProviderFactory provider;
                DbCommand command;
                DbDataReader reader;
                string connectionName;

                System.IO.File.Copy(templatePath, filePath);

                excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                IWorkbook workbook = application.Workbooks.Open(filePath);
                //workbook.Version = ExcelVersion.Excel2013;
                IWorksheet worksheet;

                int row;

                // Identity ////////////////////////////////////////////////////////////////////////

                connectionName = MultiTenantHelper.GetConnectionName("Identity");

                provider = AdoNetHelper.GetProvider(connectionName);
                connection = provider.CreateConnection();
                connection.ConnectionString = AdoNetHelper.GetConnectionString(connectionName);
                connection.Open();

                //DbParameter parameter;

                command = provider.CreateCommand();
                command.Connection = connection;
                command.CommandTimeout = 600;
                command.CommandType = System.Data.CommandType.Text;

                // Users

                worksheet = workbook.Worksheets["Users"];

                command.CommandText = @"
SELECT
    UserName UserName,
    Email email
FROM
    AspNetUsers
ORDER BY
    UserName";

                reader = command.ExecuteReader();

                row = 2;
                while (reader.Read())
                {
                    int column = 1;

                    worksheet.Range[row, column++].Value2 = reader.ToString("UserName");
                    worksheet.Range[row, column++].Value2 = reader.ToString("email");

                    row++;
                }

                reader.Close();

                worksheet.AutoAlign(1, 2);

                // Roles

                worksheet = workbook.Worksheets["Roles"];

                command.CommandText = @"
SELECT
    Name RoleName
FROM
    AspNetRoles
ORDER BY
    RoleName
";

                reader = command.ExecuteReader();

                row = 2;
                while (reader.Read())
                {
                    int column = 1;

                    worksheet.Range[row, column++].Value2 = reader.ToString("RoleName");

                    row++;
                }

                reader.Close();

                worksheet.AutoAlign(1, 1);

                // Roles by User

                worksheet = workbook.Worksheets["Roles by User"];

                command.CommandText = @"
SELECT
    AspNetUsers.UserName UserName,
    AspNetRoles.Name RoleName
FROM
    AspNetUserRoles
    INNER JOIN AspNetUsers ON
        AspNetUsers.Id = AspNetUserRoles.UserId
    INNER JOIN AspNetRoles ON
        AspNetRoles.Id = AspNetUserRoles.RoleId
ORDER BY
    1,2
";

                reader = command.ExecuteReader();

                row = 2;
                while (reader.Read())
                {
                    int column = 1;

                    worksheet.Range[row, column++].Value2 = reader.ToString("UserName");
                    worksheet.Range[row, column++].Value2 = reader.ToString("RoleName");

                    row++;
                }

                reader.Close();

                worksheet.AutoAlign(1, 2);

                connection.Close();

                // Activity ////////////////////////////////////////////////////////////////////////

                connectionName = MultiTenantHelper.GetConnectionName("Activity");

                provider = AdoNetHelper.GetProvider(connectionName);
                connection = provider.CreateConnection();
                connection.ConnectionString = AdoNetHelper.GetConnectionString(connectionName);
                connection.Open();

                //DbParameter parameter;

                command = provider.CreateCommand();
                command.Connection = connection;
                command.CommandTimeout = 600;
                command.CommandType = System.Data.CommandType.Text;

                // Activities

                worksheet = workbook.Worksheets["Activities"];

                command.CommandText = @"
SELECT
    Name ActivityName
FROM
    Activity
ORDER BY
    Name
";

                reader = command.ExecuteReader();

                row = 2;
                while (reader.Read())
                {
                    int column = 1;

                    worksheet.Range[row, column++].Value2 = reader.ToString("ActivityName");

                    row++;
                }

                reader.Close();

                worksheet.AutoAlign(1, 1);

                // Roles by Activity

                worksheet = workbook.Worksheets["Roles by Activity"];

                command.CommandText = @"
SELECT
    Activity.Name ActivityName,
    RoleName,
    Operations
FROM
    ActivityRole
    INNER JOIN Activity ON
        Activity.Id = ActivityRole.ActivityId
ORDER BY
    Name
";

                reader = command.ExecuteReader();

                row = 2;
                while (reader.Read())
                {
                    int column = 1;

                    worksheet.Range[row, column++].Value2 = reader.ToString("ActivityName");
                    worksheet.Range[row, column++].Value2 = reader.ToString("RoleName");
                    worksheet.Range[row, column++].Value2 = reader.ToString("Operations");

                    row++;
                }

                reader.Close();

                worksheet.AutoAlign(1, 3);

                workbook.Save();
                workbook.Close();
            }
            catch (Exception exception)
            {
                (operationResult as ZOperationResult).ParseException(exception);
            }
            finally
            {
                if (excelEngine != null)
                {
                    excelEngine.Dispose();
                }

                if (connection != null)
                {
                    connection.Close();
                }
            }

            return operationResult.Ok;
        }
    }
}