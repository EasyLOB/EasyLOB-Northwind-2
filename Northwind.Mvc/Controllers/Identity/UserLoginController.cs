using EasyLOB.Identity;
using EasyLOB.Identity.Data;
using EasyLOB.Identity.Data.Resources;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Mvc;
using Newtonsoft.Json;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyLOB.Identity.Mvc
{
    public partial class UserLoginController : BaseMvcControllerSCRUDApplication<UserLoginDTO, UserLogin>
    {
        #region Methods

        public UserLoginController(IIdentityGenericApplicationDTO<UserLoginDTO, UserLogin> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: UserLogin
        // GET: UserLogin/Index
        [HttpGet]
        public ActionResult Index(string masterUserId = null)
        {
            UserLoginCollectionModel userLoginCollectionModel = new UserLoginCollectionModel(ActivityOperations, "Index", null, masterUserId);

            try
            {
                if (IsIndex(userLoginCollectionModel.OperationResult))
                {
                    return View(userLoginCollectionModel);
                }
            }
            catch (Exception exception)
            {
                userLoginCollectionModel.OperationResult.ParseException(exception);
            }

            return View("OperationResult", new OperationResultModel(userLoginCollectionModel.OperationResult));
        }        

        // GET & POST: UserLogin/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, string masterUserId = null)
        {
            UserLoginCollectionModel userLoginCollectionModel = new UserLoginCollectionModel(ActivityOperations, "Search", masterControllerAction, masterUserId);

            try
            {
                if (IsOperation(userLoginCollectionModel.OperationResult))
                {
                    return PartialView(userLoginCollectionModel);
                }
            }
            catch (Exception exception)
            {
                userLoginCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userLoginCollectionModel.OperationResult);
        }

        // GET & POST: UserLogin/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_UserLoginLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: UserLogin/Create
        [HttpGet]
        public ActionResult Create(string masterUserId = null)
        {
            UserLoginItemModel userLoginItemModel = new UserLoginItemModel(ActivityOperations, "Create", masterUserId);

            try
            {
                if (IsCreate(userLoginItemModel.OperationResult))
                {
                    return PartialView("CRUD", userLoginItemModel);
                }            
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userLoginItemModel.OperationResult);
        }

        // POST: UserLogin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserLoginItemModel userLoginItemModel)
        {
            try
            {
                if (IsCreate(userLoginItemModel.OperationResult))
                {
                    if (IsValid(userLoginItemModel.OperationResult, userLoginItemModel.UserLogin))
                    {
                        UserLoginDTO userLoginDTO = (UserLoginDTO)userLoginItemModel.UserLogin.ToDTO();
                        if (Application.Create(userLoginItemModel.OperationResult, userLoginDTO))
                        {
                            if (userLoginItemModel.IsSave)
                            {
                                userLoginItemModel.OperationResult.InformationMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(userLoginItemModel.OperationResult,
                                    Url.Action("Update", "UserLogin", new { LoginProvider = userLoginDTO.LoginProvider, ProviderKey = userLoginDTO.ProviderKey, UserId = userLoginDTO.UserId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(userLoginItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }

            userLoginItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(userLoginItemModel.OperationResult);
        }

        // GET: UserLogin/Read/1
        [HttpGet]
        public ActionResult Read(string loginProvider, string providerKey, string userId, string masterUserId = null)
        {
            UserLoginItemModel userLoginItemModel = new UserLoginItemModel(ActivityOperations, "Read", masterUserId);
            
            try
            {
                if (IsRead(userLoginItemModel.OperationResult))
                {
                    UserLoginDTO userLoginDTO = Application.GetById(userLoginItemModel.OperationResult, new object[] { loginProvider, providerKey, userId });
                    if (userLoginDTO != null)
                    {
                        userLoginItemModel.UserLogin = new UserLoginViewModel(userLoginDTO);                    

                        return PartialView("CRUD", userLoginItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(userLoginItemModel.OperationResult);
        }

        // GET: UserLogin/Update/1
        [HttpGet]
        public ActionResult Update(string loginProvider, string providerKey, string userId, string masterUserId = null)
        {
            UserLoginItemModel userLoginItemModel = new UserLoginItemModel(ActivityOperations, "Update", masterUserId);
            
            try
            {
                if (IsUpdate(userLoginItemModel.OperationResult))
                {            
                    UserLoginDTO userLoginDTO = Application.GetById(userLoginItemModel.OperationResult, new object[] { loginProvider, providerKey, userId });
                    if (userLoginDTO != null)
                    {
                        userLoginItemModel.UserLogin = new UserLoginViewModel(userLoginDTO);

                        return PartialView("CRUD", userLoginItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userLoginItemModel.OperationResult);
        }

        // POST: UserLogin/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserLoginItemModel userLoginItemModel)
        {
            try
            {
                if (IsUpdate(userLoginItemModel.OperationResult))
                {
                    if (IsValid(userLoginItemModel.OperationResult, userLoginItemModel.UserLogin))
                    {
                        UserLoginDTO userLoginDTO = (UserLoginDTO)userLoginItemModel.UserLogin.ToDTO();
                        if (Application.Update(userLoginItemModel.OperationResult, userLoginDTO))
                        {
                            if (userLoginItemModel.IsSave)
                            {
                                return JsonResultSuccess(userLoginItemModel.OperationResult,
                                    Url.Action("Update", "UserLogin", new { LoginProvider = userLoginDTO.LoginProvider, ProviderKey = userLoginDTO.ProviderKey, UserId = userLoginDTO.UserId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(userLoginItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }

            userLoginItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(userLoginItemModel.OperationResult);
        }

        // GET: UserLogin/Delete/1
        [HttpGet]
        public ActionResult Delete(string loginProvider, string providerKey, string userId, string masterUserId = null)
        {
            UserLoginItemModel userLoginItemModel = new UserLoginItemModel(ActivityOperations, "Delete", masterUserId);
            
            try
            {
                if (IsDelete(userLoginItemModel.OperationResult))
                {            
                    UserLoginDTO userLoginDTO = Application.GetById(userLoginItemModel.OperationResult, new object[] { loginProvider, providerKey, userId });
                    if (userLoginDTO != null)
                    {
                        userLoginItemModel.UserLogin = new UserLoginViewModel(userLoginDTO);

                        return PartialView("CRUD", userLoginItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userLoginItemModel.OperationResult);
        }

        // POST: UserLogin/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserLoginItemModel userLoginItemModel)
        {
            try
            {
                if (IsDelete(userLoginItemModel.OperationResult))
                {
                    if (Application.Delete(userLoginItemModel.OperationResult, (UserLoginDTO)userLoginItemModel.UserLogin.ToDTO()))
                    {
                        return JsonResultSuccess(userLoginItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }

            userLoginItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(userLoginItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: UserLogin/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            SyncfusionDataResult dataResult = new SyncfusionDataResult();
            dataResult.result = new List<UserLoginViewModel>();

            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(UserLogin), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    dataResult.result = ZViewHelper<UserLoginViewModel, UserLoginDTO, UserLogin>.ToViewList(Application.Search(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: UserLogin/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, UserLoginResources.EntitySingular + ".xlsx");
            }
        }

        // POST: UserLogin/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, UserLoginResources.EntitySingular + ".pdf");
            }
        }

        // POST: UserLogin/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, UserLoginResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}