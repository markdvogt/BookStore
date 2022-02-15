using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public interface IBookstoreRepository //An interface is not a class, but a TEMPLATE for a class
    {
        IQueryable<Book> Books { get; } //NO SETTER, JUST A GET
    }
}
