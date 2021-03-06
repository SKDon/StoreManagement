﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NLog;
using StoreManagement.Data;
using StoreManagement.Data.Attributes;
using StoreManagement.Data.Constants;
using StoreManagement.Data.Entities;
using StoreManagement.Data.GeneralHelper;
using StoreManagement.Data.RequestModel;


namespace StoreManagement.Liquid.Controllers
{
    [OutputCache(CacheProfile = "Cache1Days")]
    [Compress]
    public class AjaxProductsController : AjaxController
    {

        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<JsonResult> GetProductCategories(String designName = "ProductCategoriesPartial", int imageWidth = 0, int imageHeight = 0)
        {

            if (String.IsNullOrEmpty(designName))
            {
                return Json("No Desing Name is defined.", JsonRequestBehavior.AllowGet);
            }
            String returnHtml = "";

            try
            {
                returnHtml = await GetProductCategoriesHtml(designName, imageWidth, imageHeight);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "GetProductCategories:" + ex.StackTrace, designName);

            }



            return Json(returnHtml, JsonRequestBehavior.AllowGet);
        }

        private async Task<String> GetProductCategoriesHtml(string designName, int imageWidth, int imageHeight)
        {
            string returnHtml;
            var categoriesTask = ProductCategoryService.GetProductCategoriesByStoreIdAsync(StoreId, StoreConstants.ProductType,
                                                                                           true);
            var pageDesignTask = PageDesignService.GetPageDesignByName(StoreId, designName);

            ProductCategoryHelper.StoreSettings = GetStoreSettings();
            ProductCategoryHelper.ImageWidth = imageWidth == 0
                                                   ? GetSettingValueInt("ProductCategoriesPartial_ImageWidth", 50)
                                                   : imageWidth;
            ProductCategoryHelper.ImageHeight = imageHeight == 0
                                                    ? GetSettingValueInt("ProductCategoriesPartial_ImageHeight", 50)
                                                    : imageHeight;

            await Task.WhenAll(categoriesTask, pageDesignTask);
            var categories = categoriesTask.Result;
            var pageDesign = pageDesignTask.Result;

            var pageOuput = ProductCategoryHelper.GetProductCategoriesPartial(categories, pageDesign);
            returnHtml = pageOuput.PageOutputText;

            return returnHtml;
        }


        public async Task<JsonResult> GetBrands(String designName = "BrandsPartial", int take = 0,
            int imageWidth = 0,
            int imageHeight = 0)
        {

            if (String.IsNullOrEmpty(designName))
            {
                return Json("No Desing Name is defined.", JsonRequestBehavior.AllowGet);
            }

            String returnHtml = "";

            try
            {
                returnHtml = await GetBrandsHtml(designName, take, imageWidth, imageHeight);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "GetBrands:" + ex.StackTrace, designName);

            }


