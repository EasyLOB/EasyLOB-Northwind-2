using EasyLOB.AuditTrail;
using EasyLOB.AuditTrail.Data;
using EasyLOB.AuditTrail.Data.Resources;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Mvc;
using Newtonsoft.Json;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyLOB.AuditTrail.Mvc
{
    public partial class AuditTrailLogController : BaseMvcControllerSCRUDApplication<AuditTrailLogDTO, AuditTrailLog>
    {
        #region Methods

        public AuditTrailLogController(IAuditTrailGenericApplicationDTO<AuditTrailLogDTO, AuditTrailLog> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: AuditTrailLog
        // GET: AuditTrailLog/Index
        [HttpGet]
        public ActionResult Index()
        {
            AuditTrailLogCollectionModel auditTrailLogCollectionModel = new AuditTrailLogCollectionModel(ActivityOperations, "Index", null);

            try
            {
                if (IsIndex(auditTrailLogCollectionModel.OperationResult))
                {
                    return View(auditTrailLogCollectionModel);
                }
            }
            catch (Exception exception)
            {
                auditTrailLogCollectionModel.OperationResult.ParseException(exception);
            }

            return View("OperationResult", new OperationResultViewModel(auditTrailLogCollectionModel.OperationResult));
        }        

        // GET & POST: AuditTrailLog/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            AuditTrailLogCollectionModel auditTrailLogCollectionModel = new AuditTrailLogCollectionModel(ActivityOperations, "Search", masterControllerAction);

            try
            {
                if (IsOperation(auditTrailLogCollectionModel.OperationResult))
                {
                    return PartialView(auditTrailLogCollectionModel);
                }
            }
            catch (Exception exception)
            {
                auditTrailLogCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(auditTrailLogCollectionModel.OperationResult);
        }

        // GET & POST: AuditTrailLog/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_AuditTrailLogLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: AuditTrailLog/Create
        [HttpGet]
        public ActionResult Create()
        {
            AuditTrailLogItemModel auditTrailLogItemModel = new AuditTrailLogItemModel(ActivityOperations, "Create");

            try
            {
                if (IsCreate(auditTrailLogItemModel.OperationResult))
                {
                    return PartialView("CRUD", auditTrailLogItemModel);
                }            
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(auditTrailLogItemModel.OperationResult);
        }

        // POST: AuditTrailLog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuditTrailLogItemModel auditTrailLogItemModel)
        {
            try
            {
                if (IsCreate(auditTrailLogItemModel.OperationResult))
                {
                    if (IsValid(auditTrailLogItemModel.OperationResult, auditTrailLogItemModel.AuditTrailLog))
                    {
                        AuditTrailLogDTO auditTrailLogDTO = (AuditTrailLogDTO)auditTrailLogItemModel.AuditTrailLog.ToDTO();
                        if (Application.Create(auditTrailLogItemModel.OperationResult, auditTrailLogDTO))
                        {
                            if (auditTrailLogItemModel.IsSave)
                            {
                                auditTrailLogItemModel.OperationResult.InformationMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(auditTrailLogItemModel.OperationResult,
                                    Url.Action("Update", "AuditTrailLog", new { Id = auditTrailLogDTO.Id }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(auditTrailLogItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }

            auditTrailLogItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(auditTrailLogItemModel.OperationResult);
        }

        // GET: AuditTrailLog/Read/1
        [HttpGet]
        public ActionResult Read(int id)
        {
            AuditTrailLogItemModel auditTrailLogItemModel = new AuditTrailLogItemModel(ActivityOperations, "Read");
            
            try
            {
                if (IsRead(auditTrailLogItemModel.OperationResult))
                {
                    AuditTrailLogDTO auditTrailLogDTO = Application.GetById(auditTrailLogItemModel.OperationResult, new object[] { id });
                    if (auditTrailLogDTO != null)
                    {
                        auditTrailLogItemModel.AuditTrailLog = new AuditTrailLogViewModel(auditTrailLogDTO);                    

                        return PartialView("CRUD", auditTrailLogItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(auditTrailLogItemModel.OperationResult);
        }

        // GET: AuditTrailLog/Update/1
        [HttpGet]
        public ActionResult Update(int id)
        {
            AuditTrailLogItemModel auditTrailLogItemModel = new AuditTrailLogItemModel(ActivityOperations, "Update");
            
            try
            {
                if (IsUpdate(auditTrailLogItemModel.OperationResult))
                {            
                    AuditTrailLogDTO auditTrailLogDTO = Application.GetById(auditTrailLogItemModel.OperationResult, new object[] { id });
                    if (auditTrailLogDTO != null)
                    {
                        auditTrailLogItemModel.AuditTrailLog = new AuditTrailLogViewModel(auditTrailLogDTO);

                        return PartialView("CRUD", auditTrailLogItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(auditTrailLogItemModel.OperationResult);
        }

        // POST: AuditTrailLog/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(AuditTrailLogItemModel auditTrailLogItemModel)
        {
            try
            {
                if (IsUpdate(auditTrailLogItemModel.OperationResult))
                {
                    if (IsValid(auditTrailLogItemModel.OperationResult, auditTrailLogItemModel.AuditTrailLog))
                    {
                        AuditTrailLogDTO auditTrailLogDTO = (AuditTrailLogDTO)auditTrailLogItemModel.AuditTrailLog.ToDTO();
                        if (Application.Update(auditTrailLogItemModel.OperationResult, auditTrailLogDTO))
                        {
                            if (auditTrailLogItemModel.IsSave)
                            {
                                return JsonResultSuccess(auditTrailLogItemModel.OperationResult,
                                    Url.Action("Update", "AuditTrailLog", new { Id = auditTrailLogDTO.Id }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(auditTrailLogItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }

            auditTrailLogItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(auditTrailLogItemModel.OperationResult);
        }

        // GET: AuditTrailLog/Delete/1
        [HttpGet]
        public ActionResult Delete(int id)
        {
            AuditTrailLogItemModel auditTrailLogItemModel = new AuditTrailLogItemModel(ActivityOperations, "Delete");
            
            try
            {
                if (IsDelete(auditTrailLogItemModel.OperationResult))
                {            
                    AuditTrailLogDTO auditTrailLogDTO = Application.GetById(auditTrailLogItemModel.OperationResult, new object[] { id });
                    if (auditTrailLogDTO != null)
                    {
                        auditTrailLogItemModel.AuditTrailLog = new AuditTrailLogViewModel(auditTrailLogDTO);

                        return PartialView("CRUD", auditTrailLogItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(auditTrailLogItemModel.OperationResult);
        }

        // POST: AuditTrailLog/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AuditTrailLogItemModel auditTrailLogItemModel)
        {
            try
            {
                if (IsDelete(auditTrailLogItemModel.OperationResult))
                {
                    if (Application.Delete(auditTrailLogItemModel.OperationResult, (AuditTrailLogDTO)auditTrailLogItemModel.AuditTrailLog.ToDTO()))
                    {
                        return JsonResultSuccess(auditTrailLogItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }

            auditTrailLogItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(auditTrailLogItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: AuditTrailLog/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            SyncfusionDataResult dataResult = new SyncfusionDataResult();
            dataResult.result = new List<AuditTrailLogViewModel>();

            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(AuditTrailLog), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    dataResult.result = ZViewHelper<AuditTrailLogViewModel, AuditTrailLogDTO, AuditTrailLog>.ToViewList(Application.Search(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: AuditTrailLog/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, AuditTrailLogResources.EntitySingular + ".xlsx");
            }
        }

        // POST: AuditTrailLog/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, AuditTrailLogResources.EntitySingular + ".pdf");
            }
        }

        // POST: AuditTrailLog/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, AuditTrailLogResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}