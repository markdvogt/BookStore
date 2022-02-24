using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Components
{
    public class BookCategoryViewComponent : ViewComponent
    {
        private IBookstoreRepository repo { get; set; }

        public BookCategoryViewComponent (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke() //When this ViewComponent is invoked, it will return a view with all of the different book categories
        {
            ViewBag.SelectedCategory = RouteData?.Values["bookCategory"]; // ? means it's okay to be null in the case the user hasnt selected a category yet

            var BookCategories = repo.Books
                            .Select(x => x.Category) //Load up the variable "BookCategories" with all the book categories from the model
                            .Distinct() //Only load unique categories so there aren't duplicates
                            .OrderBy(x => x); //Order by x.Category which is now just x

            return View(BookCategories); //Now return this list of book categories to the view
        }
    }
}
