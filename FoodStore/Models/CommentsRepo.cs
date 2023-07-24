//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using FoodStore.Models.DTO;

//namespace FoodStore.Models
//{
//    public class CommentsRepo
//    {
//        FoodStoreEntities context = new FoodStoreEntities();

//        public IQueryable<Comments> GetAll(int? productid)
//        {

//            return context.Comments.OrderBy(x => x.CreateDate).Where(x => x.ProductId == productid);
//        }

//        public commentViewModel AddComment(commentDTO comment)
//        {
//            var _comment = new Comments()
//            {
//                ParentId = comment.parentId,
//                CommentText = comment.commentText,
//                UserName = comment.username,
//                CreateDate = DateTime.Now,
//                ProductId = comment.productId,
//            };

//            context.Comments.Add(_comment);
//            context.SaveChanges();
//            return context.Comments.Where(x => x.CommentId == _comment.CommentId)
//                    .Select(x => new commentViewModel
//                    {
//                        commentId = x.CommentId,
//                        commentText = x.CommentText,
//                        parentId = (int)x.ParentId,
//                        createdDate = (DateTime)x.CreateDate,
//                        username = x.UserName,
//                        productId = (int)x.ProductId

//                    }).FirstOrDefault();
//        }
//    }
//}