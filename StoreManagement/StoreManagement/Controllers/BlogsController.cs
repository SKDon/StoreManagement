﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using StoreManagement.Data.Constants;
using StoreManagement.Data.Entities;
using StoreManagement.Data.GeneralHelper;
using StoreManagement.Data.RequestModel;


namespace StoreManagement.Controllers
{

    [OutputCache(CacheProfile = "Cache1Days")]
    public class BlogsController : ContentsController
    {
        private const String ContentType = StoreConstants.BlogsType;

        public BlogsController()
            : base(ContentType)
        {

        }

        //public ActionResult Index(int page = 1)
        //{
        //    var resultModel = new ContentsViewModel();
        //    resultModel.SStore = MyStore;
        //    var m = ContentService.GetContentsCategoryId(MyStore.Id, null, ContentType, true, page, 24);
        //    resultModel.SContents = new PagedList<Content>(m.items, m.page - 1, m.pageSize, m.totalItemCount);
        //    resultModel.SCategories = CategoryService.GetCategoriesByStoreId(MyStore.Id, ContentType, true);
        //    resultModel.Type = ContentType;
        //    resultModel.SNavigations = NavigationService.GetStoreActiveNavigations(this.MyStore.Id);
        //    resultModel.SSettings = this.GetStoreSettings();
        //    return View(resultModel);
        //}
        //public ActionResult Blog(String id)
        //{
        //    var resultModel = new ContentDetailViewModel();
        //    int blogId = id.Split("-".ToCharArray()).Last().ToInt();
        //    resultModel.SContent = ContentService.GetContentsContentId(blogId);
        //    resultModel.SStore = MyStore;
        //    resultModel.SCategory = CategoryService.GetCategory(resultModel.Content.CategoryId);
        //    resultModel.SCategories = CategoryService.GetCategoriesByStoreId(MyStore.Id, ContentType, true);
        //    resultModel.Type = ContentType;
        //    resultModel.SNavigations = NavigationService.GetStoreActiveNavigations(this.MyStore.Id);
        //    resultModel.SSettings = this.GetStoreSettings();
        //    return View(resultModel);
        //}
        
    }
}