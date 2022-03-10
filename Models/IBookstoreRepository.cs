using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public interface IBookstoreRepository //An interface is not a class, but a TEMPLATE for a class
    {
        IQueryable<Book> Books { get; } //NO SETTER, JUST A GET

        public void SaveBook(Book b); //admin can edit a book
        public void CreateBook(Book b); //admin can create or add a book
        public void Deletebook(Book b); //admin can delete a book

    }
}
