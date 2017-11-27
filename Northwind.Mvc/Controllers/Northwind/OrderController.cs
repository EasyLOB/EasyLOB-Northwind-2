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
    public partial class OrderController : BaseMvcControllerSCRUDApplication<OrderDTO, Order>
    {
        #region Methods

        public OrderController(INorthwindGenericApplicationDTO<OrderDTO, Order> application)
        {
            Application = application;            
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: Order
        // GET: Order/Index
        [HttpGet]
        public ActionResult Index(string masterCustomerId = null, int? masterEmployeeId = null, int? masterShipVia = null)
        {
            OrderCollectionModel orderCollectionModel = new OrderCollectionModel(ActivityOperations, "Index", null, masterCustomerId, masterEmployeeId, masterShipVia);

            try
            {
                if (IsIndex(orderCollectionModel.OperationResult))
                {
                    return View(orderCollectionModel);
                }
            }
            catch (Exception exception)
            {
                orderCollectionModel.OperationResult.ParseException(exception);
            }

            return View("OperationResult", new OperationResultViewModel(orderCollectionModel.OperationResult));
        }        

        // GET & POST: Order/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, string masterCustomerId = null, int? masterEmployeeId = null, int? masterShipVia = null)
        {
            OrderCollectionModel orderCollectionModel = new OrderCollectionModel(ActivityOperations, "Search", masterControllerAction, masterCustomerId, masterEmployeeId, masterShipVia);

            try
            {
                if (IsOperation(orderCollectionModel.OperationResult))
                {
                    return PartialView(orderCollectionModel);
                }
            }
            catch (Exception exception)
            {
                orderCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(orderCollectionModel.OperationResult);
        }

        // GET & POST: Order/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_OrderLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: Order/Create
        [HttpGet]
        public ActionResult Create(string masterCustomerId = null, int? masterEmployeeId = null, int? masterShipVia = null)
        {
            OrderItemModel orderItemModel = new OrderItemModel(ActivityOperations, "Create", masterCustomerId, masterEmployeeId, masterShipVia);

            try
            {
                if (IsCreate(orderItemModel.OperationResult))
                {
                    return PartialView("CRUD", orderItemModel);
                }            
            }
            catch (Exception exception)
            {
                orderItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(orderItemModel.OperationResult);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderItemModel orderItemModel)
        {
            try
            {
                if (IsCreate(orderItemModel.OperationResult))
                {
                    if (IsValid(orderItemModel.OperationResult, orderItemModel.Order))
                    {
                        OrderDTO orderDTO = (OrderDTO)orderItemModel.Order.ToDTO();
                        if (Application.Create(orderItemModel.OperationResult, orderDTO))
                        {
                            if (orderItemModel.IsSave)
                            {
                                orderItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(orderItemModel.OperationResult,
                                    Url.Action("Update", "Order", new { OrderId = orderDTO.OrderId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(orderItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                orderItemModel.OperationResult.ParseException(exception);
            }

            orderItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(orderItemModel.OperationResult);
        }

        // GET: Order/Read/1
        [HttpGet]
        public ActionResult Read(int orderId, string masterCustomerId = null, int? masterEmployeeId = null, int? masterShipVia = null)
        {
            OrderItemModel orderItemModel = new OrderItemModel(ActivityOperations, "Read", masterCustomerId, masterEmployeeId, masterShipVia);
            
            try
            {
                if (IsRead(orderItemModel.OperationResult))
                {
                    OrderDTO orderDTO = Application.GetById(orderItemModel.OperationResult, new object[] { orderId });
                    if (orderDTO != null)
                    {
                        orderItemModel.Order = new OrderViewModel(orderDTO);                    

                        return PartialView("CRUD", orderItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                orderItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(orderItemModel.OperationResult);
        }

        // GET: Order/Update/1
        [HttpGet]
        public ActionResult Update(int orderId, string masterCustomerId = null, int? masterEmployeeId = null, int? masterShipVia = null)
        {
            OrderItemModel orderItemModel = new OrderItemModel(ActivityOperations, "Update", masterCustomerId, masterEmployeeId, masterShipVia);
            
            try
            {
                if (IsUpdate(orderItemModel.OperationResult))
                {            
                    OrderDTO orderDTO = Application.GetById(orderItemModel.OperationResult, new object[] { orderId });
                    if (orderDTO != null)
                    {
                        orderItemModel.Order = new OrderViewModel(orderDTO);

                        return PartialView("CRUD", orderItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                orderItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(orderItemModel.OperationResult);
        }

        // POST: Order/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(OrderItemModel orderItemModel)
        {
            try
            {
                if (IsUpdate(orderItemModel.OperationResult))
                {
                    if (IsValid(orderItemModel.OperationResult, orderItemModel.Order))
                    {
                        OrderDTO orderDTO = (OrderDTO)orderItemModel.Order.ToDTO();
                        if (Application.Update(orderItemModel.OperationResult, orderDTO))
                        {
                            if (orderItemModel.IsSave)
                            {
                                return JsonResultSuccess(orderItemModel.OperationResult,
                                    Url.Action("Update", "Order", new { OrderId = orderDTO.OrderId }, Request.Url.Scheme));
                            }
                            else
                            {
                                return JsonResultSuccess(orderItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                orderItemModel.OperationResult.ParseException(exception);
            }

            orderItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(orderItemModel.OperationResult);
        }

        // GET: Order/Delete/1
        [HttpGet]
        public ActionResult Delete(int orderId, string masterCustomerId = null, int? masterEmployeeId = null, int? masterShipVia = null)
        {
            OrderItemModel orderItemModel = new OrderItemModel(ActivityOperations, "Delete", masterCustomerId, masterEmployeeId, masterShipVia);
            
            try
            {
                if (IsDelete(orderItemModel.OperationResult))
                {            
                    OrderDTO orderDTO = Application.GetById(orderItemModel.OperationResult, new object[] { orderId });
                    if (orderDTO != null)
                    {
                        orderItemModel.Order = new OrderViewModel(orderDTO);

                        return PartialView("CRUD", orderItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                orderItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(orderItemModel.OperationResult);
        }

        // POST: Order/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(OrderItemModel orderItemModel)
        {
            try
            {
                if (IsDelete(orderItemModel.OperationResult))
                {
                    if (Application.Delete(orderItemModel.OperationResult, (OrderDTO)orderItemModel.Order.ToDTO()))
                    {
                        return JsonResultSuccess(orderItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                orderItemModel.OperationResult.ParseException(exception);
            }

            orderItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(orderItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Order/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            SyncfusionDataResult dataResult = new SyncfusionDataResult();
            dataResult.result = new List<OrderViewModel>();

            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch(operationResult))
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Order), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    dataResult.result = ZViewHelper<OrderViewModel, OrderDTO, Order>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: Order/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, OrderResources.EntitySingular + ".xlsx");
            }
        }

        // POST: Order/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, OrderResources.EntitySingular + ".pdf");
            }
        }

        // POST: Order/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, OrderResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}