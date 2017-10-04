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
    public partial class TerritoryController : BaseMvcControllerSCRUDApplication<TerritoryDTO, Territory>
    {
        #region Methods

        public TerritoryController(INorthwindGenericApplicationDTO<TerritoryDTO, Territory> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: Territory
        // GET: Territory/Index
        [HttpGet]
        public ActionResult Index(int? masterRegionId = null)
        {
            TerritoryCollectionModel territoryCollectionModel = new TerritoryCollectionModel(ActivityOperations, "Index", null, masterRegionId);

            try
            {
                IsOperation(territoryCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                territoryCollectionModel.OperationResult.ParseException(exception);
            }

            return View(territoryCollectionModel);
        }        

        // GET & POST: Territory/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, int? masterRegionId = null)
        {
            TerritoryCollectionModel territoryCollectionModel = new TerritoryCollectionModel(ActivityOperations, "Search", masterControllerAction, masterRegionId);

            try
            {
                if (IsOperation(territoryCollectionModel.OperationResult))
                {
                    return PartialView(territoryCollectionModel);
                }
            }
            catch (Exception exception)
            {
                territoryCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(territoryCollectionModel.OperationResult);
        }

        // GET & POST: Territory/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_TerritoryLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: Territory/Create
        [HttpGet]
        public ActionResult Create(int? masterRegionId = null)
        {
            TerritoryItemModel territoryItemModel = new TerritoryItemModel(ActivityOperations, "Create", masterRegionId);

            try
            {
                if (IsCreate(territoryItemModel.OperationResult))
                {
                    return PartialView("CRUD", territoryItemModel);
                }            
            }
            catch (Exception exception)
            {
                territoryItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(territoryItemModel.OperationResult);
        }

        // POST: Territory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TerritoryItemModel territoryItemModel)
        {
            try
            {
                if (IsCreate(territoryItemModel.OperationResult))
                {
                    if (IsValid(territoryItemModel.OperationResult, territoryItemModel.Territory))
                    {
                        TerritoryDTO territoryDTO = (TerritoryDTO)territoryItemModel.Territory.ToDTO();
                        if (Application.Create(territoryItemModel.OperationResult, territoryDTO))
                        {
                            if (territoryItemModel.IsSave)
                            {
                                territoryItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(territoryItemModel.OperationResult,
                                    Url.Action("Update", "Territory", new { TerritoryId = territoryDTO.TerritoryId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(territoryItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                territoryItemModel.OperationResult.ParseException(exception);
            }

            territoryItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(territoryItemModel.OperationResult);
        }

        // GET: Territory/Read/1
        [HttpGet]
        public ActionResult Read(string territoryId, int? masterRegionId = null)
        {
            TerritoryItemModel territoryItemModel = new TerritoryItemModel(ActivityOperations, "Read", masterRegionId);
            
            try
            {
                if (IsRead(territoryItemModel.OperationResult))
                {
                    TerritoryDTO territoryDTO = Application.GetById(territoryItemModel.OperationResult, new object[] { territoryId });
                    if (territoryDTO != null)
                    {
                        territoryItemModel.Territory = new TerritoryViewModel(territoryDTO);                    

                        return PartialView("CRUD", territoryItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                territoryItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(territoryItemModel.OperationResult);
        }

        // GET: Territory/Update/1
        [HttpGet]
        public ActionResult Update(string territoryId, int? masterRegionId = null)
        {
            TerritoryItemModel territoryItemModel = new TerritoryItemModel(ActivityOperations, "Update", masterRegionId);
            
            try
            {
                if (IsUpdate(territoryItemModel.OperationResult))
                {            
                    TerritoryDTO territoryDTO = Application.GetById(territoryItemModel.OperationResult, new object[] { territoryId });
                    if (territoryDTO != null)
                    {
                        territoryItemModel.Territory = new TerritoryViewModel(territoryDTO);

                        return PartialView("CRUD", territoryItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                territoryItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(territoryItemModel.OperationResult);
        }

        // POST: Territory/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(TerritoryItemModel territoryItemModel)
        {
            try
            {
                if (IsUpdate(territoryItemModel.OperationResult))
                {
                    if (IsValid(territoryItemModel.OperationResult, territoryItemModel.Territory))
                    {
                        TerritoryDTO territoryDTO = (TerritoryDTO)territoryItemModel.Territory.ToDTO();
                        if (Application.Update(territoryItemModel.OperationResult, territoryDTO))
                        {
                            if (territoryItemModel.IsSave)
                            {
                                return JsonResultSuccess(territoryItemModel.OperationResult,
                                    Url.Action("Update", "Territory", new { TerritoryId = territoryDTO.TerritoryId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(territoryItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                territoryItemModel.OperationResult.ParseException(exception);
            }

            territoryItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(territoryItemModel.OperationResult);
        }

        // GET: Territory/Delete/1
        [HttpGet]
        public ActionResult Delete(string territoryId, int? masterRegionId = null)
        {
            TerritoryItemModel territoryItemModel = new TerritoryItemModel(ActivityOperations, "Delete", masterRegionId);
            
            try
            {
                if (IsDelete(territoryItemModel.OperationResult))
                {            
                    TerritoryDTO territoryDTO = Application.GetById(territoryItemModel.OperationResult, new object[] { territoryId });
                    if (territoryDTO != null)
                    {
                        territoryItemModel.Territory = new TerritoryViewModel(territoryDTO);

                        return PartialView("CRUD", territoryItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                territoryItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(territoryItemModel.OperationResult);
        }

        // POST: Territory/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TerritoryItemModel territoryItemModel)
        {
            try
            {
                if (IsDelete(territoryItemModel.OperationResult))
                {
                    if (Application.Delete(territoryItemModel.OperationResult, (TerritoryDTO)territoryItemModel.Territory.ToDTO()))
                    {
                        return JsonResultSuccess(territoryItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                territoryItemModel.OperationResult.ParseException(exception);
            }

            territoryItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(territoryItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Territory/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<TerritoryViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Territory), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<TerritoryViewModel, TerritoryDTO, Territory>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
                    if (dataManager.RequiresCounts)
                    {
                        countAll = Application.Count(where, args.ToArray());
                    }
                }
                catch (Exception exception)
                {
                    operationResult.ParseException(exception);
                }
            }

            if (!operationResult.Ok)
            {
                throw new InvalidOperationException(operationResult.Text);
            }

            return Json(JsonConvert.SerializeObject(new { result = data, count = countAll }), JsonRequestBehavior.AllowGet);
        } 

        // POST: Territory/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, TerritoryResources.EntitySingular + ".xlsx");
            }
        }

        // POST: Territory/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, TerritoryResources.EntitySingular + ".pdf");
            }
        }

        // POST: Territory/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, TerritoryResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}