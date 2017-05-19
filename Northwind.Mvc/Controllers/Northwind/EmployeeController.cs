using Northwind.Application;
using Northwind.Data;
using Northwind.Data.Resources;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Library.Syncfusion;
using EasyLOB.Mvc;
using Newtonsoft.Json;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Northwind.Mvc
{
    public partial class EmployeeController : BaseMvcControllerSCRUDApplication<EmployeeDTO, Employee>
    {
        #region Methods

        public EmployeeController(INorthwindGenericApplicationDTO<EmployeeDTO, Employee> application)
        {
            Application = application;            
        }

        #endregion
        
        #region Methods SCRUD

        // GET: Employee
        // GET: Employee/Index
        [HttpGet]
        public ActionResult Index(int? masterReportsTo = null)
        {
            EmployeeCollectionModel employeeCollectionModel = new EmployeeCollectionModel(ActivityOperations, "Index", null, masterReportsTo);

            try
            {
                IsOperation(employeeCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                employeeCollectionModel.OperationResult.ParseException(exception);
            }

            return View(employeeCollectionModel);
        }        

        // GET & POST: Employee/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, int? masterReportsTo = null)
        {
            EmployeeCollectionModel employeeCollectionModel = new EmployeeCollectionModel(ActivityOperations, "Search", masterControllerAction, masterReportsTo);

            try
            {
                if (IsOperation(employeeCollectionModel.OperationResult))
                {
                    return PartialView(employeeCollectionModel);
                }
            }
            catch (Exception exception)
            {
                employeeCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(employeeCollectionModel.OperationResult);
        }

        // GET & POST: Employee/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_EmployeeLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: Employee/Create
        [HttpGet]
        public ActionResult Create(int? masterReportsTo = null)
        {
            EmployeeItemModel employeeItemModel = new EmployeeItemModel(ActivityOperations, "Create", masterReportsTo);

            try
            {
                if (IsCreate(employeeItemModel.OperationResult))
                {
                    return PartialView("CRUD", employeeItemModel);
                }            
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(employeeItemModel.OperationResult);
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeItemModel employeeItemModel)
        {
            try
            {
                if (IsCreate(employeeItemModel.OperationResult))
                {
                    if (IsValid(employeeItemModel.OperationResult, employeeItemModel.Employee))
                    {
                        EmployeeDTO employeeDTO = (EmployeeDTO)employeeItemModel.Employee.ToDTO();
                        if (Application.Create(employeeItemModel.OperationResult, employeeDTO))
                        {
                            if (employeeItemModel.IsSave)
                            {
                                employeeItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(employeeItemModel.OperationResult,
                                    Url.Content(String.Format("~/Employee/Update?EmployeeId={0}", employeeDTO.EmployeeId)));
                            }
                            else
                            {
                                return JsonResultSuccess(employeeItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            employeeItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(employeeItemModel.OperationResult);
        }

        // GET: Employee/Read/1
        [HttpGet]
        public ActionResult Read(int employeeId, int? masterReportsTo = null)
        {
            EmployeeItemModel employeeItemModel = new EmployeeItemModel(ActivityOperations, "Read", masterReportsTo);
            
            try
            {
                if (IsRead(employeeItemModel.OperationResult))
                {
                    EmployeeDTO employeeDTO = Application.GetById(employeeItemModel.OperationResult, new object[] { employeeId });
                    if (employeeDTO != null)
                    {
                        employeeItemModel.Employee = new EmployeeViewModel(employeeDTO);                    

                        return PartialView("CRUD", employeeItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(employeeItemModel.OperationResult);
        }

        // GET: Employee/Update/1
        [HttpGet]
        public ActionResult Update(int employeeId, int? masterReportsTo = null)
        {
            EmployeeItemModel employeeItemModel = new EmployeeItemModel(ActivityOperations, "Update", masterReportsTo);
            
            try
            {
                if (IsUpdate(employeeItemModel.OperationResult))
                {            
                    EmployeeDTO employeeDTO = Application.GetById(employeeItemModel.OperationResult, new object[] { employeeId });
                    if (employeeDTO != null)
                    {
                        employeeItemModel.Employee = new EmployeeViewModel(employeeDTO);

                        return PartialView("CRUD", employeeItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(employeeItemModel.OperationResult);
        }

        // POST: Employee/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EmployeeItemModel employeeItemModel)
        {
            try
            {
                if (IsUpdate(employeeItemModel.OperationResult))
                {
                    if (IsValid(employeeItemModel.OperationResult, employeeItemModel.Employee))
                    {
                        EmployeeDTO employeeDTO = (EmployeeDTO)employeeItemModel.Employee.ToDTO();
                        if (Application.Update(employeeItemModel.OperationResult, employeeDTO))
                        {
                            if (employeeItemModel.IsSave)
                            {
                                return JsonResultSuccess(employeeItemModel.OperationResult,
                                    Url.Content(String.Format("~/Employee/Update?EmployeeId={0}", employeeDTO.EmployeeId)));
                            }
                            else
                            {
                                return JsonResultSuccess(employeeItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            employeeItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(employeeItemModel.OperationResult);
        }

        // GET: Employee/Delete/1
        [HttpGet]
        public ActionResult Delete(int employeeId, int? masterReportsTo = null)
        {
            EmployeeItemModel employeeItemModel = new EmployeeItemModel(ActivityOperations, "Delete", masterReportsTo);
            
            try
            {
                if (IsDelete(employeeItemModel.OperationResult))
                {            
                    EmployeeDTO employeeDTO = Application.GetById(employeeItemModel.OperationResult, new object[] { employeeId });
                    if (employeeDTO != null)
                    {
                        employeeItemModel.Employee = new EmployeeViewModel(employeeDTO);

                        return PartialView("CRUD", employeeItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(employeeItemModel.OperationResult);
        }

        // POST: Employee/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EmployeeItemModel employeeItemModel)
        {
            try
            {
                if (IsDelete(employeeItemModel.OperationResult))
                {
                    if (Application.Delete(employeeItemModel.OperationResult, (EmployeeDTO)employeeItemModel.Employee.ToDTO()))
                    {
                        return JsonResultSuccess(employeeItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                employeeItemModel.OperationResult.ParseException(exception);
            }

            employeeItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(employeeItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Employee/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<EmployeeViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Employee), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<EmployeeViewModel, EmployeeDTO, Employee>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
                    if (dataManager.RequiresCounts)
                    {
                        countAll = Application.Count(where, args.ToArray());
                    }
                }
                catch (Exception exception)
                {
                    operationResult.ParseException(exception);
                }

                if (!operationResult.Ok)
                {
                    throw new InvalidOperationException(operationResult.Text);
                }
            }

            return Json(JsonConvert.SerializeObject(new { result = data, count = countAll }), JsonRequestBehavior.AllowGet);
        } 

        // POST: Employee/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, EmployeeResources.EntitySingular + ".xlsx");
            }
        }

        // POST: Employee/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, EmployeeResources.EntitySingular + ".pdf");
            }
        }

        // POST: Employee/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, EmployeeResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}