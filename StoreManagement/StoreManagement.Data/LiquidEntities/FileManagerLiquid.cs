﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using StoreManagement.Data.Entities;
using StoreManagement.Data.GeneralHelper;

namespace StoreManagement.Data.LiquidEntities
{
    public class FileManagerLiquid : BaseDrop
    {

        public FileManager FileManager;
        public String Link { get; set; }

        public FileManagerLiquid(FileManager fileManager)
        {
            this.FileManager = fileManager;

        }

        public FileManagerLiquid(FileManager fileManager, int imageWidth, int imageHeight)
        {
            this.FileManager = fileManager;
            this.ImageWidth = imageWidth;
            this.ImageHeight = imageHeight;

        }
        public String ImageSource
        {
            get
            {
                return LinkHelper.GetImageLink("Thumbnail", this.FileManager, this.ImageWidth, this.ImageHeight);
            }
        }

        public int Id
        {
            get { return FileManager.Id; }
        }
        public String Name
        {
            get { return FileManager.Name; }
        }

        public DateTime CreatedDate
        {
            get { return FileManager.CreatedDate.Value; }
        }
        public DateTime UpdatedDate
        {
            get { return FileManager.UpdatedDate.Value; }
        }
        public bool State
        {
            get { return FileManager.State; }
        }

    }
}
