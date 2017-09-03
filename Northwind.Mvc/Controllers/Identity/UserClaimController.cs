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
    public partial class UserClaimController : BaseMvcControllerSCRUDApplication<UserClaimDTO, UserClaim>
    {
        #region Methods

        public UserClaimController(IIdentityGenericApplicationDTO<UserClaimDTO, UserClaim> application)
        {
            Application = application;            
        }

        #endregion
        
        #region Methods SCRUD

        // GET: UserClaim
        // GET: UserClaim/Index
        [HttpGet]
        public ActionResult Index(string masterUserId = null)
        {
            UserClaimCollectionModel userClaimCollectionModel = new UserClaimCollectionModel(ActivityOperations, "Index", null, masterUserId);

            try
            {
                IsOperation(userClaimCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                userClaimCollectionModel.OperationResult.ParseException(exception);
            }

            return View(userClaimCollectionModel);
        }        

        // GET & POST: UserClaim/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, string masterUserId = null)
        {
            UserClaimCollectionModel userClaimCollectionModel = new UserClaimCollectionModel(ActivityOperations, "Search", masterControllerAction, masterUserId);

            try
            {
                if (IsOperation(userClaimCollectionModel.OperationResult))
                {
                    return PartialView(userClaimCollectionModel);
                }
            }
            catch (Exception exception)
            {
                userClaimCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userClaimCollectionModel.OperationResult);
        }

        // GET & POST: UserClaim/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_UserClaimLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: UserClaim/Create
        [HttpGet]
        public ActionResult Create(string masterUserId = null)
        {
            UserClaimItemModel userClaimItemModel = new UserClaimItemModel(ActivityOperations, "Create", masterUserId);

            try
            {
                if (IsCreate(userClaimItemModel.OperationResult))
                {
                    return PartialView("CRUD", userClaimItemModel);
                }            
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userClaimItemModel.OperationResult);
        }

        // POST: UserClaim/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserClaimItemModel userClaimItemModel)
        {
            try
            {
                if (IsCreate(userClaimItemModel.OperationResult))
                {
                    if (IsValid(userClaimItemModel.OperationResult, userClaimItemModel.UserClaim))
                    {
                        UserClaimDTO userClaimDTO = (UserClaimDTO)userClaimItemModel.UserClaim.ToDTO();
                        if (Application.Create(userClaimItemModel.OperationResult, userClaimDTO))
                        {
                            if (userClaimItemModel.IsSave)
                            {
                                userClaimItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(userClaimItemModel.OperationResult,
                                    Url.Action("Update", "UserClaim", new { Id = userClaimDTO.Id }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(userClaimItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }

            userClaimItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(userClaimItemModel.OperationResult);
        }

        // GET: UserClaim/Read/1
        [HttpGet]
        public ActionResult Read(int id, string masterUserId = null)
        {
            UserClaimItemModel userClaimItemModel = new UserClaimItemModel(ActivityOperations, "Read", masterUserId);
            
            try
            {
                if (IsRead(userClaimItemModel.OperationResult))
                {
                    UserClaimDTO userClaimDTO = Application.GetById(userClaimItemModel.OperationResult, new object[] { id });
                    if (userClaimDTO != null)
                    {
                        userClaimItemModel.UserClaim = new UserClaimViewModel(userClaimDTO);                    

                        return PartialView("CRUD", userClaimItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(userClaimItemModel.OperationResult);
        }

        // GET: UserClaim/Update/1
        [HttpGet]
        public ActionResult Update(int id, string masterUserId = null)
        {
            UserClaimItemModel userClaimItemModel = new UserClaimItemModel(ActivityOperations, "Update", masterUserId);
            
            try
            {
                if (IsUpdate(userClaimItemModel.OperationResult))
                {            
                    UserClaimDTO userClaimDTO = Application.GetById(userClaimItemModel.OperationResult, new object[] { id });
                    if (userClaimDTO != null)
                    {
                        userClaimItemModel.UserClaim = new UserClaimViewModel(userClaimDTO);

                        return PartialView("CRUD", userClaimItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userClaimItemModel.OperationResult);
        }

        // POST: UserClaim/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserClaimItemModel userClaimItemModel)
        {
            try
            {
                if (IsUpdate(userClaimItemModel.OperationResult))
                {
                    if (IsValid(userClaimItemModel.OperationResult, userClaimItemModel.UserClaim))
                    {
                        UserClaimDTO userClaimDTO = (UserClaimDTO)userClaimItemModel.UserClaim.ToDTO();
                        if (Application.Update(userClaimItemModel.OperationResult, userClaimDTO))
                        {
                            if (userClaimItemModel.IsSave)
                            {
                                return JsonResultSuccess(userClaimItemModel.OperationResult,
                                    Url.Action("Update", "UserClaim", new { Id = userClaimDTO.Id }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(userClaimItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }

            userClaimItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(userClaimItemModel.OperationResult);
        }

        // GET: UserClaim/Delete/1
        [HttpGet]
        public ActionResult Delete(int id, string masterUserId = null)
        {
            UserClaimItemModel userClaimItemModel = new UserClaimItemModel(ActivityOperations, "Delete", masterUserId);
            
            try
            {
                if (IsDelete(userClaimItemModel.OperationResult))
                {            
                    UserClaimDTO userClaimDTO = Application.GetById(userClaimItemModel.OperationResult, new object[] { id });
                    if (userClaimDTO != null)
                    {
                        userClaimItemModel.UserClaim = new UserClaimViewModel(userClaimDTO);

                        return PartialView("CRUD", userClaimItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userClaimItemModel.OperationResult);
        }

        // POST: UserClaim/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserClaimItemModel userClaimItemModel)
        {
            try
            {
                if (IsDelete(userClaimItemModel.OperationResult))
                {
                    if (Application.Delete(userClaimItemModel.OperationResult, (UserClaimDTO)userClaimItemModel.UserClaim.ToDTO()))
                    {
                        return JsonResultSuccess(userClaimItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }

            userClaimItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(userClaimItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: UserClaim/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<UserClaimViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(UserClaim), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<UserClaimViewModel, UserClaimDTO, UserClaim>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: UserClaim/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, UserClaimResources.EntitySingular + ".xlsx");
            }
        }

        // POST: UserClaim/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, UserClaimResources.EntitySingular + ".pdf");
            }
        }

        // POST: UserClaim/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, UserClaimResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}