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
    public partial class CustomerController : BaseMvcControllerSCRUDApplication<CustomerDTO, Customer>
    {
        #region Methods

        public CustomerController(INorthwindGenericApplicationDTO<CustomerDTO, Customer> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: Customer
        // GET: Customer/Index
        [HttpGet]
        public ActionResult Index()
        {
            CustomerCollectionModel customerCollectionModel = new CustomerCollectionModel(ActivityOperations, "Index", null);

            try
            {
                IsOperation(customerCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                customerCollectionModel.OperationResult.ParseException(exception);
            }

            return View(customerCollectionModel);
        }        

        // GET & POST: Customer/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            CustomerCollectionModel customerCollectionModel = new CustomerCollectionModel(ActivityOperations, "Search", masterControllerAction);

            try
            {
                if (IsOperation(customerCollectionModel.OperationResult))
                {
                    return PartialView(customerCollectionModel);
                }
            }
            catch (Exception exception)
            {
                customerCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerCollectionModel.OperationResult);
        }

        // GET & POST: Customer/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_CustomerLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: Customer/Create
        [HttpGet]
        public ActionResult Create()
        {
            CustomerItemModel customerItemModel = new CustomerItemModel(ActivityOperations, "Create");

            try
            {
                if (IsCreate(customerItemModel.OperationResult))
                {
                    return PartialView("CRUD", customerItemModel);
                }            
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerItemModel.OperationResult);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerItemModel customerItemModel)
        {
            try
            {
                if (IsCreate(customerItemModel.OperationResult))
                {
                    if (IsValid(customerItemModel.OperationResult, customerItemModel.Customer))
                    {
                        CustomerDTO customerDTO = (CustomerDTO)customerItemModel.Customer.ToDTO();
                        if (Application.Create(customerItemModel.OperationResult, customerDTO))
                        {
                            if (customerItemModel.IsSave)
                            {
                                customerItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(customerItemModel.OperationResult,
                                    Url.Action("Update", "Customer", new { CustomerId = customerDTO.CustomerId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(customerItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            customerItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(customerItemModel.OperationResult);
        }

        // GET: Customer/Read/1
        [HttpGet]
        public ActionResult Read(string customerId)
        {
            CustomerItemModel customerItemModel = new CustomerItemModel(ActivityOperations, "Read");
            
            try
            {
                if (IsRead(customerItemModel.OperationResult))
                {
                    CustomerDTO customerDTO = Application.GetById(customerItemModel.OperationResult, new object[] { customerId });
                    if (customerDTO != null)
                    {
                        customerItemModel.Customer = new CustomerViewModel(customerDTO);                    

                        return PartialView("CRUD", customerItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(customerItemModel.OperationResult);
        }

        // GET: Customer/Update/1
        [HttpGet]
        public ActionResult Update(string customerId)
        {
            CustomerItemModel customerItemModel = new CustomerItemModel(ActivityOperations, "Update");
            
            try
            {
                if (IsUpdate(customerItemModel.OperationResult))
                {            
                    CustomerDTO customerDTO = Application.GetById(customerItemModel.OperationResult, new object[] { customerId });
                    if (customerDTO != null)
                    {
                        customerItemModel.Customer = new CustomerViewModel(customerDTO);

                        return PartialView("CRUD", customerItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerItemModel.OperationResult);
        }

        // POST: Customer/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CustomerItemModel customerItemModel)
        {
            try
            {
                if (IsUpdate(customerItemModel.OperationResult))
                {
                    if (IsValid(customerItemModel.OperationResult, customerItemModel.Customer))
                    {
                        CustomerDTO customerDTO = (CustomerDTO)customerItemModel.Customer.ToDTO();
                        if (Application.Update(customerItemModel.OperationResult, customerDTO))
                        {
                            if (customerItemModel.IsSave)
                            {
                                return JsonResultSuccess(customerItemModel.OperationResult,
                                    Url.Action("Update", "Customer", new { CustomerId = customerDTO.CustomerId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(customerItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            customerItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(customerItemModel.OperationResult);
        }

        // GET: Customer/Delete/1
        [HttpGet]
        public ActionResult Delete(string customerId)
        {
            CustomerItemModel customerItemModel = new CustomerItemModel(ActivityOperations, "Delete");
            
            try
            {
                if (IsDelete(customerItemModel.OperationResult))
                {            
                    CustomerDTO customerDTO = Application.GetById(customerItemModel.OperationResult, new object[] { customerId });
                    if (customerDTO != null)
                    {
                        customerItemModel.Customer = new CustomerViewModel(customerDTO);

                        return PartialView("CRUD", customerItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerItemModel.OperationResult);
        }

        // POST: Customer/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CustomerItemModel customerItemModel)
        {
            try
            {
                if (IsDelete(customerItemModel.OperationResult))
                {
                    if (Application.Delete(customerItemModel.OperationResult, (CustomerDTO)customerItemModel.Customer.ToDTO()))
                    {
                        return JsonResultSuccess(customerItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            customerItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(customerItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Customer/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            SyncfusionDataResult dataResult = new SyncfusionDataResult();
            dataResult.result = new List<CustomerViewModel>();

            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Customer), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    dataResult.result = ZViewHelper<CustomerViewModel, CustomerDTO, Customer>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: Customer/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, CustomerResources.EntitySingular + ".xlsx");
            }
        }

        // POST: Customer/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, CustomerResources.EntitySingular + ".pdf");
            }
        }

        // POST: Customer/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, CustomerResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}