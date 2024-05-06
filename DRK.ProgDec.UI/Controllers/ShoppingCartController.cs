﻿using DRK.ProgDec.UI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DRK.ProgDec.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCart cart;
        // GET: ShoppingCartController
        public ActionResult Index()
        {
            ViewBag.Title = "Shopping Cart";
            cart = GetShoppingCart();
            return View(cart);
        }

        private ShoppingCart GetShoppingCart()
        {
            if (HttpContext.Session.GetObject<ShoppingCart>("cart") != null)
            {
                return HttpContext.Session.GetObject<ShoppingCart>("cart");
            }
            else
            {
                return new ShoppingCart();
            }
        }

        public IActionResult Remove(int id)
        {
            cart = GetShoppingCart();

            Declaration declaration = cart.Items.FirstOrDefault(i => i.ID == id);

            ShoppingCartManager.Remove(cart, declaration);

            HttpContext.Session.SetObject("cart", cart);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add(int id)
        {
            cart = GetShoppingCart();
            Declaration declaration = DeclarationManager.LoadById(id);
            ShoppingCartManager.Add(cart, declaration);

            HttpContext.Session.SetObject("cart", cart);

            return RedirectToAction(nameof(Index), "Declaration");
        }

        public IActionResult Checkout()
        {
            cart = GetShoppingCart();
            ShoppingCartManager.Checkout(cart);
            HttpContext.Session.SetObject("cart", null);
            return View();

        }

    }
}
