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
    public partial class ProductController : BaseMvcControllerSCRUDApplication<ProductDTO, Product>
    {
        #region Methods

        public ProductController(INorthwindGenericApplicationDTO<ProductDTO, Product> application)
        {
            Application = application;            
        }

        #endregion
        
        #region Methods SCRUD

        // GET: Product
        // GET: Product/Index
        [HttpGet]
        public ActionResult Index(int? masterCategoryId = null, int? masterSupplierId = null)
        {
            ProductCollectionModel productCollectionModel = new ProductCollectionModel(ActivityOperations, "Index", null, masterCategoryId, masterSupplierId);

            try
            {
                IsOperation(productCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                productCollectionModel.OperationResult.ParseException(exception);
            }

            return View(productCollectionModel);
        }        

        // GET & POST: Product/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, int? masterCategoryId = null, int? masterSupplierId = null)
        {
            ProductCollectionModel productCollectionModel = new ProductCollectionModel(ActivityOperations, "Search", masterControllerAction, masterCategoryId, masterSupplierId);

            try
            {
                if (IsOperation(productCollectionModel.OperationResult))
                {
                    return PartialView(productCollectionModel);
                }
            }
            catch (Exception exception)
            {
                productCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(productCollectionModel.OperationResult);
        }

        // GET & POST: Product/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_ProductLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: Product/Create
        [HttpGet]
        public ActionResult Create(int? masterCategoryId = null, int? masterSupplierId = null)
        {
            ProductItemModel productItemModel = new ProductItemModel(ActivityOperations, "Create", masterCategoryId, masterSupplierId);

            try
            {
                if (IsCreate(productItemModel.OperationResult))
                {
                    return PartialView("CRUD", productItemModel);
                }            
            }
            catch (Exception exception)
            {
                productItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(productItemModel.OperationResult);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductItemModel productItemModel)
        {
            try
            {
                if (IsCreate(productItemModel.OperationResult))
                {
                    if (IsValid(productItemModel.OperationResult, productItemModel.Product))
                    {
                        ProductDTO productDTO = (ProductDTO)productItemModel.Product.ToDTO();
                        if (Application.Create(productItemModel.OperationResult, productDTO))
                        {
                            if (productItemModel.IsSave)
                            {
                                productItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(productItemModel.OperationResult,
                                    Url.Content(String.Format("~/Product/Update?ProductId={0}", productDTO.ProductId)));
                            }
                            else
                            {
                                return JsonResultSuccess(productItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                productItemModel.OperationResult.ParseException(exception);
            }

            productItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(productItemModel.OperationResult);
        }

        // GET: Product/Read/1
        [HttpGet]
        public ActionResult Read(int productId, int? masterCategoryId = null, int? masterSupplierId = null)
        {
            ProductItemModel productItemModel = new ProductItemModel(ActivityOperations, "Read", masterCategoryId, masterSupplierId);
            
            try
            {
                if (IsRead(productItemModel.OperationResult))
                {
                    ProductDTO productDTO = Application.GetById(productItemModel.OperationResult, new object[] { productId });
                    if (productDTO != null)
                    {
                        productItemModel.Product = new ProductViewModel(productDTO);                    

                        return PartialView("CRUD", productItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                productItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(productItemModel.OperationResult);
        }

        // GET: Product/Update/1
        [HttpGet]
        public ActionResult Update(int productId, int? masterCategoryId = null, int? masterSupplierId = null)
        {
            ProductItemModel productItemModel = new ProductItemModel(ActivityOperations, "Update", masterCategoryId, masterSupplierId);
            
            try
            {
                if (IsUpdate(productItemModel.OperationResult))
                {            
                    ProductDTO productDTO = Application.GetById(productItemModel.OperationResult, new object[] { productId });
                    if (productDTO != null)
                    {
                        productItemModel.Product = new ProductViewModel(productDTO);

                        return PartialView("CRUD", productItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                productItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(productItemModel.OperationResult);
        }

        // POST: Product/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProductItemModel productItemModel)
        {
            try
            {
                if (IsUpdate(productItemModel.OperationResult))
                {
                    if (IsValid(productItemModel.OperationResult, productItemModel.Product))
                    {
                        ProductDTO productDTO = (ProductDTO)productItemModel.Product.ToDTO();
                        if (Application.Update(productItemModel.OperationResult, productDTO))
                        {
                            if (productItemModel.IsSave)
                            {
                                return JsonResultSuccess(productItemModel.OperationResult,
                                    Url.Content(String.Format("~/Product/Update?ProductId={0}", productDTO.ProductId)));
                            }
                            else
                            {
                                return JsonResultSuccess(productItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                productItemModel.OperationResult.ParseException(exception);
            }

            productItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(productItemModel.OperationResult);
        }

        // GET: Product/Delete/1
        [HttpGet]
        public ActionResult Delete(int productId, int? masterCategoryId = null, int? masterSupplierId = null)
        {
            ProductItemModel productItemModel = new ProductItemModel(ActivityOperations, "Delete", masterCategoryId, masterSupplierId);
            
            try
            {
                if (IsDelete(productItemModel.OperationResult))
                {            
                    ProductDTO productDTO = Application.GetById(productItemModel.OperationResult, new object[] { productId });
                    if (productDTO != null)
                    {
                        productItemModel.Product = new ProductViewModel(productDTO);

                        return PartialView("CRUD", productItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                productItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(productItemModel.OperationResult);
        }

        // POST: Product/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductItemModel productItemModel)
        {
            try
            {
                if (IsDelete(productItemModel.OperationResult))
                {
                    if (Application.Delete(productItemModel.OperationResult, (ProductDTO)productItemModel.Product.ToDTO()))
                    {
                        return JsonResultSuccess(productItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                productItemModel.OperationResult.ParseException(exception);
            }

            productItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(productItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Product/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<ProductViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Product), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<ProductViewModel, ProductDTO, Product>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: Product/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, ProductResources.EntitySingular + ".xlsx");
            }
        }

        // POST: Product/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, ProductResources.EntitySingular + ".pdf");
            }
        }

        // POST: Product/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, ProductResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}