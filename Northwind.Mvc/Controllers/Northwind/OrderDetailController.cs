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
    public partial class OrderDetailController : BaseMvcControllerSCRUDApplication<OrderDetailDTO, OrderDetail>
    {
        #region Methods

        public OrderDetailController(INorthwindGenericApplicationDTO<OrderDetailDTO, OrderDetail> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: OrderDetail
        // GET: OrderDetail/Index
        [HttpGet]
        public ActionResult Index(int? masterOrderId = null, int? masterProductId = null)
        {
            OrderDetailCollectionModel orderDetailCollectionModel = new OrderDetailCollectionModel(ActivityOperations, "Index", null, masterOrderId, masterProductId);

            try
            {
                if (IsIndex(orderDetailCollectionModel.OperationResult))
                {
                    return View(orderDetailCollectionModel);
                }
            }
            catch (Exception exception)
            {
                orderDetailCollectionModel.OperationResult.ParseException(exception);
            }

            return View("OperationResult", new OperationResultModel(orderDetailCollectionModel.OperationResult));
        }        

        // GET & POST: OrderDetail/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, int? masterOrderId = null, int? masterProductId = null)
        {
            OrderDetailCollectionModel orderDetailCollectionModel = new OrderDetailCollectionModel(ActivityOperations, "Search", masterControllerAction, masterOrderId, masterProductId);

            try
            {
                if (IsOperation(orderDetailCollectionModel.OperationResult))
                {
                    return PartialView(orderDetailCollectionModel);
                }
            }
            catch (Exception exception)
            {
                orderDetailCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(orderDetailCollectionModel.OperationResult);
        }

        // GET & POST: OrderDetail/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_OrderDetailLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: OrderDetail/Create
        [HttpGet]
        public ActionResult Create(int? masterOrderId = null, int? masterProductId = null)
        {
            OrderDetailItemModel orderDetailItemModel = new OrderDetailItemModel(ActivityOperations, "Create", masterOrderId, masterProductId);

            try
            {
                if (IsCreate(orderDetailItemModel.OperationResult))
                {
                    return PartialView("CRUD", orderDetailItemModel);
                }            
            }
            catch (Exception exception)
            {
                orderDetailItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(orderDetailItemModel.OperationResult);
        }

        // POST: OrderDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderDetailItemModel orderDetailItemModel)
        {
            try
            {
                if (IsCreate(orderDetailItemModel.OperationResult))
                {
                    if (IsValid(orderDetailItemModel.OperationResult, orderDetailItemModel.OrderDetail))
                    {
                        OrderDetailDTO orderDetailDTO = (OrderDetailDTO)orderDetailItemModel.OrderDetail.ToDTO();
                        if (Application.Create(orderDetailItemModel.OperationResult, orderDetailDTO))
                        {
                            if (orderDetailItemModel.IsSave)
                            {
                                orderDetailItemModel.OperationResult.InformationMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(orderDetailItemModel.OperationResult,
                                    Url.Action("Update", "OrderDetail", new { OrderId = orderDetailDTO.OrderId, ProductId = orderDetailDTO.ProductId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(orderDetailItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                orderDetailItemModel.OperationResult.ParseException(exception);
            }

            orderDetailItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(orderDetailItemModel.OperationResult);
        }

        // GET: OrderDetail/Read/1
        [HttpGet]
        public ActionResult Read(int orderId, int productId, int? masterOrderId = null, int? masterProductId = null)
        {
            OrderDetailItemModel orderDetailItemModel = new OrderDetailItemModel(ActivityOperations, "Read", masterOrderId, masterProductId);
            
            try
            {
                if (IsRead(orderDetailItemModel.OperationResult))
                {
                    OrderDetailDTO orderDetailDTO = Application.GetById(orderDetailItemModel.OperationResult, new object[] { orderId, productId });
                    if (orderDetailDTO != null)
                    {
                        orderDetailItemModel.OrderDetail = new OrderDetailViewModel(orderDetailDTO);                    

                        return PartialView("CRUD", orderDetailItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                orderDetailItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(orderDetailItemModel.OperationResult);
        }

        // GET: OrderDetail/Update/1
        [HttpGet]
        public ActionResult Update(int orderId, int productId, int? masterOrderId = null, int? masterProductId = null)
        {
            OrderDetailItemModel orderDetailItemModel = new OrderDetailItemModel(ActivityOperations, "Update", masterOrderId, masterProductId);
            
            try
            {
                if (IsUpdate(orderDetailItemModel.OperationResult))
                {            
                    OrderDetailDTO orderDetailDTO = Application.GetById(orderDetailItemModel.OperationResult, new object[] { orderId, productId });
                    if (orderDetailDTO != null)
                    {
                        orderDetailItemModel.OrderDetail = new OrderDetailViewModel(orderDetailDTO);

                        return PartialView("CRUD", orderDetailItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                orderDetailItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(orderDetailItemModel.OperationResult);
        }

        // POST: OrderDetail/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OrderDetailItemModel orderDetailItemModel)
        {
            try
            {
                if (IsUpdate(orderDetailItemModel.OperationResult))
                {
                    if (IsValid(orderDetailItemModel.OperationResult, orderDetailItemModel.OrderDetail))
                    {
                        OrderDetailDTO orderDetailDTO = (OrderDetailDTO)orderDetailItemModel.OrderDetail.ToDTO();
                        if (Application.Update(orderDetailItemModel.OperationResult, orderDetailDTO))
                        {
                            if (orderDetailItemModel.IsSave)
                            {
                                return JsonResultSuccess(orderDetailItemModel.OperationResult,
                                    Url.Action("Update", "OrderDetail", new { OrderId = orderDetailDTO.OrderId, ProductId = orderDetailDTO.ProductId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(orderDetailItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                orderDetailItemModel.OperationResult.ParseException(exception);
            }

            orderDetailItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(orderDetailItemModel.OperationResult);
        }

        // GET: OrderDetail/Delete/1
        [HttpGet]
        public ActionResult Delete(int orderId, int productId, int? masterOrderId = null, int? masterProductId = null)
        {
            OrderDetailItemModel orderDetailItemModel = new OrderDetailItemModel(ActivityOperations, "Delete", masterOrderId, masterProductId);
            
            try
            {
                if (IsDelete(orderDetailItemModel.OperationResult))
                {            
                    OrderDetailDTO orderDetailDTO = Application.GetById(orderDetailItemModel.OperationResult, new object[] { orderId, productId });
                    if (orderDetailDTO != null)
                    {
                        orderDetailItemModel.OrderDetail = new OrderDetailViewModel(orderDetailDTO);

                        return PartialView("CRUD", orderDetailItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                orderDetailItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(orderDetailItemModel.OperationResult);
        }

        // POST: OrderDetail/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(OrderDetailItemModel orderDetailItemModel)
        {
            try
            {
                if (IsDelete(orderDetailItemModel.OperationResult))
                {
                    if (Application.Delete(orderDetailItemModel.OperationResult, (OrderDetailDTO)orderDetailItemModel.OrderDetail.ToDTO()))
                    {
                        return JsonResultSuccess(orderDetailItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                orderDetailItemModel.OperationResult.ParseException(exception);
            }

            orderDetailItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(orderDetailItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: OrderDetail/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            SyncfusionDataResult dataResult = new SyncfusionDataResult();
            dataResult.result = new List<OrderDetailViewModel>();

            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(OrderDetail), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    dataResult.result = ZViewHelper<OrderDetailViewModel, OrderDetailDTO, OrderDetail>.ToViewList(Application.Search(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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
                throw operationResult.Exception;
            }

            return Json(JsonConvert.SerializeObject(dataResult), JsonRequestBehavior.AllowGet);
        } 

        // POST: OrderDetail/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, OrderDetailResources.EntitySingular + ".xlsx");
            }
        }

        // POST: OrderDetail/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, OrderDetailResources.EntitySingular + ".pdf");
            }
        }

        // POST: OrderDetail/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, OrderDetailResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}