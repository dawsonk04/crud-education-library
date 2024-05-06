using DRK.ProgDec.BL.Models;

namespace DRK.ProgDec.BL
{
    public static class ShoppingCartManager
    {

        public static void Add(ShoppingCart cart, Declaration declaration)
        {
            if (declaration != null) { cart.Items.Add(declaration); }
        }
        public static void Remove(ShoppingCart cart, Declaration declaration)
        {
            if (declaration != null) { cart.Items.Remove(declaration); }
        }

        // in dvd central we need to make an orderItem 
        public static void Checkout(ShoppingCart cart)
        {
            // ## Stuff for DVD Central (#7) - Instructions, Make sure to add try catch and what not
            // Make a new Order
            // Set the order Fields as need

            // forEach(Movie item in cart.Items)

            // Make new orderItem
            // Set the OrderItem fields for the item
            // order.OrderItems.Add(orderItem)

            // OrderManager.Insert(order)

            // Decrement the tblMovie.InStkQty appropiately
            // Prolly add a Checkpoint Test
            cart = new ShoppingCart();



        }

    }
}
