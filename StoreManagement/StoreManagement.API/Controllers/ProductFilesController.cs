﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StoreManagement.Data.Constants;
using StoreManagement.Data.Entities;
using StoreManagement.Service.IGeneralRepositories;
using WebApi.OutputCache.V2;

namespace StoreManagement.API.Controllers
{
    [CacheOutput(ClientTimeSpan = StoreConstants.CacheClientTimeSpanSeconds, ServerTimeSpan = StoreConstants.CacheServerTimeSpanSeconds)]
    public class ProductFilesController : BaseApiController<ProductFile>, IProductFileGeneralRepository
    {


        public override IEnumerable<ProductFile> GetAll()
        {
            throw new NotImplementedException();
        }

        public override ProductFile Get(int id)
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage Post(ProductFile value)
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage Put(int id, ProductFile value)
        {
            throw new NotImplementedException();
        }

        public override HttpResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductFile> GetProductFilesByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public List<ProductFile> GetProductFilesByFileManagerId(int fileManagerId)
        {
            throw new NotImplementedException();
        }

       
    }
}
