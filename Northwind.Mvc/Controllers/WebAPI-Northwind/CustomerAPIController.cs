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
    public class CustomerAPIController : BaseApiControllerApplication<CustomerDTO, Customer>
    {
        #region Methods

        public CustomerAPIController(INorthwindGenericApplicationDTO<CustomerDTO, Customer> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/customerapi/1
        [Route("api/customerapi/{customerId}")]
        public IHttpActionResult DeleteCustomer(string customerId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                CustomerDTO customerDTO = Application.GetById(operationResult, new object[] { customerId });    
                if (customerDTO != null)
                {
                    if (Application.Delete(operationResult, customerDTO))
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

        // GET: api/customerapi
        public IHttpActionResult GetCustomers()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<CustomerDTO>>(Application.Search(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<CustomerDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/customerapi/1
        [Route("api/customerapi/{customerId}")]
        public IHttpActionResult GetCustomer(string customerId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                CustomerDTO customerDTO = Application.GetById(operationResult, new object[] { customerId });   
                if (customerDTO != null)
                {
                    return Ok(customerDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/customerapi
        public IHttpActionResult PostCustomer(CustomerDTO customerDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, customerDTO))
                {
                    return CreatedAtRoute("DefaultApi", new { customerDTO.CustomerId }, customerDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/customerapi/1
        [Route("api/customerapi/{customerId}")]
        public IHttpActionResult PutCustomer(string customerId, CustomerDTO customerDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, customerDTO))
                {
                    return Ok(customerDTO);
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
