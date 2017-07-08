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
    public class ShipperAPIController : BaseApiControllerApplication<ShipperDTO, Shipper>
    {
        #region Methods

        public ShipperAPIController(INorthwindGenericApplicationDTO<ShipperDTO, Shipper> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/shipperapi/1
        [Route("api/shipperapi/{shipperId}")]
        public IHttpActionResult DeleteShipper(int shipperId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                ShipperDTO shipperDTO = Application.GetById(operationResult, new object[] { shipperId });    
                if (shipperDTO != null)
                {
                    if (Application.Delete(operationResult, shipperDTO))
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

        // GET: api/shipperapi
        public IHttpActionResult GetShippers()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<ShipperDTO>>(Application.Select(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<ShipperDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/shipperapi/1
        [Route("api/shipperapi/{shipperId}")]
        public IHttpActionResult GetShipper(int shipperId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                ShipperDTO shipperDTO = Application.GetById(operationResult, new object[] { shipperId });   
                if (shipperDTO != null)
                {
                    return Ok(shipperDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/shipperapi
        public IHttpActionResult PostShipper(ShipperDTO shipperDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, shipperDTO))
                {
                    return CreatedAtRoute("DefaultApi", new { shipperDTO.ShipperId }, shipperDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/shipperapi/1
        [Route("api/shipperapi/{shipperId}")]
        public IHttpActionResult PutShipper(int shipperId, ShipperDTO shipperDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, shipperDTO))
                {
                    return Ok(shipperDTO);
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
