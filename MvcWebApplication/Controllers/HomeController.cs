using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcWebApplication.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;

namespace MvcWebApplication.Controllers {
    public class HomeController : Controller {

        List<BlogPost> tempList;
        BlogPost post = new BlogPost {
            ID = 0,
            Title = "Test",
            Content = "lalalal",
            CreateDate = DateTime.Now,
            Author = "Bob",
        };

        public IActionResult Index() {

            try {
                var Author = HttpContext.Session.GetString("Author");
                tempList = BlogPostRepository.blogPostList.FindAll(x => x.Author == Author);

            } catch(Exception) {
                throw;
            }

            if(BlogPostRepository.blogPostList.Count() <= 0) {

                BlogPostRepository.blogPostList.Add(post);
                tempList = new List<BlogPost>(BlogPostRepository.blogPostList);
            }
            return View(tempList);
        }
        [HttpPost]
        public IActionResult Index(string Author) {
            tempList = BlogPostRepository.blogPostList.FindAll(x => x.Author == Author);

            HttpContext.Session.SetString("Author", Author);

            return RedirectToAction("Index");
        }
    }
}
