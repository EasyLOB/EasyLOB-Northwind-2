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
    public partial class CategoryController : BaseMvcControllerSCRUDApplication<CategoryDTO, Category>
    {
        #region Methods

        public CategoryController(INorthwindGenericApplicationDTO<CategoryDTO, Category> application)
        {
            Application = application;            
        }

        #endregion
        
        #region Methods SCRUD

        // GET: Category
        // GET: Category/Index
        [HttpGet]
        public ActionResult Index()
        {
            CategoryCollectionModel categoryCollectionModel = new CategoryCollectionModel(ActivityOperations, "Index", null);

            try
            {
                IsOperation(categoryCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                categoryCollectionModel.OperationResult.ParseException(exception);
            }

            return View(categoryCollectionModel);
        }        

        // GET & POST: Category/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            CategoryCollectionModel categoryCollectionModel = new CategoryCollectionModel(ActivityOperations, "Search", masterControllerAction);

            try
            {
                if (IsOperation(categoryCollectionModel.OperationResult))
                {
                    return PartialView(categoryCollectionModel);
                }
            }
            catch (Exception exception)
            {
                categoryCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(categoryCollectionModel.OperationResult);
        }

        // GET & POST: Category/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_CategoryLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: Category/Create
        [HttpGet]
        public ActionResult Create()
        {
            CategoryItemModel categoryItemModel = new CategoryItemModel(ActivityOperations, "Create");

            try
            {
                if (IsCreate(categoryItemModel.OperationResult))
                {
                    return PartialView("CRUD", categoryItemModel);
                }            
            }
            catch (Exception exception)
            {
                categoryItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(categoryItemModel.OperationResult);
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryItemModel categoryItemModel)
        {
            try
            {
                if (IsCreate(categoryItemModel.OperationResult))
                {
                    if (IsValid(categoryItemModel.OperationResult, categoryItemModel.Category))
                    {
                        CategoryDTO categoryDTO = (CategoryDTO)categoryItemModel.Category.ToDTO();
                        if (Application.Create(categoryItemModel.OperationResult, categoryDTO))
                        {
                            if (categoryItemModel.IsSave)
                            {
                                categoryItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(categoryItemModel.OperationResult,
                                    Url.Action("Update", "Category", new { CategoryId = categoryDTO.CategoryId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(categoryItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                categoryItemModel.OperationResult.ParseException(exception);
            }

            categoryItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(categoryItemModel.OperationResult);
        }

        // GET: Category/Read/1
        [HttpGet]
        public ActionResult Read(int categoryId)
        {
            CategoryItemModel categoryItemModel = new CategoryItemModel(ActivityOperations, "Read");
            
            try
            {
                if (IsRead(categoryItemModel.OperationResult))
                {
                    CategoryDTO categoryDTO = Application.GetById(categoryItemModel.OperationResult, new object[] { categoryId });
                    if (categoryDTO != null)
                    {
                        categoryItemModel.Category = new CategoryViewModel(categoryDTO);                    

                        return PartialView("CRUD", categoryItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                categoryItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(categoryItemModel.OperationResult);
        }

        // GET: Category/Update/1
        [HttpGet]
        public ActionResult Update(int categoryId)
        {
            CategoryItemModel categoryItemModel = new CategoryItemModel(ActivityOperations, "Update");
            
            try
            {
                if (IsUpdate(categoryItemModel.OperationResult))
                {            
                    CategoryDTO categoryDTO = Application.GetById(categoryItemModel.OperationResult, new object[] { categoryId });
                    if (categoryDTO != null)
                    {
                        categoryItemModel.Category = new CategoryViewModel(categoryDTO);

                        return PartialView("CRUD", categoryItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                categoryItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(categoryItemModel.OperationResult);
        }

        // POST: Category/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CategoryItemModel categoryItemModel)
        {
            try
            {
                if (IsUpdate(categoryItemModel.OperationResult))
                {
                    if (IsValid(categoryItemModel.OperationResult, categoryItemModel.Category))
                    {
                        CategoryDTO categoryDTO = (CategoryDTO)categoryItemModel.Category.ToDTO();
                        if (Application.Update(categoryItemModel.OperationResult, categoryDTO))
                        {
                            if (categoryItemModel.IsSave)
                            {
                                return JsonResultSuccess(categoryItemModel.OperationResult,
                                    Url.Action("Update", "Category", new { CategoryId = categoryDTO.CategoryId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(categoryItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                categoryItemModel.OperationResult.ParseException(exception);
            }

            categoryItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(categoryItemModel.OperationResult);
        }

        // GET: Category/Delete/1
        [HttpGet]
        public ActionResult Delete(int categoryId)
        {
            CategoryItemModel categoryItemModel = new CategoryItemModel(ActivityOperations, "Delete");
            
            try
            {
                if (IsDelete(categoryItemModel.OperationResult))
                {            
                    CategoryDTO categoryDTO = Application.GetById(categoryItemModel.OperationResult, new object[] { categoryId });
                    if (categoryDTO != null)
                    {
                        categoryItemModel.Category = new CategoryViewModel(categoryDTO);

                        return PartialView("CRUD", categoryItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                categoryItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(categoryItemModel.OperationResult);
        }

        // POST: Category/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CategoryItemModel categoryItemModel)
        {
            try
            {
                if (IsDelete(categoryItemModel.OperationResult))
                {
                    if (Application.Delete(categoryItemModel.OperationResult, (CategoryDTO)categoryItemModel.Category.ToDTO()))
                    {
                        return JsonResultSuccess(categoryItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                categoryItemModel.OperationResult.ParseException(exception);
            }

            categoryItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(categoryItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Category/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<CategoryViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Category), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<CategoryViewModel, CategoryDTO, Category>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: Category/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, CategoryResources.EntitySingular + ".xlsx");
            }
        }

        // POST: Category/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, CategoryResources.EntitySingular + ".pdf");
            }
        }

        // POST: Category/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, CategoryResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}