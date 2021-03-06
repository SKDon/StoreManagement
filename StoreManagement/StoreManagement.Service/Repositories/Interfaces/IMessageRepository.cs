﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Data.Entities;
using StoreManagement.Service.IGeneralRepositories;

namespace StoreManagement.Service.Repositories.Interfaces
{

    public interface IMessageRepository : IBaseRepository<Message, int>, IMessageGeneralRepository, IDisposable
    {

        List<Message> GetMessagesByStoreId(int storeId, string search);
    }
}
