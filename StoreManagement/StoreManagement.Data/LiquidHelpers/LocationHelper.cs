﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using StoreManagement.Data.Constants;
using StoreManagement.Data.Entities;
using StoreManagement.Data.LiquidEngineHelpers;
using StoreManagement.Data.LiquidEntities;
using StoreManagement.Data.LiquidHelpers.Interfaces;

namespace StoreManagement.Data.LiquidHelpers
{
    public class LocationHelper : BaseLiquidHelper, ILocationHelper
    {


        public StoreLiquidResult GetLocationIndexPage(PageDesign pageDesign, List<Location> locations)
        {
            var result = new StoreLiquidResult();
            var dic = new Dictionary<String, String>();
            result.LiquidRenderedResult = dic;
            result.PageDesingName = pageDesign.Name;
            dic.Add(StoreConstants.PageOutput, "");

            try
            {


              
                if (pageDesign == null)
                {
                    throw new Exception("PageDesing is null");
                }


                var items = new List<LocationLiquid>();
                foreach (var item in locations)
                {

                    var i = new LocationLiquid(item, ImageWidth, ImageHeight);
                    items.Add(i);

                }


                object anonymousObject = new
                {
                    locations = LiquidAnonymousObject.GetLocationsEnumerable(items)


                };


                var indexPageOutput = LiquidEngineHelper.RenderPage(pageDesign, anonymousObject);


                dic[StoreConstants.PageOutput] = indexPageOutput;


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return result;
        }

    }
}