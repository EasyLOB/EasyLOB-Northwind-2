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
    public partial class RegionController : BaseMvcControllerSCRUDApplication<RegionDTO, Region>
    {
        #region Methods

        public RegionController(INorthwindGenericApplicationDTO<RegionDTO, Region> application)
        {
            Application = application;            
        }

        #endregion
        
        #region Methods SCRUD

        // GET: Region
        // GET: Region/Index
        [HttpGet]
        public ActionResult Index()
        {
            RegionCollectionModel regionCollectionModel = new RegionCollectionModel(ActivityOperations, "Index", null);

            try
            {
                IsOperation(regionCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                regionCollectionModel.OperationResult.ParseException(exception);
            }

            return View(regionCollectionModel);
        }        

        // GET & POST: Region/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            RegionCollectionModel regionCollectionModel = new RegionCollectionModel(ActivityOperations, "Search", masterControllerAction);

            try
            {
                if (IsOperation(regionCollectionModel.OperationResult))
                {
                    return PartialView(regionCollectionModel);
                }
            }
            catch (Exception exception)
            {
                regionCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(regionCollectionModel.OperationResult);
        }

        // GET & POST: Region/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_RegionLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: Region/Create
        [HttpGet]
        public ActionResult Create()
        {
            RegionItemModel regionItemModel = new RegionItemModel(ActivityOperations, "Create");

            try
            {
                if (IsCreate(regionItemModel.OperationResult))
                {
                    return PartialView("CRUD", regionItemModel);
                }            
            }
            catch (Exception exception)
            {
                regionItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(regionItemModel.OperationResult);
        }

        // POST: Region/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegionItemModel regionItemModel)
        {
            try
            {
                if (IsCreate(regionItemModel.OperationResult))
                {
                    if (IsValid(regionItemModel.OperationResult, regionItemModel.Region))
                    {
                        RegionDTO regionDTO = (RegionDTO)regionItemModel.Region.ToDTO();
                        if (Application.Create(regionItemModel.OperationResult, regionDTO))
                        {
                            if (regionItemModel.IsSave)
                            {
                                regionItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(regionItemModel.OperationResult,
                                    Url.Content(String.Format("~/Region/Update?RegionId={0}", regionDTO.RegionId)));
                            }
                            else
                            {
                                return JsonResultSuccess(regionItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                regionItemModel.OperationResult.ParseException(exception);
            }

            regionItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(regionItemModel.OperationResult);
        }

        // GET: Region/Read/1
        [HttpGet]
        public ActionResult Read(int regionId)
        {
            RegionItemModel regionItemModel = new RegionItemModel(ActivityOperations, "Read");
            
            try
            {
                if (IsRead(regionItemModel.OperationResult))
                {
                    RegionDTO regionDTO = Application.GetById(regionItemModel.OperationResult, new object[] { regionId });
                    if (regionDTO != null)
                    {
                        regionItemModel.Region = new RegionViewModel(regionDTO);                    

                        return PartialView("CRUD", regionItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                regionItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(regionItemModel.OperationResult);
        }

        // GET: Region/Update/1
        [HttpGet]
        public ActionResult Update(int regionId)
        {
            RegionItemModel regionItemModel = new RegionItemModel(ActivityOperations, "Update");
            
            try
            {
                if (IsUpdate(regionItemModel.OperationResult))
                {            
                    RegionDTO regionDTO = Application.GetById(regionItemModel.OperationResult, new object[] { regionId });
                    if (regionDTO != null)
                    {
                        regionItemModel.Region = new RegionViewModel(regionDTO);

                        return PartialView("CRUD", regionItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                regionItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(regionItemModel.OperationResult);
        }

        // POST: Region/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(RegionItemModel regionItemModel)
        {
            try
            {
                if (IsUpdate(regionItemModel.OperationResult))
                {
                    if (IsValid(regionItemModel.OperationResult, regionItemModel.Region))
                    {
                        RegionDTO regionDTO = (RegionDTO)regionItemModel.Region.ToDTO();
                        if (Application.Update(regionItemModel.OperationResult, regionDTO))
                        {
                            if (regionItemModel.IsSave)
                            {
                                return JsonResultSuccess(regionItemModel.OperationResult,
                                    Url.Content(String.Format("~/Region/Update?RegionId={0}", regionDTO.RegionId)));
                            }
                            else
                            {
                                return JsonResultSuccess(regionItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                regionItemModel.OperationResult.ParseException(exception);
            }

            regionItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(regionItemModel.OperationResult);
        }

        // GET: Region/Delete/1
        [HttpGet]
        public ActionResult Delete(int regionId)
        {
            RegionItemModel regionItemModel = new RegionItemModel(ActivityOperations, "Delete");
            
            try
            {
                if (IsDelete(regionItemModel.OperationResult))
                {            
                    RegionDTO regionDTO = Application.GetById(regionItemModel.OperationResult, new object[] { regionId });
                    if (regionDTO != null)
                    {
                        regionItemModel.Region = new RegionViewModel(regionDTO);

                        return PartialView("CRUD", regionItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                regionItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(regionItemModel.OperationResult);
        }

        // POST: Region/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(RegionItemModel regionItemModel)
        {
            try
            {
                if (IsDelete(regionItemModel.OperationResult))
                {
                    if (Application.Delete(regionItemModel.OperationResult, (RegionDTO)regionItemModel.Region.ToDTO()))
                    {
                        return JsonResultSuccess(regionItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                regionItemModel.OperationResult.ParseException(exception);
            }

            regionItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(regionItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Region/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<RegionViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Region), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<RegionViewModel, RegionDTO, Region>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: Region/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, RegionResources.EntitySingular + ".xlsx");
            }
        }

        // POST: Region/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, RegionResources.EntitySingular + ".pdf");
            }
        }

        // POST: Region/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, RegionResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}