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
    public partial class UserController : BaseMvcControllerSCRUDApplication<UserDTO, User>
    {
        #region Methods

        public UserController(IIdentityGenericApplicationDTO<UserDTO, User> application)
        {
            Application = application;            
        }

        #endregion
        
        #region Methods SCRUD

        // GET: User
        // GET: User/Index
        [HttpGet]
        public ActionResult Index()
        {
            UserCollectionModel userCollectionModel = new UserCollectionModel(ActivityOperations, "Index", null);

            try
            {
                IsOperation(userCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                userCollectionModel.OperationResult.ParseException(exception);
            }

            return View(userCollectionModel);
        }        

        // GET & POST: User/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            UserCollectionModel userCollectionModel = new UserCollectionModel(ActivityOperations, "Search", masterControllerAction);

            try
            {
                if (IsOperation(userCollectionModel.OperationResult))
                {
                    return PartialView(userCollectionModel);
                }
            }
            catch (Exception exception)
            {
                userCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userCollectionModel.OperationResult);
        }

        // GET & POST: User/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_UserLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: User/Create
        [HttpGet]
        public ActionResult Create()
        {
            UserItemModel userItemModel = new UserItemModel(ActivityOperations, "Create");

            try
            {
                if (IsCreate(userItemModel.OperationResult))
                {
                    return PartialView("CRUD", userItemModel);
                }            
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userItemModel.OperationResult);
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserItemModel userItemModel)
        {
            try
            {
                if (IsCreate(userItemModel.OperationResult))
                {
                    if (IsValid(userItemModel.OperationResult, userItemModel.User))
                    {
                        UserDTO userDTO = (UserDTO)userItemModel.User.ToDTO();
                        if (Application.Create(userItemModel.OperationResult, userDTO))
                        {
                            if (userItemModel.IsSave)
                            {
                                userItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(userItemModel.OperationResult,
                                    Url.Action("Update", "User", new { Id = userDTO.Id }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(userItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }

            userItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(userItemModel.OperationResult);
        }

        // GET: User/Read/1
        [HttpGet]
        public ActionResult Read(string id)
        {
            UserItemModel userItemModel = new UserItemModel(ActivityOperations, "Read");
            
            try
            {
                if (IsRead(userItemModel.OperationResult))
                {
                    UserDTO userDTO = Application.GetById(userItemModel.OperationResult, new object[] { id });
                    if (userDTO != null)
                    {
                        userItemModel.User = new UserViewModel(userDTO);                    

                        return PartialView("CRUD", userItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(userItemModel.OperationResult);
        }

        // GET: User/Update/1
        [HttpGet]
        public ActionResult Update(string id)
        {
            UserItemModel userItemModel = new UserItemModel(ActivityOperations, "Update");
            
            try
            {
                if (IsUpdate(userItemModel.OperationResult))
                {            
                    UserDTO userDTO = Application.GetById(userItemModel.OperationResult, new object[] { id });
                    if (userDTO != null)
                    {
                        userItemModel.User = new UserViewModel(userDTO);

                        return PartialView("CRUD", userItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userItemModel.OperationResult);
        }

        // POST: User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserItemModel userItemModel)
        {
            try
            {
                if (IsUpdate(userItemModel.OperationResult))
                {
                    if (IsValid(userItemModel.OperationResult, userItemModel.User))
                    {
                        UserDTO userDTO = (UserDTO)userItemModel.User.ToDTO();
                        if (Application.Update(userItemModel.OperationResult, userDTO))
                        {
                            if (userItemModel.IsSave)
                            {
                                return JsonResultSuccess(userItemModel.OperationResult,
                                    Url.Action("Update", "User", new { Id = userDTO.Id }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(userItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }

            userItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(userItemModel.OperationResult);
        }

        // GET: User/Delete/1
        [HttpGet]
        public ActionResult Delete(string id)
        {
            UserItemModel userItemModel = new UserItemModel(ActivityOperations, "Delete");
            
            try
            {
                if (IsDelete(userItemModel.OperationResult))
                {            
                    UserDTO userDTO = Application.GetById(userItemModel.OperationResult, new object[] { id });
                    if (userDTO != null)
                    {
                        userItemModel.User = new UserViewModel(userDTO);

                        return PartialView("CRUD", userItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userItemModel.OperationResult);
        }

        // POST: User/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserItemModel userItemModel)
        {
            try
            {
                if (IsDelete(userItemModel.OperationResult))
                {
                    if (Application.Delete(userItemModel.OperationResult, (UserDTO)userItemModel.User.ToDTO()))
                    {
                        return JsonResultSuccess(userItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }

            userItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(userItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: User/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<UserViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(User), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<UserViewModel, UserDTO, User>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: User/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, UserResources.EntitySingular + ".xlsx");
            }
        }

        // POST: User/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, UserResources.EntitySingular + ".pdf");
            }
        }

        // POST: User/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, UserResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}