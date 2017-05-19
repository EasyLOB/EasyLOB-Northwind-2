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

namespace Northwind.Mvc
{
    public class CustomerDemographicAPIController : BaseApiControllerApplication<CustomerDemographicDTO, CustomerDemographic>
    {
        #region Methods

        public CustomerDemographicAPIController(INorthwindGenericApplicationDTO<CustomerDemographicDTO, CustomerDemographic> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/customerDemographicapi/1
        [Route("api/customerDemographicapi/{customerTypeId}")]
        public IHttpActionResult DeleteCustomerDemographic(string customerTypeId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                CustomerDemographicDTO customerDemographicDTO = Application.GetById(operationResult, new object[] { customerTypeId });    
                if (customerDemographicDTO != null)
                {
                    if (Application.Delete(operationResult, customerDemographicDTO))
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

        // GET: api/customerDemographicapi
        public IHttpActionResult GetCustomerDemographics()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<CustomerDemographicDTO>>(Application.Select(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<CustomerDemographicDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/customerDemographicapi/1
        [Route("api/customerDemographicapi/{customerTypeId}")]
        public IHttpActionResult GetCustomerDemographic(string customerTypeId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                CustomerDemographicDTO customerDemographicDTO = Application.GetById(operationResult, new object[] { customerTypeId });   
                if (customerDemographicDTO != null)
                {
                    return Ok(customerDemographicDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/customerDemographicapi
        public IHttpActionResult PostCustomerDemographic(CustomerDemographicDTO customerDemographicDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (IsValid(operationResult, customerDemographicDTO))
                {
                    if (Application.Create(operationResult, customerDemographicDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { customerDemographicDTO.CustomerTypeId }, customerDemographicDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/customerDemographicapi/1
        [Route("api/customerDemographicapi/{customerTypeId}")]
        public IHttpActionResult PutCustomerDemographic(string customerTypeId, CustomerDemographicDTO customerDemographicDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (IsValid(operationResult, customerDemographicDTO))
                {
                    if (Application.Create(operationResult, customerDemographicDTO))
                    {
                        return Ok(customerDemographicDTO);
                    }
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
