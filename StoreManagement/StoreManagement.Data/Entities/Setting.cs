﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using GenericRepository;
using Newtonsoft.Json;

namespace StoreManagement.Data.Entities
{

    public class Setting : BaseEntity
    {
        public string Name { get; set; }
        // [Required(ErrorMessage = "Please key name")]
        public string SettingKey { get; set; }
        // [Required(ErrorMessage = "Please value name")]
        public string SettingValue { get; set; }




         [JsonIgnore]
        public string Type { get; set; }
         [JsonIgnore]
        public string Description { get; set; }

        public override string ToString()
        {
            return String.Format("{2}-{0}-{1}", SettingKey, SettingValue, Id);
        }
    }
}
