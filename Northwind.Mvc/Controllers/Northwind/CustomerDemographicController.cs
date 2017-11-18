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
    public partial class CustomerDemographicController : BaseMvcControllerSCRUDApplication<CustomerDemographicDTO, CustomerDemographic>
    {
        #region Methods

        public CustomerDemographicController(INorthwindGenericApplicationDTO<CustomerDemographicDTO, CustomerDemographic> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: CustomerDemographic
        // GET: CustomerDemographic/Index
        [HttpGet]
        public ActionResult Index()
        {
            CustomerDemographicCollectionModel customerDemographicCollectionModel = new CustomerDemographicCollectionModel(ActivityOperations, "Index", null);

            try
            {
                IsOperation(customerDemographicCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                customerDemographicCollectionModel.OperationResult.ParseException(exception);
            }

            return View(customerDemographicCollectionModel);
        }        

        // GET & POST: CustomerDemographic/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            CustomerDemographicCollectionModel customerDemographicCollectionModel = new CustomerDemographicCollectionModel(ActivityOperations, "Search", masterControllerAction);

            try
            {
                if (IsOperation(customerDemographicCollectionModel.OperationResult))
                {
                    return PartialView(customerDemographicCollectionModel);
                }
            }
            catch (Exception exception)
            {
                customerDemographicCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerDemographicCollectionModel.OperationResult);
        }

        // GET & POST: CustomerDemographic/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_CustomerDemographicLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: CustomerDemographic/Create
        [HttpGet]
        public ActionResult Create()
        {
            CustomerDemographicItemModel customerDemographicItemModel = new CustomerDemographicItemModel(ActivityOperations, "Create");

            try
            {
                if (IsCreate(customerDemographicItemModel.OperationResult))
                {
                    return PartialView("CRUD", customerDemographicItemModel);
                }            
            }
            catch (Exception exception)
            {
                customerDemographicItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerDemographicItemModel.OperationResult);
        }

        // POST: CustomerDemographic/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerDemographicItemModel customerDemographicItemModel)
        {
            try
            {
                if (IsCreate(customerDemographicItemModel.OperationResult))
                {
                    if (IsValid(customerDemographicItemModel.OperationResult, customerDemographicItemModel.CustomerDemographic))
                    {
                        CustomerDemographicDTO customerDemographicDTO = (CustomerDemographicDTO)customerDemographicItemModel.CustomerDemographic.ToDTO();
                        if (Application.Create(customerDemographicItemModel.OperationResult, customerDemographicDTO))
                        {
                            if (customerDemographicItemModel.IsSave)
                            {
                                customerDemographicItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(customerDemographicItemModel.OperationResult,
                                    Url.Action("Update", "CustomerDemographic", new { CustomerTypeId = customerDemographicDTO.CustomerTypeId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(customerDemographicItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                customerDemographicItemModel.OperationResult.ParseException(exception);
            }

            customerDemographicItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(customerDemographicItemModel.OperationResult);
        }

        // GET: CustomerDemographic/Read/1
        [HttpGet]
        public ActionResult Read(string customerTypeId)
        {
            CustomerDemographicItemModel customerDemographicItemModel = new CustomerDemographicItemModel(ActivityOperations, "Read");
            
            try
            {
                if (IsRead(customerDemographicItemModel.OperationResult))
                {
                    CustomerDemographicDTO customerDemographicDTO = Application.GetById(customerDemographicItemModel.OperationResult, new object[] { customerTypeId });
                    if (customerDemographicDTO != null)
                    {
                        customerDemographicItemModel.CustomerDemographic = new CustomerDemographicViewModel(customerDemographicDTO);                    

                        return PartialView("CRUD", customerDemographicItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                customerDemographicItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(customerDemographicItemModel.OperationResult);
        }

        // GET: CustomerDemographic/Update/1
        [HttpGet]
        public ActionResult Update(string customerTypeId)
        {
            CustomerDemographicItemModel customerDemographicItemModel = new CustomerDemographicItemModel(ActivityOperations, "Update");
            
            try
            {
                if (IsUpdate(customerDemographicItemModel.OperationResult))
                {            
                    CustomerDemographicDTO customerDemographicDTO = Application.GetById(customerDemographicItemModel.OperationResult, new object[] { customerTypeId });
                    if (customerDemographicDTO != null)
                    {
                        customerDemographicItemModel.CustomerDemographic = new CustomerDemographicViewModel(customerDemographicDTO);

                        return PartialView("CRUD", customerDemographicItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                customerDemographicItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerDemographicItemModel.OperationResult);
        }

        // POST: CustomerDemographic/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CustomerDemographicItemModel customerDemographicItemModel)
        {
            try
            {
                if (IsUpdate(customerDemographicItemModel.OperationResult))
                {
                    if (IsValid(customerDemographicItemModel.OperationResult, customerDemographicItemModel.CustomerDemographic))
                    {
                        CustomerDemographicDTO customerDemographicDTO = (CustomerDemographicDTO)customerDemographicItemModel.CustomerDemographic.ToDTO();
                        if (Application.Update(customerDemographicItemModel.OperationResult, customerDemographicDTO))
                        {
                            if (customerDemographicItemModel.IsSave)
                            {
                                return JsonResultSuccess(customerDemographicItemModel.OperationResult,
                                    Url.Action("Update", "CustomerDemographic", new { CustomerTypeId = customerDemographicDTO.CustomerTypeId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(customerDemographicItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                customerDemographicItemModel.OperationResult.ParseException(exception);
            }

            customerDemographicItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(customerDemographicItemModel.OperationResult);
        }

        // GET: CustomerDemographic/Delete/1
        [HttpGet]
        public ActionResult Delete(string customerTypeId)
        {
            CustomerDemographicItemModel customerDemographicItemModel = new CustomerDemographicItemModel(ActivityOperations, "Delete");
            
            try
            {
                if (IsDelete(customerDemographicItemModel.OperationResult))
                {            
                    CustomerDemographicDTO customerDemographicDTO = Application.GetById(customerDemographicItemModel.OperationResult, new object[] { customerTypeId });
                    if (customerDemographicDTO != null)
                    {
                        customerDemographicItemModel.CustomerDemographic = new CustomerDemographicViewModel(customerDemographicDTO);

                        return PartialView("CRUD", customerDemographicItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                customerDemographicItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerDemographicItemModel.OperationResult);
        }

        // POST: CustomerDemographic/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CustomerDemographicItemModel customerDemographicItemModel)
        {
            try
            {
                if (IsDelete(customerDemographicItemModel.OperationResult))
                {
                    if (Application.Delete(customerDemographicItemModel.OperationResult, (CustomerDemographicDTO)customerDemographicItemModel.CustomerDemographic.ToDTO()))
                    {
                        return JsonResultSuccess(customerDemographicItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                customerDemographicItemModel.OperationResult.ParseException(exception);
            }

            customerDemographicItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(customerDemographicItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: CustomerDemographic/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            SyncfusionDataResult dataResult = new SyncfusionDataResult();
            dataResult.result = new List<CustomerDemographicViewModel>();

            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(CustomerDemographic), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    dataResult.result = ZViewHelper<CustomerDemographicViewModel, CustomerDemographicDTO, CustomerDemographic>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
                    if (dataManager.RequiresCounts)
                    {
                        dataResult.count = Application.Count(where, args.ToArray());
                    }
                }
                catch (Exception exception)
                {
                    operationResult.ParseException(exception);
                }
            }

            if (!operationResult.Ok)
            {
                throw new InvalidOperationException(operationResult.Text);
            }

            return Json(JsonConvert.SerializeObject(dataResult), JsonRequestBehavior.AllowGet);
        } 

        // POST: CustomerDemographic/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, CustomerDemographicResources.EntitySingular + ".xlsx");
            }
        }

        // POST: CustomerDemographic/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, CustomerDemographicResources.EntitySingular + ".pdf");
            }
        }

        // POST: CustomerDemographic/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, CustomerDemographicResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}