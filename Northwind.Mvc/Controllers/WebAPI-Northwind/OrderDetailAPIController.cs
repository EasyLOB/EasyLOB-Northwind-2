using Northwind;
using Northwind.Application;
using Northwind.Data;
using EasyLOB;
using EasyLOB.Library.WebApi;
using EasyLOB.Mvc;
using EasyLOB.WebApi;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Northwind.WebApi
{
    public class OrderDetailAPIController : BaseApiControllerApplication<OrderDetailDTO, OrderDetail>
    {
        #region Methods

        public OrderDetailAPIController(INorthwindGenericApplicationDTO<OrderDetailDTO, OrderDetail> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/orderDetailapi/1
        [Route("api/orderDetailapi/{orderId}/{productId}")]
        public IHttpActionResult DeleteOrderDetail(int orderId, int productId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                OrderDetailDTO orderDetailDTO = Application.GetById(operationResult, new object[] { orderId, productId });    
                if (orderDetailDTO != null)
                {
                    if (Application.Delete(operationResult, orderDetailDTO))
                    {
                        return Ok();
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/orderDetailapi
        public IHttpActionResult GetOrderDetails()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<OrderDetailDTO>>(Application.Search(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<OrderDetailDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/orderDetailapi/1
        [Route("api/orderDetailapi/{orderId}/{productId}")]
        public IHttpActionResult GetOrderDetail(int orderId, int productId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                OrderDetailDTO orderDetailDTO = Application.GetById(operationResult, new object[] { orderId, productId });   
                if (orderDetailDTO != null)
                {
                    return Ok(orderDetailDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/orderDetailapi
        public IHttpActionResult PostOrderDetail(OrderDetailDTO orderDetailDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, orderDetailDTO))
                {
                    return CreatedAtRoute("DefaultApi", new { orderDetailDTO.OrderId, orderDetailDTO.ProductId }, orderDetailDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/orderDetailapi/1
        [Route("api/orderDetailapi/{orderId}/{productId}")]
        public IHttpActionResult PutOrderDetail(int orderId, int productId, OrderDetailDTO orderDetailDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, orderDetailDTO))
                {
                    return Ok(orderDetailDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        #endregion Methods REST
    }
}
