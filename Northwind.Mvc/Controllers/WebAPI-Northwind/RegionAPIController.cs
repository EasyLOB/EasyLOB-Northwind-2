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
    public class RegionAPIController : BaseApiControllerApplication<RegionDTO, Region>
    {
        #region Methods

        public RegionAPIController(INorthwindGenericApplicationDTO<RegionDTO, Region> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/regionapi/1
        [Route("api/regionapi/{regionId}")]
        public IHttpActionResult DeleteRegion(int regionId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                RegionDTO regionDTO = Application.GetById(operationResult, new object[] { regionId });    
                if (regionDTO != null)
                {
                    if (Application.Delete(operationResult, regionDTO))
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

        // GET: api/regionapi
        public IHttpActionResult GetRegions()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<RegionDTO>>(Application.Search(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<RegionDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/regionapi/1
        [Route("api/regionapi/{regionId}")]
        public IHttpActionResult GetRegion(int regionId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                RegionDTO regionDTO = Application.GetById(operationResult, new object[] { regionId });   
                if (regionDTO != null)
                {
                    return Ok(regionDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/regionapi
        public IHttpActionResult PostRegion(RegionDTO regionDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, regionDTO))
                {
                    return CreatedAtRoute("DefaultApi", new { regionDTO.RegionId }, regionDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/regionapi/1
        [Route("api/regionapi/{regionId}")]
        public IHttpActionResult PutRegion(int regionId, RegionDTO regionDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, regionDTO))
                {
                    return Ok(regionDTO);
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
