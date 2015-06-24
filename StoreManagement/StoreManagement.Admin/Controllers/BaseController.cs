﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using NLog;
using Ninject;
using StoreManagement.Data;
using StoreManagement.Data.CacheHelper;
using StoreManagement.Data.EmailHelper;
using StoreManagement.Data.Entities;
using StoreManagement.Data.GeneralHelper;
using StoreManagement.Service.DbContext;
using StoreManagement.Service.Repositories.Interfaces;
using WebMatrix.WebData;

namespace StoreManagement.Admin.Controllers
{
    public abstract class BaseController : Controller
    {

        static readonly TypedObjectCache<Store> UserStoreCache = new TypedObjectCache<Store>("UserStoreCache");



        [Inject]
        public IFileManagerRepository FileManagerRepository { get; set; }

        [Inject]
        public IContentFileRepository ContentFileRepository { set; get; }

        [Inject]
        public IContentRepository ContentRepository { set; get; }

        [Inject]
        public ICategoryRepository CategoryRepository { set; get; }

        [Inject]
        public IStoreRepository StoreRepository { set; get; }

        [Inject]
        public INavigationRepository NavigationRepository { set; get; }

        [Inject]
        public IPageDesignRepository PageDesignRepository { set; get; }

        [Inject]
        public IStoreUserRepository StoreUserRepository { set; get; }

        [Inject]
        public ISettingRepository SettingRepository { set; get; }

        [Inject]
        public IStoreContext DbContext { set; get; }

        [Inject]
        public IEmailSender EmailSender { set; get; }

        [Inject]
        public IProductRepository ProductRepository { set; get; }

        [Inject]
        public IProductFileRepository ProductFileRepository { set; get; }

        [Inject]
        public IProductCategoryRepository ProductCategoryRepository { set; get; }

        [Inject]
        public ILabelRepository LabelRepository { set; get; }

        [Inject]
        public ILabelLineRepository LabelLineRepository { set; get; }

        [Inject]
        public IEmailListRepository EmailListRepository { set; get; }

        private Boolean MySuperAdmin
        {
            get
            {
                var formsIdentity = HttpContext.User.Identity as FormsIdentity;
                if (formsIdentity != null)
                {
                    FormsAuthenticationTicket ticket = formsIdentity.Ticket;
                    string userData = ticket.UserData;

                    return userData.Equals("true",StringComparison.InvariantCultureIgnoreCase);
                }
                else
                {
                    return false;
                }
            }
             
        }
        protected Boolean IsSuperAdmin
        {
            get { return MySuperAdmin; }
        }

        protected int GetStoreId(int id)
        {
            if (IsSuperAdmin)
            {
                return id;
            }
            else
            {
                if (LoginStore.Id > 0)
                {
                    return LoginStore.Id;
                }
                else
                {
                    return -1;
                }
            }
        }
        private Store _store = new Store();
        private Store MyLoginStore
        {
            get
            {
                 
                var formsIdentity = HttpContext.User.Identity as FormsIdentity;
                if (formsIdentity != null)
                {
                    FormsAuthenticationTicket ticket = formsIdentity.Ticket;
                    string userData = ticket.UserData;
                    var serializer = new JavaScriptSerializer();
                    var s = serializer.Deserialize<Store>(userData);

                    return s;
                }
                else
                {
                    return null;
                }
            }
            
        }
        protected Store LoginStore
        {
            get { return MyLoginStore; }
        }
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected BaseController()
        {

        }

      
 
        protected string GetCleanHtml(String source)
        {
            if (String.IsNullOrEmpty(source))
                return String.Empty;

            source = HttpUtility.HtmlDecode(source);

            string path = HttpContext.Server.MapPath("~/tags.config");
            var myFile = new System.IO.StreamReader(path);
            string myTags = myFile.ReadToEnd();
            var returnHtml = HtmlCleanHelper.SanitizeHtmlSoft(myTags, source);
            returnHtml = GeneralHelper.NofollowExternalLinks(returnHtml);
            return returnHtml;
        }
        protected bool CheckRequest(BaseEntity entity)
        {
            if (IsSuperAdmin)
            {
                return true;
            }
            else
            {
                return entity.StoreId == LoginStore.Id;
            }
        }

    }
}
