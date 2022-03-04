using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class EFPurchaseRepository : IPurchaseRepository
    {
        private BookstoreContext context;

        public EFPurchaseRepository(BookstoreContext temp)
        {
            context = temp;
        }

        //This grabs the details of the purchase and then gets the details of the specific books that were purchased
        public IQueryable<Purchase> Purchase => context.Purchases.Include(x => x.Lines).ThenInclude(x => x.BookToBuy);

        public void SavePurchase(Purchase purchase)
        {
            context.AttachRange(purchase.Lines.Select(x => x.BookToBuy));
            if(purchase.PurchaseId == 0)
            {
                context.Purchases.Add(purchase);
            }

            context.SaveChanges();

        }
    }
}
