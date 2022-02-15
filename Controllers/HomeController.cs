using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController(IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(int pageNum = 1) //pass in start page number as 1
        {
            int pageSize = 10; //This will be the number of entries we see on each page

            var x = new EntriesViewModel
            {
                Entries = repo.Books
                  //.OrderBy(b => b.Title) //Order by Title
                  .Skip((pageNum - 1) * pageSize) //Now if you are on page 2 of the website, you want to see the second set of 10 records
                                                //so it will skip (page2-1) * 10 records
                  .Take(pageSize), //now only 10 results will show on a page

                PageInfo = new PageInfo
                {
                    TotalNumEntries = repo.Books.Count(),
                    EntriesPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }
    }
}
