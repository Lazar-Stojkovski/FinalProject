using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Lazar.Stojkovski.PageObjects
{
    public class HomePageLocators
    {
        public By CategoriesButton => By.XPath("//a[@title='Categories']/parent::li");
        public By BookCard(string author, string bookTitle) => By.XPath($"//a[text()='{author}']/ancestor::div[2]//*[contains(text(),'{bookTitle}')]/ancestor::li");
        public By AddWishlistButton => By.XPath("(//i[@class='flaticon-heart']/ancestor::a)[1]");
        public By BrowseWishlistLink => By.XPath("//span[text()='Browse wishlist']/ancestor::a");
        public By EditButton => By.XPath("//a[@class='btn button show-title-form']");
        public By EditWishlistInput => By.XPath("//div[@class='hidden-title-form']/input[@name='wishlist_name']");
        public By SaveTitleButton => By.XPath("//div[@class='hidden-title-form']//a[@role='button' and @class='save-title-form']");
        public By ItemAdded => By.XPath("//table/tbody/tr/td[@class='product-name']/*");
        public By WishlistTitle => By.XPath("//a[@class='save-title-form']/ancestor::div[3]//h2");
        public By WishlistTitleInputField => By.XPath("//div[@class='hidden-title-form']/input[@type='text']");

        // Method to retrieve the value of the wishlist title input field
        public string GetWishlistTitleInputFieldValue(IWebDriver driver)
        {
            IWebElement wishlistTitleInput = driver.FindElement(WishlistTitleInputField);
            return wishlistTitleInput.GetAttribute("value");
        }
    }
}
