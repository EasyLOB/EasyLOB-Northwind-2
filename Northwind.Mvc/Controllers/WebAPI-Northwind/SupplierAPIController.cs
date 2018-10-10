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
    public class SupplierAPIController : BaseApiControllerApplication<SupplierDTO, Supplier>
    {
        #region Methods

        public SupplierAPIController(INorthwindGenericApplicationDTO<SupplierDTO, Supplier> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/supplierapi/1
        [Route("api/supplierapi/{supplierId}")]
        public IHttpActionResult DeleteSupplier(int supplierId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                SupplierDTO supplierDTO = Application.GetById(operationResult, new object[] { supplierId });    
                if (supplierDTO != null)
                {
                    if (Application.Delete(operationResult, supplierDTO))
                    {
                        return Ok();
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new ZActionResultApi(Request, operationResult);
        }

        // GET: api/supplierapi
        public IHttpActionResult GetSuppliers()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<SupplierDTO>>(Application.Search(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<SupplierDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new ZActionResultApi(Request, operationResult);
        }

        // GET: api/supplierapi/1
        [Route("api/supplierapi/{supplierId}")]
        public IHttpActionResult GetSupplier(int supplierId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                SupplierDTO supplierDTO = Application.GetById(operationResult, new object[] { supplierId });   
                if (supplierDTO != null)
                {
                    return Ok(supplierDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new ZActionResultApi(Request, operationResult);
        }

        // POST: api/supplierapi
        public IHttpActionResult PostSupplier(SupplierDTO supplierDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, supplierDTO))
                {
                    return CreatedAtRoute("DefaultApi", new { supplierDTO.SupplierId }, supplierDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new ZActionResultApi(Request, operationResult);
        }

        // PUT: api/supplierapi/1
        [Route("api/supplierapi/{supplierId}")]
        public IHttpActionResult PutSupplier(int supplierId, SupplierDTO supplierDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, supplierDTO))
                {
                    return Ok(supplierDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new ZActionResultApi(Request, operationResult);
        }

        #endregion Methods REST
    }
}
