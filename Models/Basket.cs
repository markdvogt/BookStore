using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Basket
    {
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();

        public virtual void AddItem(Book book, int qty) //virtual allows the method to be overridden when we inherit from it
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

        public virtual void RemoveItem(Book book)
        {
            Items.RemoveAll(x => x.BookToBuy.BookId == book.BookId); //Look to see where the Books in our list match the book that were passed into the basket so you can remove them
        }

        public virtual void ClearBasket()
        {
            Items.Clear(); //This allows the user to clear the entire basket
        }

        public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * x.BookToBuy.Price); //++++++++This is where the price needs to go++++++++\\

            return sum;
        }
    }

    public class BasketLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Book BookToBuy { get; set; }
        public int Quantity { get; set; }
    }
}

