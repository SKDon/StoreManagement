﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericRepository.EntityFramework;
using StoreManagement.Data.Entities;
using StoreManagement.Service.IGeneralRepositories;

namespace StoreManagement.Service.Repositories.Interfaces
{
    public interface IFileManagerRepository : IBaseRepository<FileManager, int>, IFileManagerGeneralRepository, IDisposable 
    {
        List<FileManager> GetFilesByStoreIdAndLabels(int storeId, string[] labels);
        List<FileManager> GetFilesBySearchKey(int storeId, String search);
        List<FileManager> GetFilesByFileStatus(String fileStatus);
    }

}
