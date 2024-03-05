using FinalProject_Lazar.Stojkovski.PageObjects;
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
        public void WaitForPageToLoad(IWebDriver driver)
        {
            // Wait for the document to be in the 'complete' state
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void WaitForBlockUItoDissapear(IWebDriver driver)
        {
            // Wait for the blocker overlay to disappear
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='blockUI blockOverlay']")));
        }

        private IWebDriver driver;
        private WebDriverWait wait;


        [SetUp]
        public void Setup()
        {
            // Initialize the WebDriver instance
            driver = new ChromeDriver();

            // Set implicit wait timeout
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            // Initialize WebDriverWait
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("https://bookworm.madrasthemes.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            // Instantiate page locators
            HomePageLocators homePageLocators = new HomePageLocators();
            CartPageLocators cartPageLocators = new CartPageLocators();

            // Click on Categories button
            homePageLocators.CategoriesButton.FindElement(driver).Click();

            // Define book author and title
            string author = "Anna Banks";
            string title = "All You Can Ever Know: A Memoir";

            // Click on the book card
            homePageLocators.BookCard(author, title).FindElement(driver).Click();

            // Wait for the blocker overlay to disappear
            WaitForBlockUItoDissapear(driver);

            // Scroll to Add Wishlist button
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", homePageLocators.AddWishlistButton.FindElement(driver));

            // Wait for Add Wishlist button to be clickable
            wait.Until(ExpectedConditions.ElementToBeClickable(homePageLocators.AddWishlistButton));

            // Click on Add Wishlist button
            homePageLocators.AddWishlistButton.FindElement(driver).Click();

            // Wait for the page to load
            WaitForPageToLoad(driver);

            // Wait for Browse Wishlist link to exist and be clickable
            wait.Until(ExpectedConditions.ElementExists(homePageLocators.BrowseWishlistLink));
            wait.Until(ExpectedConditions.ElementToBeClickable(homePageLocators.BrowseWishlistLink));

            // Click on Browse Wishlist link
            homePageLocators.BrowseWishlistLink.FindElement(driver).Click();

            // Wait for the page to load
            WaitForPageToLoad(driver);

            // Scroll down 500 pixels
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,500);");

            // Wait for the item to be visible
            wait.Until(ExpectedConditions.ElementIsVisible(homePageLocators.ItemAdded));
            IWebElement itemAdded = driver.FindElement(homePageLocators.ItemAdded);
            Assert.That(itemAdded, Is.Not.Null);

            // Get the wishlist title value
            string wishlistTitleValue1 = driver.FindElement(homePageLocators.WishlistTitle).Text;
            Assert.That(wishlistTitleValue1, Is.EqualTo("My wishlist"));

            // Click on Edit button
            homePageLocators.EditButton.FindElement(driver).Click();

            // Edit wishlist title
            IWebElement editWishlist = driver.FindElement(homePageLocators.EditWishlistInput);
            new Actions(driver)
                .KeyDown(Keys.Control)
                .SendKeys(editWishlist, "a")
                .KeyUp(Keys.Control)
                .SendKeys(editWishlist, Keys.Delete)
                .Perform();
            editWishlist.SendKeys("Wishlist title edited");

            // Click on Save Title button
            homePageLocators.SaveTitleButton.FindElement(driver).Click();

            // Wait for the blocker overlay to disappear
            WaitForBlockUItoDissapear(driver);

            // Wait for the page to load
            WaitForPageToLoad(driver);

            // Refresh the page
            driver.Navigate().Refresh();

            // Wait for the page to load
            WaitForPageToLoad(driver);

            // Get the updated wishlist title value
            string wishlistTitleValueStr = homePageLocators.GetWishlistTitleInputFieldValue(driver);

            // Assert the updated wishlist title value
            Assert.That(wishlistTitleValueStr, Is.EqualTo("Wishlist title edited"));
        }

        [Test]
        public void Test2()
        {
            // Instantiate page locators
            HomePageLocators homePageLocators = new HomePageLocators();
            CartPageLocators cartPageLocators = new CartPageLocators();

            // Click on the Categories button
            homePageLocators.CategoriesButton.FindElement(driver).Click();

            // Define book author and title
            string author = "Anna Banks";
            string title = "All You Can Ever Know: A Memoir";

            // Click on the book card
            homePageLocators.BookCard(author, title).FindElement(driver).Click();

            // Wait for the blocker overlay to disappear
            WaitForBlockUItoDissapear(driver);

            // Find and select book format from dropdown
            IWebElement bookFormatDD = driver.FindElement(cartPageLocators.BookFormat);
            SelectElement selectBookFormat = new SelectElement(bookFormatDD);
            selectBookFormat.SelectByText("Hardcover $29.95");

            // Wait for Add to Cart button to be clickable and click on it
            wait.Until(ExpectedConditions.ElementToBeClickable(cartPageLocators.AddToCart.FindElement(driver)));
            cartPageLocators.AddToCart.FindElement(driver).Click();

            // Wait for View Cart link to be clickable and click on it
            wait.Until(ExpectedConditions.ElementToBeClickable(cartPageLocators.ViewCartLink.FindElement(driver)));
            cartPageLocators.ViewCartLink.FindElement(driver).Click();

            // Wait for the page to load and proceed to checkout
            wait.Until(ExpectedConditions.UrlContains("/Cart/"));
            WaitForPageToLoad(driver);
            wait.Until(ExpectedConditions.ElementToBeClickable(cartPageLocators.ProceedToCheckoutLink.FindElement(driver)));
            cartPageLocators.ProceedToCheckoutLink.FindElement(driver).Click();

            // Wait for the checkout page to load
            wait.Until(ExpectedConditions.UrlContains("/Checkout/"));
            WaitForPageToLoad(driver);

            //Define form values
            string name = "Lazar";
            string surname = "Stojkovski";
            string company = "007";
            string address = "Pero Nakov 60";
            string city = "Gegelija";
            string state = "MKD";
            string postCode = "1480";
            string phoneNr = "38971832623";
            string email = "lazar.stojkovski15@gmail.com";

            // Find and populate the first name field
            IWebElement firstNameField = driver.FindElement(cartPageLocators.FirstNameField);
            firstNameField.SendKeys(name);

            // Find and populate the last name field
            IWebElement lastNameField = driver.FindElement(cartPageLocators.LastNameField);
            lastNameField.SendKeys(surname);

            // Find and populate the company field
            IWebElement companyField = driver.FindElement(cartPageLocators.CompanyField);
            companyField.SendKeys(company);

            // Find and click on the country drop-down
            IWebElement countryDropDown = driver.FindElement(cartPageLocators.CountryDropDown);
            wait.Until(ExpectedConditions.ElementToBeClickable(cartPageLocators.CountryDropDown));
            countryDropDown.Click();

            // Find and click on the North Macedonia option
            IWebElement northMacedoniaOption = driver.FindElement(cartPageLocators.NorthMacedoniaOption);
            wait.Until(ExpectedConditions.ElementToBeClickable(cartPageLocators.NorthMacedoniaOption));
            northMacedoniaOption.Click();

            // Find and populate the street address field
            IWebElement streetAddressField = driver.FindElement(cartPageLocators.StreetAddressField);
            streetAddressField.SendKeys(address);

            // Find and populate the city field
            IWebElement cityField = driver.FindElement(cartPageLocators.CityField);
            cityField.SendKeys(city);

            // Find and populate the state field
            IWebElement stateField = driver.FindElement(cartPageLocators.StateField);
            stateField.SendKeys(state);

            // Find and populate the postal code field
            IWebElement postCodeField = driver.FindElement(cartPageLocators.PostCodeField);
            postCodeField.SendKeys(postCode);

            // Find and populate the phone number field
            IWebElement phoneField = driver.FindElement(cartPageLocators.PhoneField);
            phoneField.SendKeys(phoneNr);

            // Find and populate the email field
            IWebElement emailField = driver.FindElement(cartPageLocators.EmailField);
            emailField.SendKeys(email);

            // Scroll to "Place Order" button
            WaitForBlockUItoDissapear(driver);
            IWebElement placeOrderBtn = driver.FindElement(cartPageLocators.PlaceOrderButton);
            placeOrderBtn.Click();

            // Wait for the order received message to appear
            IWebElement orderReceived = wait.Until(ExpectedConditions.ElementIsVisible(cartPageLocators.OrderReceivedMessage));
            bool isElementVisible = orderReceived.Displayed;
            Assert.That(isElementVisible, Is.True);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }
    }
}