﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Comment
    {
        [DisplayName("ID")]
        public int ID { get; set; }
        [DisplayName("Nội dung bình luận")]
        public string CommentMsg { get; set; }
        [DisplayName("Ngày bình luận")]
        public Nullable<System.DateTime> CommentDate { get; set; }
        [DisplayName("Mã sản phẩm")]
        public Nullable<int> ProductId { get; set; }

        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }
        [DisplayName("Parent")]
        public Nullable<int> ParentID { get; set; }
        [DisplayName("Đánh giá")]
        public Nullable<int> Rate { get; set; }
        [DisplayName("Mã khách hàng")]
        public Nullable<int> CustomerId { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