            return Json(returnHtml, JsonRequestBehavior.AllowGet);
        }

        private async Task<String> GetBrandsHtml(string designName, int take, int imageWidth, int imageHeight)
        {
            string returnHtml;
            take = take == 0 ? GetSettingValueInt("BrandsPartial_ItemsNumber", 50) : take;
            var pageDesignTask = PageDesignService.GetPageDesignByName(StoreId, designName);
            var brandsTask = BrandService.GetBrandsAsync(StoreId, take, true);

            BrandHelper.StoreSettings = GetStoreSettings();
            BrandHelper.ImageWidth = imageWidth == 0 ? GetSettingValueInt("BrandsPartial_ImageWidth", 50) : imageWidth;
            BrandHelper.ImageHeight = imageHeight == 0 ? GetSettingValueInt("BrandsPartial_ImageHeight", 50) : imageHeight;

            await Task.WhenAll(pageDesignTask, brandsTask);
            var pageDesign = pageDesignTask.Result;
            var brands = brandsTask.Result;
            if (pageDesign == null)
            {
                throw new Exception("PageDesing is null");
            }


            var pageOuput = BrandHelper.GetBrandsPartial(brands, pageDesign);
            returnHtml = pageOuput.PageOutputText;

            return returnHtml;
        }

        public async Task<JsonResult> GetProductLabels(int id, String designName = "ProductLabelsPartial", int imageWidth = 0, int imageHeight = 0)
        {

            if (String.IsNullOrEmpty(designName))
            {
                return Json("No Desing Name is defined.", JsonRequestBehavior.AllowGet);
            }
            String returnHtml = "";
            String key = String.Format("GetProductLabels-{0}-{1}-{2}-{3}", id, designName, imageWidth, imageHeight);
            try
            {
                returnHtml = await GetProductLabelsHtml(id, designName, imageWidth, imageHeight);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "GetProductLabels:" + ex.StackTrace, designName);
            }


            return Json(returnHtml, JsonRequestBehavior.AllowGet);
        }

        private async Task<String> GetProductLabelsHtml(int id, string designName, int imageWidth, int imageHeight)
        {
            string returnHtml;
            var pageDesignTask = PageDesignService.GetPageDesignByName(StoreId, designName);
            var labelsTask = LabelService.GetLabelsByItemTypeId(StoreId, id, StoreConstants.ProductType);

            LabelHelper.StoreSettings = GetStoreSettings();
            LabelHelper.ImageWidth = imageWidth == 0 ? GetSettingValueInt("ProductLabels_ImageWidth", 50) : imageWidth;
            LabelHelper.ImageHeight = imageHeight == 0 ? GetSettingValueInt("ProductLabels_ImageHeight", 50) : imageHeight;


            await Task.WhenAll(pageDesignTask, labelsTask);
            var labels = labelsTask.Result;
            var pageDesign = pageDesignTask.Result;

            if (pageDesign == null)
            {
                throw new Exception("PageDesing is null");
            }


            var pageOuput = LabelHelper.GetProductLabels(labels, pageDesign);
            returnHtml = pageOuput.PageOutputText;

            return returnHtml;
        }

        public async Task<JsonResult> GetProductsByProductType(int page = 1, String designName = "", int categoryId = 0, int brandId = 0, int retailerId = 0,
            int pageSize = 0, int imageWidth = 0, int imageHeight = 0, String productType = "popular", int excludedProductId = 0)
        {


            if (String.IsNullOrEmpty(designName))
            {
                return Json("No Desing Name is defined.", JsonRequestBehavior.AllowGet);
            }
            String returnHtml = "";
            String key = String.Format("GetProductsByProductTypeAsync-{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}",
                StoreId, page, designName, categoryId, brandId, pageSize, imageHeight, imageWidth, productType, excludedProductId);
            try
            {

                returnHtml = await GetProductsByProductTypeHtml(page, designName, categoryId, brandId, retailerId, pageSize, imageWidth, imageHeight, productType, excludedProductId);

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "GetProductsByProductTypeAsync:" + ex.StackTrace, page, designName, categoryId, brandId, pageSize, imageWidth, imageHeight, productType, excludedProductId);

            }


            return Json(returnHtml, JsonRequestBehavior.AllowGet);
        }

        private async Task<String> GetProductsByProductTypeHtml(int page, string designName, int categoryId, int brandId, int retailerId, int pageSize,
                                                        int imageWidth, int imageHeight, string productType, int excludedProductId)
        {

            string returnHtml = "";


            Task<List<Product>> productsTask = null;
            var catId = categoryId == 0 ? (int?)null : categoryId;
            var retId = retailerId == 0 ? (int?)null : retailerId;
            var bId = brandId == 0 ? (int?)null : brandId;
            var eProductId = excludedProductId == 0 ? (int?)null : excludedProductId;
            Logger.Trace("StoreId " + StoreId +
                         " designName:" +
                         designName +
                         " categoryId:" +
                         categoryId + " brandId:" +
                         brandId + " pageSize:" + pageSize + " page:" +
                         page + " imageWidth:" + imageWidth + " imageHeight:" + imageHeight + " productType:" + productType);

            if (productType.Equals("random"))
            {
                pageSize = pageSize == 0
                               ? GetSettingValueInt("RandomProducts_PageSize", StoreConstants.DefaultPageSize)
                               : pageSize;
                ProductHelper.ImageWidth = imageWidth == 0 ? GetSettingValueInt("RandomProducts_ImageWidth", 99) : imageWidth;
                ProductHelper.ImageHeight = imageHeight == 0
                                                ? GetSettingValueInt("RandomProducts_ImageHeight", 99)
                                                : imageHeight;
            }
            else if (productType.Equals("normal"))
            {
                pageSize = pageSize == 0
                               ? GetSettingValueInt("NormalProducts_PageSize", StoreConstants.DefaultPageSize)
                               : pageSize;
                ProductHelper.ImageWidth = imageWidth == 0 ? GetSettingValueInt("NormalProducts_ImageWidth", 99) : imageWidth;
                ProductHelper.ImageHeight = imageHeight == 0
                                                ? GetSettingValueInt("NormalProducts_ImageHeight", 99)
                                                : imageHeight;
            }
            else if (productType.Equals("popular"))
            {
                pageSize = pageSize == 0
                               ? GetSettingValueInt("PopularProducts_PageSize", StoreConstants.DefaultPageSize)
                               : pageSize;
                ProductHelper.ImageWidth = imageWidth == 0 ? GetSettingValueInt("PopularProducts_ImageWidth", 99) : imageWidth;
                ProductHelper.ImageHeight = imageHeight == 0
                                                ? GetSettingValueInt("PopularProducts_ImageHeight", 99)
                                                : imageHeight;
            }
            else if (productType.Equals("recent"))
            {
                pageSize = pageSize == 0
                               ? GetSettingValueInt("RecentProducts_PageSize", StoreConstants.DefaultPageSize)
                               : pageSize;
                ProductHelper.ImageWidth = imageWidth == 0 ? GetSettingValueInt("RecentProducts_ImageWidth", 99) : imageWidth;
                ProductHelper.ImageHeight = imageHeight == 0
                                                ? GetSettingValueInt("RecentProducts_ImageHeight", 99)
                                                : imageHeight;
            }
            else if (productType.Equals("main"))
            {
                pageSize = pageSize == 0
                               ? GetSettingValueInt("MainProducts_PageSize", StoreConstants.DefaultPageSize)
                               : pageSize;
                ProductHelper.ImageWidth = imageWidth == 0 ? GetSettingValueInt("MainProducts_ImageWidth", 99) : imageWidth;
                ProductHelper.ImageHeight = imageHeight == 0 ? GetSettingValueInt("MainProducts_ImageHeight", 99) : imageHeight;
            }
            else if (productType.Equals("discount"))
            {
                pageSize = pageSize == 0
                               ? GetSettingValueInt("DiscountProducts_PageSize", StoreConstants.DefaultPageSize)
                               : pageSize;
                ProductHelper.ImageWidth = imageWidth == 0 ? GetSettingValueInt("DiscountProducts_ImageWidth", 99) : imageWidth;
                ProductHelper.ImageHeight = imageHeight == 0
                                                ? GetSettingValueInt("DiscountProducts_ImageHeight", 99)
                                                : imageHeight;
            }

            productsTask = ProductService.GetProductsByProductTypeAsync(StoreId, catId, bId, retId, StoreConstants.ProductType, page,
                                                                 pageSize, true, productType, eProductId);
            var pageDesignTask = PageDesignService.GetPageDesignByName(StoreId, designName);
            var productCategoriesTask = ProductCategoryService.GetProductCategoriesByStoreIdAsync(StoreId,
                                                                                                  StoreConstants
                                                                                                      .ProductType, true);

            await Task.WhenAll(pageDesignTask, productsTask, productCategoriesTask);
            var products = productsTask.Result;
            var pageDesign = pageDesignTask.Result;
            var productCategories = productCategoriesTask.Result;

            ProductHelper.StoreSettings = GetStoreSettings();


            var pageOuput = ProductHelper.GetPopularProducts(products, productCategories, pageDesign);
            returnHtml = pageOuput.PageOutputText;

            return returnHtml;
        }
    }
}