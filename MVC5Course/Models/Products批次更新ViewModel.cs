using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models {
    public class Products批次更新ViewModel {
        public int ProductId { get; set; }
        [Required]
        [Range(1,500,ErrorMessage="請輸入1~500")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "請輸入1~100")]
        public Nullable<decimal> Stock { get; set; }

    }
}