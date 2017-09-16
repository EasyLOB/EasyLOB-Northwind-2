﻿using Northwind.Application;
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
    public partial class EmployeeTerritoryController : BaseMvcControllerSCRUDApplication<EmployeeTerritoryDTO, EmployeeTerritory>
    {
        #region Methods

        public EmployeeTerritoryController(INorthwindGenericApplicationDTO<EmployeeTerritoryDTO, EmployeeTerritory> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: EmployeeTerritory
        // GET: EmployeeTerritory/Index
        [HttpGet]
        public ActionResult Index(int? masterEmployeeId = null, string masterTerritoryId = null)
        {
            EmployeeTerritoryCollectionModel employeeTerritoryCollectionModel = new EmployeeTerritoryCollectionModel(ActivityOperations, "Index", null, masterEmployeeId, masterTerritoryId);

            try
            {
                IsOperation(employeeTerritoryCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                employeeTerritoryCollectionModel.OperationResult.ParseException(exception);
            }

            return View(employeeTerritoryCollectionModel);
        }        

        // GET & POST: EmployeeTerritory/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, int? masterEmployeeId = null, string masterTerritoryId = null)
        {
            EmployeeTerritoryCollectionModel employeeTerritoryCollectionModel = new EmployeeTerritoryCollectionModel(ActivityOperations, "Search", masterControllerAction, masterEmployeeId, masterTerritoryId);

            try
            {
                if (IsOperation(employeeTerritoryCollectionModel.OperationResult))
                {
                    return PartialView(employeeTerritoryCollectionModel);
                }
            }
            catch (Exception exception)
            {
                employeeTerritoryCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(employeeTerritoryCollectionModel.OperationResult);
        }

        // GET & POST: EmployeeTerritory/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_EmployeeTerritoryLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: EmployeeTerritory/Create
        [HttpGet]
        public ActionResult Create(int? masterEmployeeId = null, string masterTerritoryId = null)
        {
            EmployeeTerritoryItemModel employeeTerritoryItemModel = new EmployeeTerritoryItemModel(ActivityOperations, "Create", masterEmployeeId, masterTerritoryId);

            try
            {
                if (IsCreate(employeeTerritoryItemModel.OperationResult))
                {
                    return PartialView("CRUD", employeeTerritoryItemModel);
                }            
            }
            catch (Exception exception)
            {
                employeeTerritoryItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(employeeTerritoryItemModel.OperationResult);
        }

        // POST: EmployeeTerritory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeTerritoryItemModel employeeTerritoryItemModel)
        {
            try
            {
                if (IsCreate(employeeTerritoryItemModel.OperationResult))
                {
                    if (IsValid(employeeTerritoryItemModel.OperationResult, employeeTerritoryItemModel.EmployeeTerritory))
                    {
                        EmployeeTerritoryDTO employeeTerritoryDTO = (EmployeeTerritoryDTO)employeeTerritoryItemModel.EmployeeTerritory.ToDTO();
                        if (Application.Create(employeeTerritoryItemModel.OperationResult, employeeTerritoryDTO))
                        {
                            if (employeeTerritoryItemModel.IsSave)
                            {
                                employeeTerritoryItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(employeeTerritoryItemModel.OperationResult,
                                    Url.Action("Update", "EmployeeTerritory", new { EmployeeId = employeeTerritoryDTO.EmployeeId, TerritoryId = employeeTerritoryDTO.TerritoryId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(employeeTerritoryItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                employeeTerritoryItemModel.OperationResult.ParseException(exception);
            }

            employeeTerritoryItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(employeeTerritoryItemModel.OperationResult);
        }

        // GET: EmployeeTerritory/Read/1
        [HttpGet]
        public ActionResult Read(int employeeId, string territoryId, int? masterEmployeeId = null, string masterTerritoryId = null)
        {
            EmployeeTerritoryItemModel employeeTerritoryItemModel = new EmployeeTerritoryItemModel(ActivityOperations, "Read", masterEmployeeId, masterTerritoryId);
            
            try
            {
                if (IsRead(employeeTerritoryItemModel.OperationResult))
                {
                    EmployeeTerritoryDTO employeeTerritoryDTO = Application.GetById(employeeTerritoryItemModel.OperationResult, new object[] { employeeId, territoryId });
                    if (employeeTerritoryDTO != null)
                    {
                        employeeTerritoryItemModel.EmployeeTerritory = new EmployeeTerritoryViewModel(employeeTerritoryDTO);                    

                        return PartialView("CRUD", employeeTerritoryItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                employeeTerritoryItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(employeeTerritoryItemModel.OperationResult);
        }

        // GET: EmployeeTerritory/Update/1
        [HttpGet]
        public ActionResult Update(int employeeId, string territoryId, int? masterEmployeeId = null, string masterTerritoryId = null)
        {
            EmployeeTerritoryItemModel employeeTerritoryItemModel = new EmployeeTerritoryItemModel(ActivityOperations, "Update", masterEmployeeId, masterTerritoryId);
            
            try
            {
                if (IsUpdate(employeeTerritoryItemModel.OperationResult))
                {            
                    EmployeeTerritoryDTO employeeTerritoryDTO = Application.GetById(employeeTerritoryItemModel.OperationResult, new object[] { employeeId, territoryId });
                    if (employeeTerritoryDTO != null)
                    {
                        employeeTerritoryItemModel.EmployeeTerritory = new EmployeeTerritoryViewModel(employeeTerritoryDTO);

                        return PartialView("CRUD", employeeTerritoryItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                employeeTerritoryItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(employeeTerritoryItemModel.OperationResult);
        }

        // POST: EmployeeTerritory/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EmployeeTerritoryItemModel employeeTerritoryItemModel)
        {
            try
            {
                if (IsUpdate(employeeTerritoryItemModel.OperationResult))
                {
                    if (IsValid(employeeTerritoryItemModel.OperationResult, employeeTerritoryItemModel.EmployeeTerritory))
                    {
                        EmployeeTerritoryDTO employeeTerritoryDTO = (EmployeeTerritoryDTO)employeeTerritoryItemModel.EmployeeTerritory.ToDTO();
                        if (Application.Update(employeeTerritoryItemModel.OperationResult, employeeTerritoryDTO))
                        {
                            if (employeeTerritoryItemModel.IsSave)
                            {
                                return JsonResultSuccess(employeeTerritoryItemModel.OperationResult,
                                    Url.Action("Update", "EmployeeTerritory", new { EmployeeId = employeeTerritoryDTO.EmployeeId, TerritoryId = employeeTerritoryDTO.TerritoryId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(employeeTerritoryItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                employeeTerritoryItemModel.OperationResult.ParseException(exception);
            }

            employeeTerritoryItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(employeeTerritoryItemModel.OperationResult);
        }

        // GET: EmployeeTerritory/Delete/1
        [HttpGet]
        public ActionResult Delete(int employeeId, string territoryId, int? masterEmployeeId = null, string masterTerritoryId = null)
        {
            EmployeeTerritoryItemModel employeeTerritoryItemModel = new EmployeeTerritoryItemModel(ActivityOperations, "Delete", masterEmployeeId, masterTerritoryId);
            
            try
            {
                if (IsDelete(employeeTerritoryItemModel.OperationResult))
                {            
                    EmployeeTerritoryDTO employeeTerritoryDTO = Application.GetById(employeeTerritoryItemModel.OperationResult, new object[] { employeeId, territoryId });
                    if (employeeTerritoryDTO != null)
                    {
                        employeeTerritoryItemModel.EmployeeTerritory = new EmployeeTerritoryViewModel(employeeTerritoryDTO);

                        return PartialView("CRUD", employeeTerritoryItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                employeeTerritoryItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(employeeTerritoryItemModel.OperationResult);
        }

        // POST: EmployeeTerritory/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EmployeeTerritoryItemModel employeeTerritoryItemModel)
        {
            try
            {
                if (IsDelete(employeeTerritoryItemModel.OperationResult))
                {
                    if (Application.Delete(employeeTerritoryItemModel.OperationResult, (EmployeeTerritoryDTO)employeeTerritoryItemModel.EmployeeTerritory.ToDTO()))
                    {
                        return JsonResultSuccess(employeeTerritoryItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                employeeTerritoryItemModel.OperationResult.ParseException(exception);
            }

            employeeTerritoryItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(employeeTerritoryItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: EmployeeTerritory/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<EmployeeTerritoryViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(EmployeeTerritory), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<EmployeeTerritoryViewModel, EmployeeTerritoryDTO, EmployeeTerritory>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: EmployeeTerritory/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, EmployeeTerritoryResources.EntitySingular + ".xlsx");
            }
        }

        // POST: EmployeeTerritory/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, EmployeeTerritoryResources.EntitySingular + ".pdf");
            }
        }

        // POST: EmployeeTerritory/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, EmployeeTerritoryResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}