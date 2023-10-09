using OpenQA.Selenium;
using PlanItJupiterTestAutomation.Models;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlanItJupiterTestAutomation.Pages
{
    public class CartPage
    {
        IWebDriver driver;
        List<CartItem> cartItems;

        public CartPage(IWebDriver driver) 
        { 
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }


        //tr[ng-repeat='item in cart.items()']
        private By cartRowElements = By.CssSelector(".cart-item");
        private By cartNameElement = By.CssSelector("td[class='ng-binding']:nth-child(1)");
        private By cartPriceElement = By.CssSelector("td[class='ng-binding']:nth-child(2)");
        private By cartQuantityElement = By.CssSelector("input[name='quantity']");
        private By cartItemSubTotalElement = By.CssSelector("td[class='ng-binding']:nth-child(4)");
        private By cartTotalElement = By.CssSelector("strong[class='total ng-binding']");

        public decimal getCartTotal()
        {
            decimal total = 0;
            string totalVal = driver.FindElement(cartTotalElement).Text;
            total = decimal.Parse(Regex.Match(totalVal, @"\d+\.\d+").Value);
            //total = decimal.Parse(driver.FindElement(cartTotalElement).Text.Substring(6).Trim());
            TestContext.Progress.WriteLine("----");
            TestContext.Progress.WriteLine(total);
            return total;
        }


        public IEnumerable<CartItem> getCartItems()
        {
            var cartTableRows = driver.FindElements(cartRowElements);
            cartItems = new List<CartItem>();

            foreach (var item in cartTableRows)
            {
                var itemName = item.FindElement(cartNameElement).Text;
                var itemPrice = decimal.Parse(item.FindElement(cartPriceElement).Text.Substring(1));
                var itemQuantity = int.Parse(item.FindElement(cartQuantityElement).GetAttribute("value"));
                var itemSubTotal = decimal.Parse(item.FindElement(cartItemSubTotalElement).Text.Substring(1));

                var itemData = new CartItem
                {
                    ItemName = itemName,
                    Price = itemPrice,
                    Quantity = itemQuantity,
                    Subtotal = itemSubTotal
                };

                cartItems.Add(itemData);

            }

            return cartItems;


        }







    }
}
