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
    public class CustomerCustomerDemoAPIController : BaseApiControllerApplication<CustomerCustomerDemoDTO, CustomerCustomerDemo>
    {
        #region Methods

        public CustomerCustomerDemoAPIController(INorthwindGenericApplicationDTO<CustomerCustomerDemoDTO, CustomerCustomerDemo> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/customerCustomerDemoapi/1
        [Route("api/customerCustomerDemoapi/{customerId}/{customerTypeId}")]
        public IHttpActionResult DeleteCustomerCustomerDemo(string customerId, string customerTypeId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                CustomerCustomerDemoDTO customerCustomerDemoDTO = Application.GetById(operationResult, new object[] { customerId, customerTypeId });    
                if (customerCustomerDemoDTO != null)
                {
                    if (Application.Delete(operationResult, customerCustomerDemoDTO))
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

        // GET: api/customerCustomerDemoapi
        public IHttpActionResult GetCustomerCustomerDemos()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<CustomerCustomerDemoDTO>>(Application.Search(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<CustomerCustomerDemoDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/customerCustomerDemoapi/1
        [Route("api/customerCustomerDemoapi/{customerId}/{customerTypeId}")]
        public IHttpActionResult GetCustomerCustomerDemo(string customerId, string customerTypeId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                CustomerCustomerDemoDTO customerCustomerDemoDTO = Application.GetById(operationResult, new object[] { customerId, customerTypeId });   
                if (customerCustomerDemoDTO != null)
                {
                    return Ok(customerCustomerDemoDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/customerCustomerDemoapi
        public IHttpActionResult PostCustomerCustomerDemo(CustomerCustomerDemoDTO customerCustomerDemoDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, customerCustomerDemoDTO))
                {
                    return CreatedAtRoute("DefaultApi", new { customerCustomerDemoDTO.CustomerId, customerCustomerDemoDTO.CustomerTypeId }, customerCustomerDemoDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/customerCustomerDemoapi/1
        [Route("api/customerCustomerDemoapi/{customerId}/{customerTypeId}")]
        public IHttpActionResult PutCustomerCustomerDemo(string customerId, string customerTypeId, CustomerCustomerDemoDTO customerCustomerDemoDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, customerCustomerDemoDTO))
                {
                    return Ok(customerCustomerDemoDTO);
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
