using AngleSharp.Dom;
using PlanItJupiterTestAutomation.Models;
using PlanItJupiterTestAutomation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanItJupiterTestAutomation.Tests
{
    public class ShoppingTest : Base
    {
        public ShoppingTest() { }

        [Test]
        public void TestShoppingOrder()
        {
            HomePage homePage = new HomePage(getDriver());
            homePage.getShopMenu().Click();
            ShopPage shopPage = new ShopPage(getDriver());
            CartPage cartPage = new CartPage(getDriver());

            var itemListInCart = cartPage.getCartItems();
            var itemListInShopPage = shopPage.getShoppingItemList();


            shopPage.buyItem("Stuff Frog", 2);
            shopPage.buyItem("Fluffy Bunny", 5);
            shopPage.buyItem("Valentine Bear", 3);

            shopPage.navigateIntoCort();

            for (int i = 0; i < itemListInCart.ToList().Count; i++ )
            {
                Assert.That(itemListInCart.ToList<CartItem>()[i].Subtotal, Is.EqualTo(itemListInShopPage.ToList<ShopItem>()[i].ItemTotal));
                Assert.That(itemListInCart.ToList<CartItem>()[i].Price, Is.EqualTo(itemListInShopPage.ToList<ShopItem>()[i].Price));
                
            }

            TestContext.Progress.WriteLine("Cart total", cartPage.getCartTotal());
            TestContext.Progress.WriteLine("ShopTotal", shopPage.getShopSubtotal());

            Assert.That(cartPage.getCartTotal(), Is.EqualTo(shopPage.getShopSubtotal()));
            

        }

    }
}
