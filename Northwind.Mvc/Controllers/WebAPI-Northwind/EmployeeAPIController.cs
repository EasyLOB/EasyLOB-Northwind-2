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
    public class EmployeeAPIController : BaseApiControllerApplication<EmployeeDTO, Employee>
    {
        #region Methods

        public EmployeeAPIController(INorthwindGenericApplicationDTO<EmployeeDTO, Employee> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/employeeapi/1
        [Route("api/employeeapi/{employeeId}")]
        public IHttpActionResult DeleteEmployee(int employeeId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                EmployeeDTO employeeDTO = Application.GetById(operationResult, new object[] { employeeId });    
                if (employeeDTO != null)
                {
                    if (Application.Delete(operationResult, employeeDTO))
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

        // GET: api/employeeapi
        public IHttpActionResult GetEmployees()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<EmployeeDTO>>(Application.Select(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<EmployeeDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/employeeapi/1
        [Route("api/employeeapi/{employeeId}")]
        public IHttpActionResult GetEmployee(int employeeId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                EmployeeDTO employeeDTO = Application.GetById(operationResult, new object[] { employeeId });   
                if (employeeDTO != null)
                {
                    return Ok(employeeDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/employeeapi
        public IHttpActionResult PostEmployee(EmployeeDTO employeeDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, employeeDTO))
                {
                    return CreatedAtRoute("DefaultApi", new { employeeDTO.EmployeeId }, employeeDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/employeeapi/1
        [Route("api/employeeapi/{employeeId}")]
        public IHttpActionResult PutEmployee(int employeeId, EmployeeDTO employeeDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, employeeDTO))
                {
                    return Ok(employeeDTO);
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
