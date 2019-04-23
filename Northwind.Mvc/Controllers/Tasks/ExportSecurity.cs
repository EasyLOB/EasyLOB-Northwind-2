using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Persistence;
using EasyLOB.Security.Resources;
using Syncfusion.XlsIO;
using System;
using System.Data.Common;
using System.IO;
using System.Web.Mvc;
using EasyLOB.Library.App;

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
                    TaskModel taskModel = new TaskModel("Tasks", "ExportSecurity", SecurityResources.TaskExportSecurity);

                    return View("Task", taskModel);
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            return View("OperationResult", new OperationResultViewModel(OperationResult));
        }

        // POST: Tasks/ExportSecurity
        [HttpPost]
        public ActionResult ExportSecurity(TaskModel taskModel)
        {
            taskModel.OperationResult.Clear();

            try
            {
                if (IsTask("ExportSecurity", taskModel.OperationResult))
                {
                    if (IsValid(taskModel.OperationResult, taskModel))
                    {
                        string templateDirectory = Server.MapPath(ConfigurationHelper.AppSettings<string>("Directory.Template"));
                        string fileDirectory = Server.MapPath(ConfigurationHelper.AppSettings<string>("Directory.Export"));
                        string filePath;

                        if (ExportSecurity(taskModel.OperationResult, templateDirectory, fileDirectory, out filePath))
                        {
                            byte[] file = System.IO.File.ReadAllBytes(filePath);
                            return File(file, LibraryHelper.GetContentType(ZFileTypes.ftXLSX), Path.GetFileName(filePath));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                taskModel.OperationResult.ParseException(exception);
            }

            return View("Task", taskModel);
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
    Email EMail,
    LockoutEnabled Lockout,
    LockoutEndDateUtc LockoutEndDate
FROM
    AspNetUsers
ORDER BY
    UserName
";

                reader = command.ExecuteReader();

                row = 2;
                while (reader.Read())
                {
                    int column = 1;

                    worksheet.Range[row, column++].Value2 = reader.ToString("UserName");
                    worksheet.Range[row, column++].Value2 = reader.ToString("EMail");
                    worksheet.Range[row, column++].Value2 = reader.ToBoolean("Lockout") ? EasyLOB.Resources.PresentationResources.Yes : EasyLOB.Resources.PresentationResources.No;
                    worksheet.Range[row, column++].ClearDateTime(reader.ToDateTimeNullable("LockoutEndDate"));

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
                operationResult.ParseException(exception);
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