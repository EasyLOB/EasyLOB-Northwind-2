using EasyLOB.Activity;
using EasyLOB.Activity.Data;
using EasyLOB.Activity.Data.Resources;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Mvc;
using Newtonsoft.Json;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyLOB.Activity.Mvc
{
    public partial class ActivityController : BaseMvcControllerSCRUDApplication<ActivityDTO, EasyLOB.Activity.Data.Activity> // !?!
    {
        #region Methods

        public ActivityController(IActivityGenericApplicationDTO<ActivityDTO, EasyLOB.Activity.Data.Activity> application) // !?!
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: Activity
        // GET: Activity/Index
        [HttpGet]
        public ActionResult Index()
        {
            ActivityCollectionModel activityCollectionModel = new ActivityCollectionModel(ActivityOperations, "Index", null);

            try
            {
                if (IsIndex(activityCollectionModel.OperationResult))
                {
                    return View(activityCollectionModel);
                }
            }
            catch (Exception exception)
            {
                activityCollectionModel.OperationResult.ParseException(exception);
            }

            return View("OperationResult", new OperationResultModel(activityCollectionModel.OperationResult));
        }        

        // GET & POST: Activity/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            ActivityCollectionModel activityCollectionModel = new ActivityCollectionModel(ActivityOperations, "Search", masterControllerAction);

            try
            {
                if (IsOperation(activityCollectionModel.OperationResult))
                {
                    return PartialView(activityCollectionModel);
                }
            }
            catch (Exception exception)
            {
                activityCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(activityCollectionModel.OperationResult);
        }

        // GET & POST: Activity/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_ActivityLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: Activity/Create
        [HttpGet]
        public ActionResult Create()
        {
            ActivityItemModel activityItemModel = new ActivityItemModel(ActivityOperations, "Create");

            try
            {
                if (IsCreate(activityItemModel.OperationResult))
                {
                    return PartialView("CRUD", activityItemModel);
                }            
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(activityItemModel.OperationResult);
        }

        // POST: Activity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityItemModel activityItemModel)
        {
            try
            {
                if (IsCreate(activityItemModel.OperationResult))
                {
                    if (IsValid(activityItemModel.OperationResult, activityItemModel.Activity))
                    {
                        ActivityDTO activityDTO = (ActivityDTO)activityItemModel.Activity.ToDTO();
                        if (Application.Create(activityItemModel.OperationResult, activityDTO))
                        {
                            if (activityItemModel.IsSave)
                            {
                                activityItemModel.OperationResult.InformationMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(activityItemModel.OperationResult,
                                    Url.Action("Update", "Activity", new { Id = activityDTO.Id }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(activityItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            activityItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(activityItemModel.OperationResult);
        }

        // GET: Activity/Read/1
        [HttpGet]
        public ActionResult Read(string id)
        {
            ActivityItemModel activityItemModel = new ActivityItemModel(ActivityOperations, "Read");
            
            try
            {
                if (IsRead(activityItemModel.OperationResult))
                {
                    ActivityDTO activityDTO = Application.GetById(activityItemModel.OperationResult, new object[] { id });
                    if (activityDTO != null)
                    {
                        activityItemModel.Activity = new ActivityViewModel(activityDTO);                    

                        return PartialView("CRUD", activityItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(activityItemModel.OperationResult);
        }

        // GET: Activity/Update/1
        [HttpGet]
        public ActionResult Update(string id)
        {
            ActivityItemModel activityItemModel = new ActivityItemModel(ActivityOperations, "Update");
            
            try
            {
                if (IsUpdate(activityItemModel.OperationResult))
                {            
                    ActivityDTO activityDTO = Application.GetById(activityItemModel.OperationResult, new object[] { id });
                    if (activityDTO != null)
                    {
                        activityItemModel.Activity = new ActivityViewModel(activityDTO);

                        return PartialView("CRUD", activityItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(activityItemModel.OperationResult);
        }

        // POST: Activity/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ActivityItemModel activityItemModel)
        {
            try
            {
                if (IsUpdate(activityItemModel.OperationResult))
                {
                    if (IsValid(activityItemModel.OperationResult, activityItemModel.Activity))
                    {
                        ActivityDTO activityDTO = (ActivityDTO)activityItemModel.Activity.ToDTO();
                        if (Application.Update(activityItemModel.OperationResult, activityDTO))
                        {
                            if (activityItemModel.IsSave)
                            {
                                return JsonResultSuccess(activityItemModel.OperationResult,
                                    Url.Action("Update", "Activity", new { Id = activityDTO.Id }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(activityItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            activityItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(activityItemModel.OperationResult);
        }

        // GET: Activity/Delete/1
        [HttpGet]
        public ActionResult Delete(string id)
        {
            ActivityItemModel activityItemModel = new ActivityItemModel(ActivityOperations, "Delete");
            
            try
            {
                if (IsDelete(activityItemModel.OperationResult))
                {            
                    ActivityDTO activityDTO = Application.GetById(activityItemModel.OperationResult, new object[] { id });
                    if (activityDTO != null)
                    {
                        activityItemModel.Activity = new ActivityViewModel(activityDTO);

                        return PartialView("CRUD", activityItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(activityItemModel.OperationResult);
        }

        // POST: Activity/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ActivityItemModel activityItemModel)
        {
            try
            {
                if (IsDelete(activityItemModel.OperationResult))
                {
                    if (Application.Delete(activityItemModel.OperationResult, (ActivityDTO)activityItemModel.Activity.ToDTO()))
                    {
                        return JsonResultSuccess(activityItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            activityItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(activityItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Activity/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            SyncfusionDataResult dataResult = new SyncfusionDataResult();
            dataResult.result = new List<ActivityViewModel>();

            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(EasyLOB.Activity.Data.Activity), Application.UnitOfWork.DBMS); // !?!
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    dataResult.result = ZViewHelper<ActivityViewModel, ActivityDTO, EasyLOB.Activity.Data.Activity>.ToViewList(Application.Search(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take)); // !?!
        
                    if (dataManager.RequiresCounts)
                    {
                        dataResult.count = Application.Count(where, args.ToArray());
                    }
                }
                catch (Exception exception)
                {
                    operationResult.ParseException(exception);
                }

                if (!operationResult.Ok)
                {
                    throw operationResult.Exception;
                }
            }

            return Json(JsonConvert.SerializeObject(dataResult), JsonRequestBehavior.AllowGet);
        } 

        // POST: Activity/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, ActivityResources.EntitySingular + ".xlsx");
            }
        }

        // POST: Activity/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, ActivityResources.EntitySingular + ".pdf");
            }
        }

        // POST: Activity/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, ActivityResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}