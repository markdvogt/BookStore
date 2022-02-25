using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Basket
    {
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();

        public void AddItem(Book book, int qty)
        {
            BasketLineItem line = Items
                .Where(b => b.BookToBuy.BookId == book.BookId) //finds the book that matches the selected bookID that the user wants to buy
                .FirstOrDefault();

            if (line == null) //if the book hasn’t been selected yet, this creates a new line item in the basket
            {
                Items.Add(new BasketLineItem
                {
                    BookToBuy = book,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty; //If the book is already in the basket, this will keep a counter on the quantity of that book
            }
        }

        public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * x.BookToBuy.Price); //++++++++This is where the price needs to go++++++++\\

            return sum;
        }
    }

    public class BasketLineItem
    {
        public int LineID { get; set; }
        public Book BookToBuy { get; set; }
        public int Quantity { get; set; }
    }
}

