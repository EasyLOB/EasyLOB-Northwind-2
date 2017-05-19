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
    public class EmployeeTerritoryAPIController : BaseApiControllerApplication<EmployeeTerritoryDTO, EmployeeTerritory>
    {
        #region Methods

        public EmployeeTerritoryAPIController(INorthwindGenericApplicationDTO<EmployeeTerritoryDTO, EmployeeTerritory> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/employeeTerritoryapi/1
        [Route("api/employeeTerritoryapi/{employeeId}/{territoryId}")]
        public IHttpActionResult DeleteEmployeeTerritory(int employeeId, string territoryId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                EmployeeTerritoryDTO employeeTerritoryDTO = Application.GetById(operationResult, new object[] { employeeId, territoryId });    
                if (employeeTerritoryDTO != null)
                {
                    if (Application.Delete(operationResult, employeeTerritoryDTO))
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

        // GET: api/employeeTerritoryapi
        public IHttpActionResult GetEmployeeTerritories()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<EmployeeTerritoryDTO>>(Application.Select(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<EmployeeTerritoryDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/employeeTerritoryapi/1
        [Route("api/employeeTerritoryapi/{employeeId}/{territoryId}")]
        public IHttpActionResult GetEmployeeTerritory(int employeeId, string territoryId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                EmployeeTerritoryDTO employeeTerritoryDTO = Application.GetById(operationResult, new object[] { employeeId, territoryId });   
                if (employeeTerritoryDTO != null)
                {
                    return Ok(employeeTerritoryDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/employeeTerritoryapi
        public IHttpActionResult PostEmployeeTerritory(EmployeeTerritoryDTO employeeTerritoryDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (IsValid(operationResult, employeeTerritoryDTO))
                {
                    if (Application.Create(operationResult, employeeTerritoryDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { employeeTerritoryDTO.EmployeeId, employeeTerritoryDTO.TerritoryId }, employeeTerritoryDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/employeeTerritoryapi/1
        [Route("api/employeeTerritoryapi/{employeeId}/{territoryId}")]
        public IHttpActionResult PutEmployeeTerritory(int employeeId, string territoryId, EmployeeTerritoryDTO employeeTerritoryDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (IsValid(operationResult, employeeTerritoryDTO))
                {
                    if (Application.Create(operationResult, employeeTerritoryDTO))
                    {
                        return Ok(employeeTerritoryDTO);
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
