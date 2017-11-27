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
    public partial class UserRoleController : BaseMvcControllerSCRUDApplication<UserRoleDTO, UserRole>
    {
        #region Methods

        public UserRoleController(IIdentityGenericApplicationDTO<UserRoleDTO, UserRole> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: UserRole
        // GET: UserRole/Index
        [HttpGet]
        public ActionResult Index(string masterRoleId = null, string masterUserId = null)
        {
            UserRoleCollectionModel userRoleCollectionModel = new UserRoleCollectionModel(ActivityOperations, "Index", null, masterRoleId, masterUserId);

            try
            {
                if (IsIndex(userRoleCollectionModel.OperationResult))
                {
                    return View(userRoleCollectionModel);
                }
            }
            catch (Exception exception)
            {
                userRoleCollectionModel.OperationResult.ParseException(exception);
            }

            return View("OperationResult", new OperationResultViewModel(userRoleCollectionModel.OperationResult));
        }        

        // GET & POST: UserRole/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, string masterRoleId = null, string masterUserId = null)
        {
            UserRoleCollectionModel userRoleCollectionModel = new UserRoleCollectionModel(ActivityOperations, "Search", masterControllerAction, masterRoleId, masterUserId);

            try
            {
                if (IsOperation(userRoleCollectionModel.OperationResult))
                {
                    return PartialView(userRoleCollectionModel);
                }
            }
            catch (Exception exception)
            {
                userRoleCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userRoleCollectionModel.OperationResult);
        }

        // GET & POST: UserRole/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_UserRoleLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: UserRole/Create
        [HttpGet]
        public ActionResult Create(string masterRoleId = null, string masterUserId = null)
        {
            UserRoleItemModel userRoleItemModel = new UserRoleItemModel(ActivityOperations, "Create", masterRoleId, masterUserId);

            try
            {
                if (IsCreate(userRoleItemModel.OperationResult))
                {
                    return PartialView("CRUD", userRoleItemModel);
                }            
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userRoleItemModel.OperationResult);
        }

        // POST: UserRole/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRoleItemModel userRoleItemModel)
        {
            try
            {
                if (IsCreate(userRoleItemModel.OperationResult))
                {
                    if (IsValid(userRoleItemModel.OperationResult, userRoleItemModel.UserRole))
                    {
                        UserRoleDTO userRoleDTO = (UserRoleDTO)userRoleItemModel.UserRole.ToDTO();
                        if (Application.Create(userRoleItemModel.OperationResult, userRoleDTO))
                        {
                            if (userRoleItemModel.IsSave)
                            {
                                userRoleItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(userRoleItemModel.OperationResult,
                                    Url.Action("Update", "UserRole", new { UserId = userRoleDTO.UserId, RoleId = userRoleDTO.RoleId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(userRoleItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }

            userRoleItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(userRoleItemModel.OperationResult);
        }

        // GET: UserRole/Read/1
        [HttpGet]
        public ActionResult Read(string userId, string roleId, string masterRoleId = null, string masterUserId = null)
        {
            UserRoleItemModel userRoleItemModel = new UserRoleItemModel(ActivityOperations, "Read", masterRoleId, masterUserId);
            
            try
            {
                if (IsRead(userRoleItemModel.OperationResult))
                {
                    UserRoleDTO userRoleDTO = Application.GetById(userRoleItemModel.OperationResult, new object[] { userId, roleId });
                    if (userRoleDTO != null)
                    {
                        userRoleItemModel.UserRole = new UserRoleViewModel(userRoleDTO);                    

                        return PartialView("CRUD", userRoleItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(userRoleItemModel.OperationResult);
        }

        // GET: UserRole/Update/1
        [HttpGet]
        public ActionResult Update(string userId, string roleId, string masterRoleId = null, string masterUserId = null)
        {
            UserRoleItemModel userRoleItemModel = new UserRoleItemModel(ActivityOperations, "Update", masterRoleId, masterUserId);
            
            try
            {
                if (IsUpdate(userRoleItemModel.OperationResult))
                {            
                    UserRoleDTO userRoleDTO = Application.GetById(userRoleItemModel.OperationResult, new object[] { userId, roleId });
                    if (userRoleDTO != null)
                    {
                        userRoleItemModel.UserRole = new UserRoleViewModel(userRoleDTO);

                        return PartialView("CRUD", userRoleItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userRoleItemModel.OperationResult);
        }

        // POST: UserRole/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserRoleItemModel userRoleItemModel)
        {
            try
            {
                if (IsUpdate(userRoleItemModel.OperationResult))
                {
                    if (IsValid(userRoleItemModel.OperationResult, userRoleItemModel.UserRole))
                    {
                        UserRoleDTO userRoleDTO = (UserRoleDTO)userRoleItemModel.UserRole.ToDTO();
                        if (Application.Update(userRoleItemModel.OperationResult, userRoleDTO))
                        {
                            if (userRoleItemModel.IsSave)
                            {
                                return JsonResultSuccess(userRoleItemModel.OperationResult,
                                    Url.Action("Update", "UserRole", new { UserId = userRoleDTO.UserId, RoleId = userRoleDTO.RoleId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(userRoleItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }

            userRoleItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(userRoleItemModel.OperationResult);
        }

        // GET: UserRole/Delete/1
        [HttpGet]
        public ActionResult Delete(string userId, string roleId, string masterRoleId = null, string masterUserId = null)
        {
            UserRoleItemModel userRoleItemModel = new UserRoleItemModel(ActivityOperations, "Delete", masterRoleId, masterUserId);
            
            try
            {
                if (IsDelete(userRoleItemModel.OperationResult))
                {            
                    UserRoleDTO userRoleDTO = Application.GetById(userRoleItemModel.OperationResult, new object[] { userId, roleId });
                    if (userRoleDTO != null)
                    {
                        userRoleItemModel.UserRole = new UserRoleViewModel(userRoleDTO);

                        return PartialView("CRUD", userRoleItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userRoleItemModel.OperationResult);
        }

        // POST: UserRole/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserRoleItemModel userRoleItemModel)
        {
            try
            {
                if (IsDelete(userRoleItemModel.OperationResult))
                {
                    if (Application.Delete(userRoleItemModel.OperationResult, (UserRoleDTO)userRoleItemModel.UserRole.ToDTO()))
                    {
                        return JsonResultSuccess(userRoleItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }

            userRoleItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(userRoleItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: UserRole/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            SyncfusionDataResult dataResult = new SyncfusionDataResult();
            dataResult.result = new List<UserRoleViewModel>();

            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(UserRole), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    dataResult.result = ZViewHelper<UserRoleViewModel, UserRoleDTO, UserRole>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: UserRole/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, UserRoleResources.EntitySingular + ".xlsx");
            }
        }

        // POST: UserRole/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, UserRoleResources.EntitySingular + ".pdf");
            }
        }

        // POST: UserRole/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, UserRoleResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}