using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcWebApplication.Models;
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
                if(Author != "") {
                    tempList = BlogPostRepository.blogPostList.FindAll(x => x.Author == Author);
                } else {
                    tempList = BlogPostRepository.blogPostList;
                }
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
            if(Author == null) {
                Author = "";
            }
            tempList = BlogPostRepository.blogPostList.FindAll(x => x.Author == Author);

            HttpContext.Session.SetString("Author", Author);

            return RedirectToAction("Index");
        }

        public IActionResult About() {
            var Author = HttpContext.Session.GetString("Author");
            if(Author == "" || Author == null) {
                return Redirect("Index");
            } else {
                

                return View((object)Author);
            }
        }
        [HttpPost]
        public IActionResult About(string Title, string Content) {
            if(Title == "" || Title == null || Content == "" || Content == null) {
                return View();
            } else {

                var nextID = BlogPostRepository.blogPostList.Last<BlogPost>().ID++;

                var tempPost = new BlogPost {
                    ID = nextID,
                    Title = Title,
                    Content = Content,
                    CreateDate = DateTime.Now,
                    Author = HttpContext.Session.GetString("Author")
                };
                lock(BlogPostRepository.blogPostList) {
                    BlogPostRepository.blogPostList.Add(tempPost);
                }

                return Redirect("Index");
            }
        }
    }
}
