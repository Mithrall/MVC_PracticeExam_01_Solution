using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcWebApplication.Models;

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

            if(BlogPostRepository.blogPostList.Count() <= 0) {



                BlogPostRepository.blogPostList.Add(post);
                tempList = new List<BlogPost>(BlogPostRepository.blogPostList);
            }
            return View(tempList);
        }
        [HttpPost]
        public IActionResult Index(string Author) {
            tempList = BlogPostRepository.blogPostList.FindAll(x => x.Author == Author);
            
            //Session["Author"] = Author;

            return RedirectToAction("Index");
        }
    }
}
