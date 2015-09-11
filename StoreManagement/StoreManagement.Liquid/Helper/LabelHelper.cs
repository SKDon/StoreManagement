﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using StoreManagement.Data.Constants;
using StoreManagement.Data.Entities;
using StoreManagement.Data.LiquidEngineHelpers;
using StoreManagement.Data.LiquidEntities;
using StoreManagement.Liquid.Helper.Interfaces;

namespace StoreManagement.Liquid.Helper
{
   

    public class LabelHelper : BaseLiquidHelper, ILabelHelper
    {


        public StoreLiquidResult GetProductLabels(
           Task<List<Label>> labelsTask,
           Task<PageDesign> pageDesignTask)
        {
            Task.WaitAll(pageDesignTask, labelsTask);
            var labels = labelsTask.Result;
            var pageDesign = pageDesignTask.Result;

            if (pageDesign == null)
            {
                throw new Exception("PageDesing is null");
            }


            var items = new List<LabelLiquid>();
            foreach (var item in labels)
            {

                var nav = new LabelLiquid(item, pageDesign);
                items.Add(nav);
            }


            object anonymousObject = new
            {
                labels = LiquidAnonymousObject.GetLabelsEnumerable(items)


            };


            var indexPageOutput = LiquidEngineHelper.RenderPage(pageDesign.PageTemplate, anonymousObject);


            var dic = new Dictionary<String, String>();

            dic.Add(StoreConstants.PageOutput, indexPageOutput);


            var result = new StoreLiquidResult();
            result.LiquidRenderedResult = dic;
            return result;
        }

       
    }
}