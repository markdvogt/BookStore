using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class EntriesViewModel
    {
        public IQueryable<Book> Entries { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
