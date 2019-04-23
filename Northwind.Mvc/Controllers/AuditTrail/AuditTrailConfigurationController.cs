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
    public partial class AuditTrailConfigurationController : BaseMvcControllerSCRUDApplication<AuditTrailConfigurationDTO, AuditTrailConfiguration>
    {
        #region Methods

        public AuditTrailConfigurationController(IAuditTrailGenericApplicationDTO<AuditTrailConfigurationDTO, AuditTrailConfiguration> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: AuditTrailConfiguration
        // GET: AuditTrailConfiguration/Index
        [HttpGet]
        public ActionResult Index()
        {
            AuditTrailConfigurationCollectionModel auditTrailConfigurationCollectionModel = new AuditTrailConfigurationCollectionModel(ActivityOperations, "Index", null);

            try
            {
                if (IsIndex(auditTrailConfigurationCollectionModel.OperationResult))
                {
                    return View(auditTrailConfigurationCollectionModel);
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationCollectionModel.OperationResult.ParseException(exception);
            }

            return View("OperationResult", new OperationResultViewModel(auditTrailConfigurationCollectionModel.OperationResult));
        }        

        // GET & POST: AuditTrailConfiguration/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            AuditTrailConfigurationCollectionModel auditTrailConfigurationCollectionModel = new AuditTrailConfigurationCollectionModel(ActivityOperations, "Search", masterControllerAction);

            try
            {
                if (IsOperation(auditTrailConfigurationCollectionModel.OperationResult))
                {
                    return PartialView(auditTrailConfigurationCollectionModel);
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(auditTrailConfigurationCollectionModel.OperationResult);
        }

        // GET & POST: AuditTrailConfiguration/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_AuditTrailConfigurationLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: AuditTrailConfiguration/Create
        [HttpGet]
        public ActionResult Create()
        {
            AuditTrailConfigurationItemModel auditTrailConfigurationItemModel = new AuditTrailConfigurationItemModel(ActivityOperations, "Create");

            try
            {
                if (IsCreate(auditTrailConfigurationItemModel.OperationResult))
                {
                    return PartialView("CRUD", auditTrailConfigurationItemModel);
                }            
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(auditTrailConfigurationItemModel.OperationResult);
        }

        // POST: AuditTrailConfiguration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuditTrailConfigurationItemModel auditTrailConfigurationItemModel)
        {
            try
            {
                if (IsCreate(auditTrailConfigurationItemModel.OperationResult))
                {
                    if (IsValid(auditTrailConfigurationItemModel.OperationResult, auditTrailConfigurationItemModel.AuditTrailConfiguration))
                    {
                        AuditTrailConfigurationDTO auditTrailConfigurationDTO = (AuditTrailConfigurationDTO)auditTrailConfigurationItemModel.AuditTrailConfiguration.ToDTO();
                        if (Application.Create(auditTrailConfigurationItemModel.OperationResult, auditTrailConfigurationDTO))
                        {
                            if (auditTrailConfigurationItemModel.IsSave)
                            {
                                auditTrailConfigurationItemModel.OperationResult.InformationMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(auditTrailConfigurationItemModel.OperationResult,
                                    Url.Action("Update", "AuditTrailConfiguration", new { Id = auditTrailConfigurationDTO.Id }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(auditTrailConfigurationItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }

            auditTrailConfigurationItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(auditTrailConfigurationItemModel.OperationResult);
        }

        // GET: AuditTrailConfiguration/Read/1
        [HttpGet]
        public ActionResult Read(int id)
        {
            AuditTrailConfigurationItemModel auditTrailConfigurationItemModel = new AuditTrailConfigurationItemModel(ActivityOperations, "Read");
            
            try
            {
                if (IsRead(auditTrailConfigurationItemModel.OperationResult))
                {
                    AuditTrailConfigurationDTO auditTrailConfigurationDTO = Application.GetById(auditTrailConfigurationItemModel.OperationResult, new object[] { id });
                    if (auditTrailConfigurationDTO != null)
                    {
                        auditTrailConfigurationItemModel.AuditTrailConfiguration = new AuditTrailConfigurationViewModel(auditTrailConfigurationDTO);                    

                        return PartialView("CRUD", auditTrailConfigurationItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(auditTrailConfigurationItemModel.OperationResult);
        }

        // GET: AuditTrailConfiguration/Update/1
        [HttpGet]
        public ActionResult Update(int id)
        {
            AuditTrailConfigurationItemModel auditTrailConfigurationItemModel = new AuditTrailConfigurationItemModel(ActivityOperations, "Update");
            
            try
            {
                if (IsUpdate(auditTrailConfigurationItemModel.OperationResult))
                {            
                    AuditTrailConfigurationDTO auditTrailConfigurationDTO = Application.GetById(auditTrailConfigurationItemModel.OperationResult, new object[] { id });
                    if (auditTrailConfigurationDTO != null)
                    {
                        auditTrailConfigurationItemModel.AuditTrailConfiguration = new AuditTrailConfigurationViewModel(auditTrailConfigurationDTO);

                        return PartialView("CRUD", auditTrailConfigurationItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(auditTrailConfigurationItemModel.OperationResult);
        }

        // POST: AuditTrailConfiguration/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(AuditTrailConfigurationItemModel auditTrailConfigurationItemModel)
        {
            try
            {
                if (IsUpdate(auditTrailConfigurationItemModel.OperationResult))
                {
                    if (IsValid(auditTrailConfigurationItemModel.OperationResult, auditTrailConfigurationItemModel.AuditTrailConfiguration))
                    {
                        AuditTrailConfigurationDTO auditTrailConfigurationDTO = (AuditTrailConfigurationDTO)auditTrailConfigurationItemModel.AuditTrailConfiguration.ToDTO();
                        if (Application.Update(auditTrailConfigurationItemModel.OperationResult, auditTrailConfigurationDTO))
                        {
                            if (auditTrailConfigurationItemModel.IsSave)
                            {
                                return JsonResultSuccess(auditTrailConfigurationItemModel.OperationResult,
                                    Url.Action("Update", "AuditTrailConfiguration", new { Id = auditTrailConfigurationDTO.Id }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(auditTrailConfigurationItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }

            auditTrailConfigurationItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(auditTrailConfigurationItemModel.OperationResult);
        }

        // GET: AuditTrailConfiguration/Delete/1
        [HttpGet]
        public ActionResult Delete(int id)
        {
            AuditTrailConfigurationItemModel auditTrailConfigurationItemModel = new AuditTrailConfigurationItemModel(ActivityOperations, "Delete");
            
            try
            {
                if (IsDelete(auditTrailConfigurationItemModel.OperationResult))
                {            
                    AuditTrailConfigurationDTO auditTrailConfigurationDTO = Application.GetById(auditTrailConfigurationItemModel.OperationResult, new object[] { id });
                    if (auditTrailConfigurationDTO != null)
                    {
                        auditTrailConfigurationItemModel.AuditTrailConfiguration = new AuditTrailConfigurationViewModel(auditTrailConfigurationDTO);

                        return PartialView("CRUD", auditTrailConfigurationItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(auditTrailConfigurationItemModel.OperationResult);
        }

        // POST: AuditTrailConfiguration/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AuditTrailConfigurationItemModel auditTrailConfigurationItemModel)
        {
            try
            {
                if (IsDelete(auditTrailConfigurationItemModel.OperationResult))
                {
                    if (Application.Delete(auditTrailConfigurationItemModel.OperationResult, (AuditTrailConfigurationDTO)auditTrailConfigurationItemModel.AuditTrailConfiguration.ToDTO()))
                    {
                        return JsonResultSuccess(auditTrailConfigurationItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }

            auditTrailConfigurationItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(auditTrailConfigurationItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: AuditTrailConfiguration/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            SyncfusionDataResult dataResult = new SyncfusionDataResult();
            dataResult.result = new List<AuditTrailConfigurationViewModel>();

            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(AuditTrailConfiguration), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    dataResult.result = ZViewHelper<AuditTrailConfigurationViewModel, AuditTrailConfigurationDTO, AuditTrailConfiguration>.ToViewList(Application.Search(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: AuditTrailConfiguration/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, AuditTrailConfigurationResources.EntitySingular + ".xlsx");
            }
        }

        // POST: AuditTrailConfiguration/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, AuditTrailConfigurationResources.EntitySingular + ".pdf");
            }
        }

        // POST: AuditTrailConfiguration/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, AuditTrailConfigurationResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}