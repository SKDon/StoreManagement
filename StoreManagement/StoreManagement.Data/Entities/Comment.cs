﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Data.Entities
{
          
    public class Comment : BaseEntity
    {
        public int ParentId { set; get; }
        public int ItemId { set; get; }
        public String ItemType { set; get; }
       // [Required(ErrorMessage = "Please enter name")]
       // public int Name { set; get; }
        [Required(ErrorMessage = "Please enter email")]
        public String Email { set; get; }
        public String CommentType { set; get; }
        [Required(ErrorMessage = "Please enter comment")]
        public String CommentText { set; get; }
    }
}
