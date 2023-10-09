using AngleSharp.Dom;
using OpenQA.Selenium;
using PlanItJupiterTestAutomation.Models;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PlanItJupiterTestAutomation.Pages
{
    internal class ShopPage
    {

        //#product-2 div a.btn.btn-success

        IWebDriver driver;
        List<ShopItem> shoppingItems = new List<ShopItem>();

        public ShopPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.CssSelector, Using = "#product-2 div a.btn.btn-success")]
        private IWebElement stuffedFrogAddButton;

        [FindsBy(How = How.CssSelector, Using = "#product-2 div span")]
        private IWebElement stuffedFrogPriceTag;

        [FindsBy(How = How.CssSelector, Using = "#product-4 div a.btn.btn-success")]
        private IWebElement fluffyBunnyAddButton;

        [FindsBy(How = How.CssSelector, Using = "#product-4 div span")]
        private IWebElement fluffyBunnyPriceTag;

        [FindsBy(How = How.CssSelector, Using = "#product-7 div a.btn.btn-success")]
        private IWebElement valentineBearAddButton;

        [FindsBy(How = How.CssSelector, Using = "#product-7 div span")]
        private IWebElement valentineBearPriceTag;

        //#nav-cart
        [FindsBy(How = How.CssSelector, Using = "#nav-cart")]
        private IWebElement navigateToCart;


        public void  addStuffedFrog()
        {
            stuffedFrogAddButton.Click();
        }

        public CartPage navigateIntoCort()
        {
            navigateToCart.Click();
            return new CartPage(driver);
        }

        public decimal getstuffedFrogPrice()
        {
            return decimal.Parse(stuffedFrogPriceTag.Text.Substring(1));
        }

        public decimal getfluffyBunnyPrice()
        {
            return decimal.Parse(fluffyBunnyPriceTag.Text.Substring(1));
        }

        public decimal getvalentineBearPrice()
        {
            return decimal.Parse(valentineBearPriceTag.Text.Substring(1));
        }


        public void buyItem(string itemName, int itemCount)
        {
            switch(itemName)
            {
                case "Stuff Frog":
                    addItemToCart(itemCount, stuffedFrogAddButton);
                    calShoppingItemSubTotal(itemName, itemCount, stuffedFrogPriceTag);
                    break;

                case "Fluffy Bunny":
                    addItemToCart(itemCount, fluffyBunnyAddButton);
                    calShoppingItemSubTotal(itemName, itemCount, fluffyBunnyPriceTag);
                    break;

                case "Valentine Bear":
                    addItemToCart(itemCount, valentineBearAddButton);
                    calShoppingItemSubTotal(itemName, itemCount, valentineBearPriceTag);
                    break;
            }
        }

        public void addItemToCart(int  itemCount, IWebElement element)
        {
            for (int i = 0; i < itemCount; i++)
            {
                element.Click();                
            }
        }

        public void calShoppingItemSubTotal(string itemName, int itemCount, IWebElement element)
        {
            var itemPrice = decimal.Parse(element.Text.Substring(1));
            var subtotal = itemPrice * itemCount;

            ShopItem item = new ShopItem
            {
                ItemName = itemName,
                Quantity = itemCount,
                Price = itemPrice,
                ItemTotal = subtotal
            };

            shoppingItems.Add(item);

        }

        public IEnumerable<ShopItem> getShoppingItemList()
        {
            return shoppingItems;
        }

        public decimal getShopSubtotal()
        {
            decimal sum = 0;
            for (int i = 0;i < shoppingItems.Count; i++)
            {
                sum = sum + shoppingItems.ToList<ShopItem>()[i].ItemTotal;
            }
            TestContext.Progress.WriteLine("====");
            TestContext.Progress.WriteLine(sum);
            return sum;
        }





    }
}
