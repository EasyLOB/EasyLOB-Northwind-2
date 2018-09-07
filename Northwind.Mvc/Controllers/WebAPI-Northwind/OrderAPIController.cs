using Northwind;
using Northwind.Application;
using Northwind.Data;
using EasyLOB;
using EasyLOB.Library.AspNet;
using EasyLOB.Mvc;
using EasyLOB.WebApi;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Northwind.WebApi
{
    public class OrderAPIController : BaseApiControllerApplication<OrderDTO, Order>
    {
        #region Methods

        public OrderAPIController(INorthwindGenericApplicationDTO<OrderDTO, Order> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/orderapi/1
        [Route("api/orderapi/{orderId}")]
        public IHttpActionResult DeleteOrder(int orderId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                OrderDTO orderDTO = Application.GetById(operationResult, new object[] { orderId });    
                if (orderDTO != null)
                {
                    if (Application.Delete(operationResult, orderDTO))
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

        // GET: api/orderapi
        public IHttpActionResult GetOrders()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<OrderDTO>>(Application.Search(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<OrderDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/orderapi/1
        [Route("api/orderapi/{orderId}")]
        public IHttpActionResult GetOrder(int orderId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                OrderDTO orderDTO = Application.GetById(operationResult, new object[] { orderId });   
                if (orderDTO != null)
                {
                    return Ok(orderDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/orderapi
        public IHttpActionResult PostOrder(OrderDTO orderDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, orderDTO))
                {
                    return CreatedAtRoute("DefaultApi", new { orderDTO.OrderId }, orderDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/orderapi/1
        [Route("api/orderapi/{orderId}")]
        public IHttpActionResult PutOrder(int orderId, OrderDTO orderDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, orderDTO))
                {
                    return Ok(orderDTO);
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
