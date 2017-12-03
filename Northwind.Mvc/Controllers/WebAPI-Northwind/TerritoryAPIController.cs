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
    public class TerritoryAPIController : BaseApiControllerApplication<TerritoryDTO, Territory>
    {
        #region Methods

        public TerritoryAPIController(INorthwindGenericApplicationDTO<TerritoryDTO, Territory> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/territoryapi/1
        [Route("api/territoryapi/{territoryId}")]
        public IHttpActionResult DeleteTerritory(string territoryId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                TerritoryDTO territoryDTO = Application.GetById(operationResult, new object[] { territoryId });    
                if (territoryDTO != null)
                {
                    if (Application.Delete(operationResult, territoryDTO))
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

        // GET: api/territoryapi
        public IHttpActionResult GetTerritories()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<TerritoryDTO>>(Application.Search(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<TerritoryDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/territoryapi/1
        [Route("api/territoryapi/{territoryId}")]
        public IHttpActionResult GetTerritory(string territoryId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                TerritoryDTO territoryDTO = Application.GetById(operationResult, new object[] { territoryId });   
                if (territoryDTO != null)
                {
                    return Ok(territoryDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/territoryapi
        public IHttpActionResult PostTerritory(TerritoryDTO territoryDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, territoryDTO))
                {
                    return CreatedAtRoute("DefaultApi", new { territoryDTO.TerritoryId }, territoryDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/territoryapi/1
        [Route("api/territoryapi/{territoryId}")]
        public IHttpActionResult PutTerritory(string territoryId, TerritoryDTO territoryDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, territoryDTO))
                {
                    return Ok(territoryDTO);
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
