using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;

namespace FinalProject_Lazar.Stojkovski
{
    public class Tests
    {

        public static IWebDriver driver = new ChromeDriver();
        public static WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        public static Actions actions = new Actions(driver);

        [SetUp]
        public void Setup()
        {
            driver.Navigate().GoToUrl("https://bookworm.madrasthemes.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            IWebElement categoriesButton = driver.FindElement(By.XPath("//a[@title='Categories']/parent::li"));
            categoriesButton.Click();

            IWebElement bookCard = driver.FindElement(By.XPath("//a[text()='Anna Banks']/ancestor::div[2]//*[contains(text(),'All You Can Ever Know: A Memoir')]/ancestor::li"));
            bookCard.Click();

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='blockUI blockOverlay']")));
            IWebElement addWishlist = driver.FindElement(By.XPath("(//i[@class='flaticon-heart']/ancestor::a)[1]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", addWishlist);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//i[@class='flaticon-heart']/ancestor::a)[1]")));
            addWishlist.Click();

            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[text()='Browse wishlist']/ancestor::a")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text()='Browse wishlist']/ancestor::a")));
            IWebElement browseWishlist = driver.FindElement(By.XPath("//span[text()='Browse wishlist']/ancestor::a"));
            browseWishlist.Click();

            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,500);");
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table/tbody/tr/td[@class='product-name']/*")));
            IWebElement itemAdded = driver.FindElement(By.XPath("//table/tbody/tr/td[@class='product-name']/*"));
            Assert.That(itemAdded, Is.Not.Null);

            string wishlistTitleValue1 = driver.FindElement(By.XPath("//a[@class='save-title-form']/ancestor::div[3]//h2")).Text;
            Console.WriteLine(wishlistTitleValue1);
            Assert.That(wishlistTitleValue1, Is.EqualTo("My wishlist"));

            IWebElement editButton = driver.FindElement(By.XPath("//a[@class='btn button show-title-form']"));
            editButton.Click();

            IWebElement editWishlist = driver.FindElement(By.XPath("//div[@class='hidden-title-form']/input[@name='wishlist_name']"));
            //Actions actions = new Actions(driver);
            actions.KeyDown(Keys.Control).SendKeys("a").KeyUp(Keys.Control);
            actions.SendKeys(Keys.Delete);
            actions.Build().Perform();
            editWishlist.SendKeys("Wishlist title edited");
            
            IWebElement checkMark = driver.FindElement(By.XPath("//div[@class='hidden-title-form']//a[@role='button' and @class='save-title-form']"));
            checkMark.Click();

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='blockUI blockOverlay']")));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            driver.Navigate().Refresh();
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            IWebElement wishlistTitleValue = driver.FindElement(By.XPath("//div[@class='hidden-title-form']/input[@type='text']"));
            string wishlistTitleValueStr = wishlistTitleValue.GetAttribute("value");
            Console.WriteLine(wishlistTitleValueStr);
            Assert.That(wishlistTitleValueStr, Is.EqualTo("Wishlist title edited"));
        }

        [Test]
        public void Test2()
        {
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            IWebElement categoriesButton = driver.FindElement(By.XPath("//a[@title='Categories']/parent::li"));
            categoriesButton.Click();

            IWebElement bookCard = driver.FindElement(By.XPath("//a[text()='Anna Banks']/ancestor::div[2]//*[contains(text(),'All You Can Ever Know: A Memoir')]/ancestor::li"));
            bookCard.Click();

        }
        [TearDown]
        public void Teardown()
        {
            driver.Close();
            driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Dispose();
        }
    }
}