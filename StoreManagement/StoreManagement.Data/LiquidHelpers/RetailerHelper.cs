﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreManagement.Data.Constants;
using StoreManagement.Data.Entities;
using StoreManagement.Data.LiquidEngineHelpers;
using StoreManagement.Data.LiquidEntities;
using StoreManagement.Data.LiquidHelpers.Interfaces;

namespace StoreManagement.Data.LiquidHelpers
{
    public class RetailerHelper : BaseLiquidHelper, IRetailerHelper
    {
        public StoreLiquidResult GetRetailers(List<Retailer> labels,PageDesign pageDesign)
        {


            var items = new List<RetailerLiquid>();
            foreach (var item in labels)
            {

                var nav = new RetailerLiquid(item);
                items.Add(nav);
            }


            object anonymousObject = new
            {
                retailers = LiquidAnonymousObject.GetRetailersEnumerable(items)
            };


            var indexPageOutput = LiquidEngineHelper.RenderPage(pageDesign, anonymousObject);


            var dic = new Dictionary<String, String>();

            dic.Add(StoreConstants.PageOutput, indexPageOutput);


            var result = new StoreLiquidResult();
            result.PageDesingName = pageDesign.Name;
            result.LiquidRenderedResult = dic;
            return result;
        }


        public StoreLiquidResult GetRetailerDetailPage(Retailer retailer, List<Product> products, PageDesign pageDesign, List<ProductCategory> productCategories)
        {
            var result = new StoreLiquidResult();
            var dic = new Dictionary<String, String>();
            dic.Add(StoreConstants.PageOutput, "");

            try
            {

                var retailerLiquid = new RetailerLiquid(retailer, ImageWidth, ImageHeight);
                retailerLiquid.Products = products;
                retailerLiquid.ProductCategories = productCategories;

                object anonymousObject = new
                {
                    retailer =  LiquidAnonymousObject.GetRetailer(retailerLiquid),
                    products = LiquidAnonymousObject.GetProductsLiquid(retailerLiquid.ProductLiquidList),
                    productCategories = LiquidAnonymousObject.GetProductCategories(retailerLiquid.ProductCategoriesLiquids)

                };
                var indexPageOutput = LiquidEngineHelper.RenderPage(pageDesign, anonymousObject);


                dic[StoreConstants.PageOutput] = indexPageOutput;
                result.PageDesingName = pageDesign.Name;
                result.DetailLink = retailerLiquid.DetailLink;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);

            }



            result.LiquidRenderedResult = dic;
            result.PageDesingName = pageDesign.Name;

            return result;
        }
    }
}