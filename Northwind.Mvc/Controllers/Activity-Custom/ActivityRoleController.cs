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
    public partial class ActivityRoleController : BaseMvcControllerSCRUDApplication<ActivityRoleDTO, ActivityRole>
    {
        #region Methods

        public ActivityRoleController(IActivityGenericApplicationDTO<ActivityRoleDTO, ActivityRole> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: ActivityRole
        // GET: ActivityRole/Index
        [HttpGet]
        public ActionResult Index(string masterActivityId = null, string masterRoleName = null) // !?!
        {
            ActivityRoleCollectionModel activityRoleCollectionModel = new ActivityRoleCollectionModel(ActivityOperations, "Index", null, masterActivityId, masterRoleName); // !?!

            try
            {
                if (IsIndex(activityRoleCollectionModel.OperationResult))
                {
                    return View(activityRoleCollectionModel);
                }
            }
            catch (Exception exception)
            {
                activityRoleCollectionModel.OperationResult.ParseException(exception);
            }

            return View("OperationResult", new OperationResultViewModel(activityRoleCollectionModel.OperationResult));
        }

        // GET & POST: ActivityRole/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, string masterActivityId = null, string masterRoleName = null) // !?!
        {
            ActivityRoleCollectionModel activityRoleCollectionModel = new ActivityRoleCollectionModel(ActivityOperations, "Search", masterControllerAction, masterActivityId, masterRoleName); // !?!

            try
            {
                if (IsOperation(activityRoleCollectionModel.OperationResult))
                {
                    return PartialView(activityRoleCollectionModel);
                }
            }
            catch (Exception exception)
            {
                activityRoleCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(activityRoleCollectionModel.OperationResult);
        }

        // GET & POST: ActivityRole/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_ActivityRoleLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: ActivityRole/Create
        [HttpGet]
        public ActionResult Create(string masterActivityId = null, string masterRoleName = null) // !?!
        {
            ActivityRoleItemModel activityRoleItemModel = new ActivityRoleItemModel(ActivityOperations, "Create", masterActivityId, masterRoleName); // !?!

            try
            {
                if (IsCreate(activityRoleItemModel.OperationResult))
                {
                    return PartialView("CRUD", activityRoleItemModel);
                }            
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(activityRoleItemModel.OperationResult);
        }

        // POST: ActivityRole/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityRoleItemModel activityRoleItemModel)
        {
            try
            {
                if (IsCreate(activityRoleItemModel.OperationResult))
                {
                    if (IsValid(activityRoleItemModel.OperationResult, activityRoleItemModel.ActivityRole))
                    {
                        ActivityRoleDTO activityRoleDTO = (ActivityRoleDTO)activityRoleItemModel.ActivityRole.ToDTO();
                        if (Application.Create(activityRoleItemModel.OperationResult, activityRoleDTO))
                        {
                            if (activityRoleItemModel.IsSave)
                            {
                                activityRoleItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(activityRoleItemModel.OperationResult,
                                    Url.Action("Update", "ActivityRole", new { ActivityId = activityRoleDTO.ActivityId, RoleName = activityRoleDTO.RoleName }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(activityRoleItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }

            activityRoleItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(activityRoleItemModel.OperationResult);
        }

        // GET: ActivityRole/Read/1
        [HttpGet]
        public ActionResult Read(string activityId, string roleName, string masterActivityId = null, string masterRoleName = null) // !?!
        {
            ActivityRoleItemModel activityRoleItemModel = new ActivityRoleItemModel(ActivityOperations, "Read", masterActivityId, masterRoleName); // !?!
            
            try
            {
                if (IsRead(activityRoleItemModel.OperationResult))
                {
                    ActivityRoleDTO activityRoleDTO = Application.GetById(activityRoleItemModel.OperationResult, new object[] { activityId, roleName });
                    if (activityRoleDTO != null)
                    {
                        activityRoleItemModel.ActivityRole = new ActivityRoleViewModel(activityRoleDTO);                    

                        return PartialView("CRUD", activityRoleItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(activityRoleItemModel.OperationResult);
        }

        // GET: ActivityRole/Update/1
        [HttpGet]
        public ActionResult Update(string activityId, string roleName, string masterActivityId = null, string masterRoleName = null) // !?!
        {
            ActivityRoleItemModel activityRoleItemModel = new ActivityRoleItemModel(ActivityOperations, "Update", masterActivityId, masterRoleName); // !?!
            
            try
            {
                if (IsUpdate(activityRoleItemModel.OperationResult))
                {            
                    ActivityRoleDTO activityRoleDTO = Application.GetById(activityRoleItemModel.OperationResult, new object[] { activityId, roleName });
                    if (activityRoleDTO != null)
                    {
                        activityRoleItemModel.ActivityRole = new ActivityRoleViewModel(activityRoleDTO);

                        return PartialView("CRUD", activityRoleItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(activityRoleItemModel.OperationResult);
        }

        // POST: ActivityRole/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ActivityRoleItemModel activityRoleItemModel)
        {
            try
            {
                if (IsUpdate(activityRoleItemModel.OperationResult))
                {
                    if (IsValid(activityRoleItemModel.OperationResult, activityRoleItemModel.ActivityRole))
                    {
                        ActivityRoleDTO activityRoleDTO = (ActivityRoleDTO)activityRoleItemModel.ActivityRole.ToDTO();
                        if (Application.Update(activityRoleItemModel.OperationResult, activityRoleDTO))
                        {
                            if (activityRoleItemModel.IsSave)
                            {
                                return JsonResultSuccess(activityRoleItemModel.OperationResult,
                                    Url.Action("Update", "ActivityRole", new { ActivityId = activityRoleDTO.ActivityId, RoleName = activityRoleDTO.RoleName }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(activityRoleItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }

            activityRoleItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(activityRoleItemModel.OperationResult);
        }

        // GET: ActivityRole/Delete/1
        [HttpGet]
        public ActionResult Delete(string activityId, string roleName, string masterActivityId = null, string masterRoleName = null) // !?!
        {
            ActivityRoleItemModel activityRoleItemModel = new ActivityRoleItemModel(ActivityOperations, "Delete", masterActivityId, masterRoleName); // !?!
            
            try
            {
                if (IsDelete(activityRoleItemModel.OperationResult))
                {            
                    ActivityRoleDTO activityRoleDTO = Application.GetById(activityRoleItemModel.OperationResult, new object[] { activityId, roleName });
                    if (activityRoleDTO != null)
                    {
                        activityRoleItemModel.ActivityRole = new ActivityRoleViewModel(activityRoleDTO);

                        return PartialView("CRUD", activityRoleItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(activityRoleItemModel.OperationResult);
        }

        // POST: ActivityRole/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ActivityRoleItemModel activityRoleItemModel)
        {
            try
            {
                if (IsDelete(activityRoleItemModel.OperationResult))
                {
                    if (Application.Delete(activityRoleItemModel.OperationResult, (ActivityRoleDTO)activityRoleItemModel.ActivityRole.ToDTO()))
                    {
                        return JsonResultSuccess(activityRoleItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }

            activityRoleItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(activityRoleItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: ActivityRole/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            SyncfusionDataResult dataResult = new SyncfusionDataResult();
            dataResult.result = new List<ActivityRoleViewModel>();

            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(ActivityRole), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    dataResult.result = ZViewHelper<ActivityRoleViewModel, ActivityRoleDTO, ActivityRole>.ToViewList(Application.Search(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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
                    throw new InvalidOperationException(operationResult.Text);
                }
            }

            return Json(JsonConvert.SerializeObject(dataResult), JsonRequestBehavior.AllowGet);
        } 

        // POST: ActivityRole/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, ActivityRoleResources.EntitySingular + ".xlsx");
            }
        }

        // POST: ActivityRole/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, ActivityRoleResources.EntitySingular + ".pdf");
            }
        }

        // POST: ActivityRole/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, ActivityRoleResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}