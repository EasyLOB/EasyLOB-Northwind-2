using Northwind.Application;
using Northwind.Data;
using Northwind.Data.Resources;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Mvc;
using Newtonsoft.Json;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Northwind.Mvc
{
    public partial class SupplierController : BaseMvcControllerSCRUDApplication<SupplierDTO, Supplier>
    {
        #region Methods

        public SupplierController(INorthwindGenericApplicationDTO<SupplierDTO, Supplier> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: Supplier
        // GET: Supplier/Index
        [HttpGet]
        public ActionResult Index()
        {
            SupplierCollectionModel supplierCollectionModel = new SupplierCollectionModel(ActivityOperations, "Index", null);

            try
            {
                if (IsIndex(supplierCollectionModel.OperationResult))
                {
                    return View(supplierCollectionModel);
                }
            }
            catch (Exception exception)
            {
                supplierCollectionModel.OperationResult.ParseException(exception);
            }

            return View("OperationResult", new OperationResultModel(supplierCollectionModel.OperationResult));
        }        

        // GET & POST: Supplier/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            SupplierCollectionModel supplierCollectionModel = new SupplierCollectionModel(ActivityOperations, "Search", masterControllerAction);

            try
            {
                if (IsOperation(supplierCollectionModel.OperationResult))
                {
                    return PartialView(supplierCollectionModel);
                }
            }
            catch (Exception exception)
            {
                supplierCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(supplierCollectionModel.OperationResult);
        }

        // GET & POST: Supplier/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_SupplierLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: Supplier/Create
        [HttpGet]
        public ActionResult Create()
        {
            SupplierItemModel supplierItemModel = new SupplierItemModel(ActivityOperations, "Create");

            try
            {
                if (IsCreate(supplierItemModel.OperationResult))
                {
                    return PartialView("CRUD", supplierItemModel);
                }            
            }
            catch (Exception exception)
            {
                supplierItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(supplierItemModel.OperationResult);
        }

        // POST: Supplier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierItemModel supplierItemModel)
        {
            try
            {
                if (IsCreate(supplierItemModel.OperationResult))
                {
                    if (IsValid(supplierItemModel.OperationResult, supplierItemModel.Supplier))
                    {
                        SupplierDTO supplierDTO = (SupplierDTO)supplierItemModel.Supplier.ToDTO();
                        if (Application.Create(supplierItemModel.OperationResult, supplierDTO))
                        {
                            if (supplierItemModel.IsSave)
                            {
                                supplierItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(supplierItemModel.OperationResult,
                                    Url.Action("Update", "Supplier", new { SupplierId = supplierDTO.SupplierId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(supplierItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                supplierItemModel.OperationResult.ParseException(exception);
            }

            supplierItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(supplierItemModel.OperationResult);
        }

        // GET: Supplier/Read/1
        [HttpGet]
        public ActionResult Read(int supplierId)
        {
            SupplierItemModel supplierItemModel = new SupplierItemModel(ActivityOperations, "Read");
            
            try
            {
                if (IsRead(supplierItemModel.OperationResult))
                {
                    SupplierDTO supplierDTO = Application.GetById(supplierItemModel.OperationResult, new object[] { supplierId });
                    if (supplierDTO != null)
                    {
                        supplierItemModel.Supplier = new SupplierViewModel(supplierDTO);                    

                        return PartialView("CRUD", supplierItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                supplierItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(supplierItemModel.OperationResult);
        }

        // GET: Supplier/Update/1
        [HttpGet]
        public ActionResult Update(int supplierId)
        {
            SupplierItemModel supplierItemModel = new SupplierItemModel(ActivityOperations, "Update");
            
            try
            {
                if (IsUpdate(supplierItemModel.OperationResult))
                {            
                    SupplierDTO supplierDTO = Application.GetById(supplierItemModel.OperationResult, new object[] { supplierId });
                    if (supplierDTO != null)
                    {
                        supplierItemModel.Supplier = new SupplierViewModel(supplierDTO);

                        return PartialView("CRUD", supplierItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                supplierItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(supplierItemModel.OperationResult);
        }

        // POST: Supplier/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SupplierItemModel supplierItemModel)
        {
            try
            {
                if (IsUpdate(supplierItemModel.OperationResult))
                {
                    if (IsValid(supplierItemModel.OperationResult, supplierItemModel.Supplier))
                    {
                        SupplierDTO supplierDTO = (SupplierDTO)supplierItemModel.Supplier.ToDTO();
                        if (Application.Update(supplierItemModel.OperationResult, supplierDTO))
                        {
                            if (supplierItemModel.IsSave)
                            {
                                return JsonResultSuccess(supplierItemModel.OperationResult,
                                    Url.Action("Update", "Supplier", new { SupplierId = supplierDTO.SupplierId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(supplierItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                supplierItemModel.OperationResult.ParseException(exception);
            }

            supplierItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(supplierItemModel.OperationResult);
        }

        // GET: Supplier/Delete/1
        [HttpGet]
        public ActionResult Delete(int supplierId)
        {
            SupplierItemModel supplierItemModel = new SupplierItemModel(ActivityOperations, "Delete");
            
            try
            {
                if (IsDelete(supplierItemModel.OperationResult))
                {            
                    SupplierDTO supplierDTO = Application.GetById(supplierItemModel.OperationResult, new object[] { supplierId });
                    if (supplierDTO != null)
                    {
                        supplierItemModel.Supplier = new SupplierViewModel(supplierDTO);

                        return PartialView("CRUD", supplierItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                supplierItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(supplierItemModel.OperationResult);
        }

        // POST: Supplier/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(SupplierItemModel supplierItemModel)
        {
            try
            {
                if (IsDelete(supplierItemModel.OperationResult))
                {
                    if (Application.Delete(supplierItemModel.OperationResult, (SupplierDTO)supplierItemModel.Supplier.ToDTO()))
                    {
                        return JsonResultSuccess(supplierItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                supplierItemModel.OperationResult.ParseException(exception);
            }

            supplierItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(supplierItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Supplier/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            SyncfusionDataResult dataResult = new SyncfusionDataResult();
            dataResult.result = new List<SupplierViewModel>();

            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Supplier), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    dataResult.result = ZViewHelper<SupplierViewModel, SupplierDTO, Supplier>.ToViewList(Application.Search(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: Supplier/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, SupplierResources.EntitySingular + ".xlsx");
            }
        }

        // POST: Supplier/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, SupplierResources.EntitySingular + ".pdf");
            }
        }

        // POST: Supplier/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, SupplierResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}