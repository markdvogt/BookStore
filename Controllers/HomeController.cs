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

        public IActionResult Index(string bookCategory, int pageNum = 1) //pass in start page number as 1
        {
            int pageSize = 10; //This will be the number of entries we see on each page

            var x = new EntriesViewModel
            {
                Entries = repo.Books
                  .Where(b => b.Category == bookCategory || bookCategory == null)
                  //Now when the user clicks on the filter link, it will only return a list of books that fall in that category.
                  //The || bookCategory == null will allow all of the books to be displayed if no category is specified

                  .Skip((pageNum - 1) * pageSize) //Now if you are on page 2 of the website, you want to see the second set of 10 records
                                                  //so it will skip (page2-1) * 10 records

                  .Take(pageSize), //now only 10 results will show on a page

                PageInfo = new PageInfo
                {
                    TotalNumEntries =
                        (bookCategory == null
                            ? repo.Books.Count() //if a category is not selected, pagination accounts for ALL records in the database
                            : repo.Books.Where(x => x.Category == bookCategory).Count()  //else the pagination is based off og the number of records in the specified category
                        ),
                    EntriesPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
                
            return View(x);
        }
    }
}
