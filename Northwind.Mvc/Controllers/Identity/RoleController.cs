using EasyLOB.Identity;
using EasyLOB.Identity.Data;
using EasyLOB.Identity.Data.Resources;
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

namespace EasyLOB.Identity.Mvc
{
    public partial class RoleController : BaseMvcControllerSCRUDApplication<RoleDTO, Role>
    {
        #region Methods

        public RoleController(IIdentityGenericApplicationDTO<RoleDTO, Role> application)
        {
            Application = application;            
        }

        #endregion
        
        #region Methods SCRUD

        // GET: Role
        // GET: Role/Index
        [HttpGet]
        public ActionResult Index()
        {
            RoleCollectionModel roleCollectionModel = new RoleCollectionModel(ActivityOperations, "Index", null);

            try
            {
                IsOperation(roleCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                roleCollectionModel.OperationResult.ParseException(exception);
            }

            return View(roleCollectionModel);
        }        

        // GET & POST: Role/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            RoleCollectionModel roleCollectionModel = new RoleCollectionModel(ActivityOperations, "Search", masterControllerAction);

            try
            {
                if (IsOperation(roleCollectionModel.OperationResult))
                {
                    return PartialView(roleCollectionModel);
                }
            }
            catch (Exception exception)
            {
                roleCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(roleCollectionModel.OperationResult);
        }

        // GET & POST: Role/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_RoleLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: Role/Create
        [HttpGet]
        public ActionResult Create()
        {
            RoleItemModel roleItemModel = new RoleItemModel(ActivityOperations, "Create");

            try
            {
                if (IsCreate(roleItemModel.OperationResult))
                {
                    return PartialView("CRUD", roleItemModel);
                }            
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(roleItemModel.OperationResult);
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleItemModel roleItemModel)
        {
            try
            {
                if (IsCreate(roleItemModel.OperationResult))
                {
                    if (IsValid(roleItemModel.OperationResult, roleItemModel.Role))
                    {
                        RoleDTO roleDTO = (RoleDTO)roleItemModel.Role.ToDTO();
                        if (Application.Create(roleItemModel.OperationResult, roleDTO))
                        {
                            if (roleItemModel.IsSave)
                            {
                                roleItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(roleItemModel.OperationResult,
                                    Url.Action("Update", "Role", new { Id = roleDTO.Id }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(roleItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }

            roleItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(roleItemModel.OperationResult);
        }

        // GET: Role/Read/1
        [HttpGet]
        public ActionResult Read(string id)
        {
            RoleItemModel roleItemModel = new RoleItemModel(ActivityOperations, "Read");
            
            try
            {
                if (IsRead(roleItemModel.OperationResult))
                {
                    RoleDTO roleDTO = Application.GetById(roleItemModel.OperationResult, new object[] { id });
                    if (roleDTO != null)
                    {
                        roleItemModel.Role = new RoleViewModel(roleDTO);                    

                        return PartialView("CRUD", roleItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(roleItemModel.OperationResult);
        }

        // GET: Role/Update/1
        [HttpGet]
        public ActionResult Update(string id)
        {
            RoleItemModel roleItemModel = new RoleItemModel(ActivityOperations, "Update");
            
            try
            {
                if (IsUpdate(roleItemModel.OperationResult))
                {            
                    RoleDTO roleDTO = Application.GetById(roleItemModel.OperationResult, new object[] { id });
                    if (roleDTO != null)
                    {
                        roleItemModel.Role = new RoleViewModel(roleDTO);

                        return PartialView("CRUD", roleItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(roleItemModel.OperationResult);
        }

        // POST: Role/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(RoleItemModel roleItemModel)
        {
            try
            {
                if (IsUpdate(roleItemModel.OperationResult))
                {
                    if (IsValid(roleItemModel.OperationResult, roleItemModel.Role))
                    {
                        RoleDTO roleDTO = (RoleDTO)roleItemModel.Role.ToDTO();
                        if (Application.Update(roleItemModel.OperationResult, roleDTO))
                        {
                            if (roleItemModel.IsSave)
                            {
                                return JsonResultSuccess(roleItemModel.OperationResult,
                                    Url.Action("Update", "Role", new { Id = roleDTO.Id }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(roleItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }

            roleItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(roleItemModel.OperationResult);
        }

        // GET: Role/Delete/1
        [HttpGet]
        public ActionResult Delete(string id)
        {
            RoleItemModel roleItemModel = new RoleItemModel(ActivityOperations, "Delete");
            
            try
            {
                if (IsDelete(roleItemModel.OperationResult))
                {            
                    RoleDTO roleDTO = Application.GetById(roleItemModel.OperationResult, new object[] { id });
                    if (roleDTO != null)
                    {
                        roleItemModel.Role = new RoleViewModel(roleDTO);

                        return PartialView("CRUD", roleItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(roleItemModel.OperationResult);
        }

        // POST: Role/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(RoleItemModel roleItemModel)
        {
            try
            {
                if (IsDelete(roleItemModel.OperationResult))
                {
                    if (Application.Delete(roleItemModel.OperationResult, (RoleDTO)roleItemModel.Role.ToDTO()))
                    {
                        return JsonResultSuccess(roleItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }

            roleItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(roleItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Role/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<RoleViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Role), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<RoleViewModel, RoleDTO, Role>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: Role/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, RoleResources.EntitySingular + ".xlsx");
            }
        }

        // POST: Role/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, RoleResources.EntitySingular + ".pdf");
            }
        }

        // POST: Role/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, RoleResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}