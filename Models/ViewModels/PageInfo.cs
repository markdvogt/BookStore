using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumEntries { get; set; } //number of entries in the database
        public int EntriesPerPage { get; set; } //number of entries you want on each page
        public int CurrentPage { get; set; } //current page you are on

        //Calculate the total pages you'll need on the site by dividing the total entries in the database by the number of entries per page
        //Cast the total number of entries divided by entries per page as a double (decimal) in case the it doesn't devide into an integer (ex: 37 entries / 5 per page = 7.4)
        //If it comes out to 7.4 like the example above, we don't need 7.4 pages, we need 8 pages! That's why you use the Math.Ceiling function
        //Now cast all of that into an int since the data type needs to be a public int
        public int TotalPages => (int) Math.Ceiling((double)TotalNumEntries / EntriesPerPage);
    }
}
