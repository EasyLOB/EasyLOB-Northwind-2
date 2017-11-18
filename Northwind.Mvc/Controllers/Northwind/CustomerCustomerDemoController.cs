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
    public partial class CustomerCustomerDemoController : BaseMvcControllerSCRUDApplication<CustomerCustomerDemoDTO, CustomerCustomerDemo>
    {
        #region Methods

        public CustomerCustomerDemoController(INorthwindGenericApplicationDTO<CustomerCustomerDemoDTO, CustomerCustomerDemo> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: CustomerCustomerDemo
        // GET: CustomerCustomerDemo/Index
        [HttpGet]
        public ActionResult Index(string masterCustomerTypeId = null, string masterCustomerId = null)
        {
            CustomerCustomerDemoCollectionModel customerCustomerDemoCollectionModel = new CustomerCustomerDemoCollectionModel(ActivityOperations, "Index", null, masterCustomerTypeId, masterCustomerId);

            try
            {
                IsOperation(customerCustomerDemoCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                customerCustomerDemoCollectionModel.OperationResult.ParseException(exception);
            }

            return View(customerCustomerDemoCollectionModel);
        }        

        // GET & POST: CustomerCustomerDemo/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, string masterCustomerTypeId = null, string masterCustomerId = null)
        {
            CustomerCustomerDemoCollectionModel customerCustomerDemoCollectionModel = new CustomerCustomerDemoCollectionModel(ActivityOperations, "Search", masterControllerAction, masterCustomerTypeId, masterCustomerId);

            try
            {
                if (IsOperation(customerCustomerDemoCollectionModel.OperationResult))
                {
                    return PartialView(customerCustomerDemoCollectionModel);
                }
            }
            catch (Exception exception)
            {
                customerCustomerDemoCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerCustomerDemoCollectionModel.OperationResult);
        }

        // GET & POST: CustomerCustomerDemo/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_CustomerCustomerDemoLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: CustomerCustomerDemo/Create
        [HttpGet]
        public ActionResult Create(string masterCustomerTypeId = null, string masterCustomerId = null)
        {
            CustomerCustomerDemoItemModel customerCustomerDemoItemModel = new CustomerCustomerDemoItemModel(ActivityOperations, "Create", masterCustomerTypeId, masterCustomerId);

            try
            {
                if (IsCreate(customerCustomerDemoItemModel.OperationResult))
                {
                    return PartialView("CRUD", customerCustomerDemoItemModel);
                }            
            }
            catch (Exception exception)
            {
                customerCustomerDemoItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerCustomerDemoItemModel.OperationResult);
        }

        // POST: CustomerCustomerDemo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerCustomerDemoItemModel customerCustomerDemoItemModel)
        {
            try
            {
                if (IsCreate(customerCustomerDemoItemModel.OperationResult))
                {
                    if (IsValid(customerCustomerDemoItemModel.OperationResult, customerCustomerDemoItemModel.CustomerCustomerDemo))
                    {
                        CustomerCustomerDemoDTO customerCustomerDemoDTO = (CustomerCustomerDemoDTO)customerCustomerDemoItemModel.CustomerCustomerDemo.ToDTO();
                        if (Application.Create(customerCustomerDemoItemModel.OperationResult, customerCustomerDemoDTO))
                        {
                            if (customerCustomerDemoItemModel.IsSave)
                            {
                                customerCustomerDemoItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(customerCustomerDemoItemModel.OperationResult,
                                    Url.Action("Update", "CustomerCustomerDemo", new { CustomerId = customerCustomerDemoDTO.CustomerId, CustomerTypeId = customerCustomerDemoDTO.CustomerTypeId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(customerCustomerDemoItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                customerCustomerDemoItemModel.OperationResult.ParseException(exception);
            }

            customerCustomerDemoItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(customerCustomerDemoItemModel.OperationResult);
        }

        // GET: CustomerCustomerDemo/Read/1
        [HttpGet]
        public ActionResult Read(string customerId, string customerTypeId, string masterCustomerTypeId = null, string masterCustomerId = null)
        {
            CustomerCustomerDemoItemModel customerCustomerDemoItemModel = new CustomerCustomerDemoItemModel(ActivityOperations, "Read", masterCustomerTypeId, masterCustomerId);
            
            try
            {
                if (IsRead(customerCustomerDemoItemModel.OperationResult))
                {
                    CustomerCustomerDemoDTO customerCustomerDemoDTO = Application.GetById(customerCustomerDemoItemModel.OperationResult, new object[] { customerId, customerTypeId });
                    if (customerCustomerDemoDTO != null)
                    {
                        customerCustomerDemoItemModel.CustomerCustomerDemo = new CustomerCustomerDemoViewModel(customerCustomerDemoDTO);                    

                        return PartialView("CRUD", customerCustomerDemoItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                customerCustomerDemoItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(customerCustomerDemoItemModel.OperationResult);
        }

        // GET: CustomerCustomerDemo/Update/1
        [HttpGet]
        public ActionResult Update(string customerId, string customerTypeId, string masterCustomerTypeId = null, string masterCustomerId = null)
        {
            CustomerCustomerDemoItemModel customerCustomerDemoItemModel = new CustomerCustomerDemoItemModel(ActivityOperations, "Update", masterCustomerTypeId, masterCustomerId);
            
            try
            {
                if (IsUpdate(customerCustomerDemoItemModel.OperationResult))
                {            
                    CustomerCustomerDemoDTO customerCustomerDemoDTO = Application.GetById(customerCustomerDemoItemModel.OperationResult, new object[] { customerId, customerTypeId });
                    if (customerCustomerDemoDTO != null)
                    {
                        customerCustomerDemoItemModel.CustomerCustomerDemo = new CustomerCustomerDemoViewModel(customerCustomerDemoDTO);

                        return PartialView("CRUD", customerCustomerDemoItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                customerCustomerDemoItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerCustomerDemoItemModel.OperationResult);
        }

        // POST: CustomerCustomerDemo/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CustomerCustomerDemoItemModel customerCustomerDemoItemModel)
        {
            try
            {
                if (IsUpdate(customerCustomerDemoItemModel.OperationResult))
                {
                    if (IsValid(customerCustomerDemoItemModel.OperationResult, customerCustomerDemoItemModel.CustomerCustomerDemo))
                    {
                        CustomerCustomerDemoDTO customerCustomerDemoDTO = (CustomerCustomerDemoDTO)customerCustomerDemoItemModel.CustomerCustomerDemo.ToDTO();
                        if (Application.Update(customerCustomerDemoItemModel.OperationResult, customerCustomerDemoDTO))
                        {
                            if (customerCustomerDemoItemModel.IsSave)
                            {
                                return JsonResultSuccess(customerCustomerDemoItemModel.OperationResult,
                                    Url.Action("Update", "CustomerCustomerDemo", new { CustomerId = customerCustomerDemoDTO.CustomerId, CustomerTypeId = customerCustomerDemoDTO.CustomerTypeId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(customerCustomerDemoItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                customerCustomerDemoItemModel.OperationResult.ParseException(exception);
            }

            customerCustomerDemoItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(customerCustomerDemoItemModel.OperationResult);
        }

        // GET: CustomerCustomerDemo/Delete/1
        [HttpGet]
        public ActionResult Delete(string customerId, string customerTypeId, string masterCustomerTypeId = null, string masterCustomerId = null)
        {
            CustomerCustomerDemoItemModel customerCustomerDemoItemModel = new CustomerCustomerDemoItemModel(ActivityOperations, "Delete", masterCustomerTypeId, masterCustomerId);
            
            try
            {
                if (IsDelete(customerCustomerDemoItemModel.OperationResult))
                {            
                    CustomerCustomerDemoDTO customerCustomerDemoDTO = Application.GetById(customerCustomerDemoItemModel.OperationResult, new object[] { customerId, customerTypeId });
                    if (customerCustomerDemoDTO != null)
                    {
                        customerCustomerDemoItemModel.CustomerCustomerDemo = new CustomerCustomerDemoViewModel(customerCustomerDemoDTO);

                        return PartialView("CRUD", customerCustomerDemoItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                customerCustomerDemoItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerCustomerDemoItemModel.OperationResult);
        }

        // POST: CustomerCustomerDemo/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CustomerCustomerDemoItemModel customerCustomerDemoItemModel)
        {
            try
            {
                if (IsDelete(customerCustomerDemoItemModel.OperationResult))
                {
                    if (Application.Delete(customerCustomerDemoItemModel.OperationResult, (CustomerCustomerDemoDTO)customerCustomerDemoItemModel.CustomerCustomerDemo.ToDTO()))
                    {
                        return JsonResultSuccess(customerCustomerDemoItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                customerCustomerDemoItemModel.OperationResult.ParseException(exception);
            }

            customerCustomerDemoItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(customerCustomerDemoItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: CustomerCustomerDemo/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            SyncfusionDataResult dataResult = new SyncfusionDataResult();
            dataResult.result = new List<CustomerCustomerDemoViewModel>();

            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(CustomerCustomerDemo), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    dataResult.result = ZViewHelper<CustomerCustomerDemoViewModel, CustomerCustomerDemoDTO, CustomerCustomerDemo>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: CustomerCustomerDemo/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, CustomerCustomerDemoResources.EntitySingular + ".xlsx");
            }
        }

        // POST: CustomerCustomerDemo/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, CustomerCustomerDemoResources.EntitySingular + ".pdf");
            }
        }

        // POST: CustomerCustomerDemo/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, CustomerCustomerDemoResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}