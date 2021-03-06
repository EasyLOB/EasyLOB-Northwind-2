﻿using Northwind;
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
    public class CategoryAPIController : BaseApiControllerApplication<CategoryDTO, Category>
    {
        #region Methods

        public CategoryAPIController(INorthwindGenericApplicationDTO<CategoryDTO, Category> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/categoryapi/1
        [Route("api/categoryapi/{categoryId}")]
        public IHttpActionResult DeleteCategory(int categoryId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                CategoryDTO categoryDTO = Application.GetById(operationResult, new object[] { categoryId });    
                if (categoryDTO != null)
                {
                    if (Application.Delete(operationResult, categoryDTO))
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

        // GET: api/categoryapi
        public IHttpActionResult GetCategories()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<CategoryDTO>>(Application.Search(operationResult,
                    null, null, (null as int?), AppDefaults.SyncfusionRecordsBySearch));
                //return Ok<IEnumerable<CategoryDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new ZActionResultApi(Request, operationResult);
        }

        // GET: api/categoryapi/1
        [Route("api/categoryapi/{categoryId}")]
        public IHttpActionResult GetCategory(int categoryId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                CategoryDTO categoryDTO = Application.GetById(operationResult, new object[] { categoryId });   
                if (categoryDTO != null)
                {
                    return Ok(categoryDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new ZActionResultApi(Request, operationResult);
        }

        // POST: api/categoryapi
        public IHttpActionResult PostCategory(CategoryDTO categoryDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, categoryDTO))
                {
                    return CreatedAtRoute("DefaultApi", new { categoryDTO.CategoryId }, categoryDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new ZActionResultApi(Request, operationResult);
        }

        // PUT: api/categoryapi/1
        [Route("api/categoryapi/{categoryId}")]
        public IHttpActionResult PutCategory(int categoryId, CategoryDTO categoryDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (Application.Create(operationResult, categoryDTO))
                {
                    return Ok(categoryDTO);
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
