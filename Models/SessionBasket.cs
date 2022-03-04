using BookStore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class SessionBasket : Basket
    {
        public static Basket GetBasket(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ?? new SessionBasket();  //if a session has already been started, we’ll use that one. If not,we will create a new session for a basket 

            basket.Session = session;

            return basket;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Book book, int qty) //We’ll override all of the methods in the Basket.cs file so we can start                                                        
        {                                                //with a new basket in our session every time we load the website
            base.AddItem(book, qty);
            Session.SetJson("Basket", this); //”this” refers to this specific instance of the object
        }
        public override void RemoveItem(Book book)
        {
            base.RemoveItem(book);
            Session.SetJson("Basket", this);
        }
        public override void ClearBasket()
        {
            base.ClearBasket();
            Session.Remove("Basket");
        }

    }

}
