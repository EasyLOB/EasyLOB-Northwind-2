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
    public class ProductAPIController : BaseApiControllerApplication<ProductDTO, Product>
    {
        #region Methods

        public ProductAPIController(INorthwindGenericApplicationDTO<ProductDTO, Product> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/productapi/1
        [Route("api/productapi/{productId}")]
        public IHttpActionResult DeleteProduct(int productId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                ProductDTO productDTO = Application.GetById(operationResult, new object[] { productId });    
                if (productDTO != null)
                {
                    if (Application.Delete(operationResult, productDTO))
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

        // GET: api/productapi
        public IHttpActionResult GetProducts()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<ProductDTO>>(Application.Search(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<ProductDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new ZActionResultApi(Request, operationResult);
        }

        // GET: api/productapi/1
        [Route("api/productapi/{productId}")]
        public IHttpActionResult GetProduct(int productId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                ProductDTO productDTO = Application.GetById(operationResult, new object[] { productId });   
                if (productDTO != null)
                {
                    return Ok(productDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new ZActionResultApi(Request, operationResult);
        }

        // POST: api/productapi
        public IHttpActionResult PostProduct(ProductDTO productDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, productDTO))
                {
                    return CreatedAtRoute("DefaultApi", new { productDTO.ProductId }, productDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new ZActionResultApi(Request, operationResult);
        }

        // PUT: api/productapi/1
        [Route("api/productapi/{productId}")]
        public IHttpActionResult PutProduct(int productId, ProductDTO productDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, productDTO))
                {
                    return Ok(productDTO);
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
