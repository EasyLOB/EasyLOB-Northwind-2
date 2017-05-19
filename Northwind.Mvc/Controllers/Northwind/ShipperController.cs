﻿using Northwind.Application;
using Northwind.Data;
using Northwind.Data.Resources;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Library.Syncfusion;
using EasyLOB.Mvc;
using Newtonsoft.Json;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Northwind.Mvc
{
    public partial class ShipperController : BaseMvcControllerSCRUDApplication<ShipperDTO, Shipper>
    {
        #region Methods

        public ShipperController(INorthwindGenericApplicationDTO<ShipperDTO, Shipper> application)
        {
            Application = application;            
        }

        #endregion
        
        #region Methods SCRUD

        // GET: Shipper
        // GET: Shipper/Index
        [HttpGet]
        public ActionResult Index()
        {
            ShipperCollectionModel shipperCollectionModel = new ShipperCollectionModel(ActivityOperations, "Index", null);

            try
            {
                IsOperation(shipperCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                shipperCollectionModel.OperationResult.ParseException(exception);
            }

            return View(shipperCollectionModel);
        }        

        // GET & POST: Shipper/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            ShipperCollectionModel shipperCollectionModel = new ShipperCollectionModel(ActivityOperations, "Search", masterControllerAction);

            try
            {
                if (IsOperation(shipperCollectionModel.OperationResult))
                {
                    return PartialView(shipperCollectionModel);
                }
            }
            catch (Exception exception)
            {
                shipperCollectionModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(shipperCollectionModel.OperationResult);
        }

        // GET & POST: Shipper/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null, string query = null)
        {
            LookupModel lookupModel = new LookupModel(ActivityOperations, text, valueId, elements, query);

            try
            {
                if (IsSearch(lookupModel.OperationResult))
                {
                    return PartialView("_ShipperLookup", lookupModel);
                }
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return null;
        }

        // GET: Shipper/Create
        [HttpGet]
        public ActionResult Create()
        {
            ShipperItemModel shipperItemModel = new ShipperItemModel(ActivityOperations, "Create");

            try
            {
                if (IsCreate(shipperItemModel.OperationResult))
                {
                    return PartialView("CRUD", shipperItemModel);
                }            
            }
            catch (Exception exception)
            {
                shipperItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(shipperItemModel.OperationResult);
        }

        // POST: Shipper/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShipperItemModel shipperItemModel)
        {
            try
            {
                if (IsCreate(shipperItemModel.OperationResult))
                {
                    if (IsValid(shipperItemModel.OperationResult, shipperItemModel.Shipper))
                    {
                        ShipperDTO shipperDTO = (ShipperDTO)shipperItemModel.Shipper.ToDTO();
                        if (Application.Create(shipperItemModel.OperationResult, shipperDTO))
                        {
                            if (shipperItemModel.IsSave)
                            {
                                shipperItemModel.OperationResult.StatusMessage =
                                    EasyLOB.Resources.PresentationResources.CreateToUpdate;
                                return JsonResultSuccess(shipperItemModel.OperationResult,
                                    Url.Content(String.Format("~/Shipper/Update?ShipperId={0}", shipperDTO.ShipperId)));
                            }
                            else
                            {
                                return JsonResultSuccess(shipperItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                shipperItemModel.OperationResult.ParseException(exception);
            }

            shipperItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(shipperItemModel.OperationResult);
        }

        // GET: Shipper/Read/1
        [HttpGet]
        public ActionResult Read(int shipperId)
        {
            ShipperItemModel shipperItemModel = new ShipperItemModel(ActivityOperations, "Read");
            
            try
            {
                if (IsRead(shipperItemModel.OperationResult))
                {
                    ShipperDTO shipperDTO = Application.GetById(shipperItemModel.OperationResult, new object[] { shipperId });
                    if (shipperDTO != null)
                    {
                        shipperItemModel.Shipper = new ShipperViewModel(shipperDTO);                    

                        return PartialView("CRUD", shipperItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                shipperItemModel.OperationResult.ParseException(exception);
            }            

            return JsonResultOperationResult(shipperItemModel.OperationResult);
        }

        // GET: Shipper/Update/1
        [HttpGet]
        public ActionResult Update(int shipperId)
        {
            ShipperItemModel shipperItemModel = new ShipperItemModel(ActivityOperations, "Update");
            
            try
            {
                if (IsUpdate(shipperItemModel.OperationResult))
                {            
                    ShipperDTO shipperDTO = Application.GetById(shipperItemModel.OperationResult, new object[] { shipperId });
                    if (shipperDTO != null)
                    {
                        shipperItemModel.Shipper = new ShipperViewModel(shipperDTO);

                        return PartialView("CRUD", shipperItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                shipperItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(shipperItemModel.OperationResult);
        }

        // POST: Shipper/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ShipperItemModel shipperItemModel)
        {
            try
            {
                if (IsUpdate(shipperItemModel.OperationResult))
                {
                    if (IsValid(shipperItemModel.OperationResult, shipperItemModel.Shipper))
                    {
                        ShipperDTO shipperDTO = (ShipperDTO)shipperItemModel.Shipper.ToDTO();
                        if (Application.Update(shipperItemModel.OperationResult, shipperDTO))
                        {
                            if (shipperItemModel.IsSave)
                            {
                                return JsonResultSuccess(shipperItemModel.OperationResult,
                                    Url.Content(String.Format("~/Shipper/Update?ShipperId={0}", shipperDTO.ShipperId)));
                            }
                            else
                            {
                                return JsonResultSuccess(shipperItemModel.OperationResult);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                shipperItemModel.OperationResult.ParseException(exception);
            }

            shipperItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(shipperItemModel.OperationResult);
        }

        // GET: Shipper/Delete/1
        [HttpGet]
        public ActionResult Delete(int shipperId)
        {
            ShipperItemModel shipperItemModel = new ShipperItemModel(ActivityOperations, "Delete");
            
            try
            {
                if (IsDelete(shipperItemModel.OperationResult))
                {            
                    ShipperDTO shipperDTO = Application.GetById(shipperItemModel.OperationResult, new object[] { shipperId });
                    if (shipperDTO != null)
                    {
                        shipperItemModel.Shipper = new ShipperViewModel(shipperDTO);

                        return PartialView("CRUD", shipperItemModel);
                    }
                }
            }
            catch (Exception exception)
            {
                shipperItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(shipperItemModel.OperationResult);
        }

        // POST: Shipper/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ShipperItemModel shipperItemModel)
        {
            try
            {
                if (IsDelete(shipperItemModel.OperationResult))
                {
                    if (Application.Delete(shipperItemModel.OperationResult, (ShipperDTO)shipperItemModel.Shipper.ToDTO()))
                    {
                        return JsonResultSuccess(shipperItemModel.OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                shipperItemModel.OperationResult.ParseException(exception);
            }

            shipperItemModel.ActivityOperations = ActivityOperations;

            return JsonResultOperationResult(shipperItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Shipper/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<ShipperViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Shipper), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<ShipperViewModel, ShipperDTO, Shipper>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
                    if (dataManager.RequiresCounts)
                    {
                        countAll = Application.Count(where, args.ToArray());
                    }
                }
                catch (Exception exception)
                {
                    operationResult.ParseException(exception);
                }

                if (!operationResult.Ok)
                {
                    throw new InvalidOperationException(operationResult.Text);
                }
            }

            return Json(JsonConvert.SerializeObject(new { result = data, count = countAll }), JsonRequestBehavior.AllowGet);
        } 

        // POST: Shipper/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            if (IsExport())
            {
                ExportToExcel(gridModel, ShipperResources.EntitySingular + ".xlsx");
            }
        }

        // POST: Shipper/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            if (IsExport())
            {
                ExportToPdf(gridModel, ShipperResources.EntitySingular + ".pdf");
            }
        }

        // POST: Shipper/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            if (IsExport())
            {
                ExportToWord(gridModel, ShipperResources.EntitySingular + ".docx");
            }
        }
        
        #endregion Methods Syncfusion
    }
}