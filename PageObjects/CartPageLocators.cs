using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Lazar.Stojkovski.PageObjects
{
    public class CartPageLocators
    {
        public By ViewCartLink => By.XPath("//div[contains(., 'has been added to your cart')]/a[text()='View cart']");
        public By ProceedToCheckoutLink => By.XPath("//a[contains(., 'Proceed to checkout')]");
        public By FirstNameField => By.XPath("//*[@id='billing_first_name_field']//input");
        public By LastNameField => By.XPath("//*[@id='billing_last_name_field']//input");
        public By CompanyField => By.XPath("//*[@id='billing_company_field']//input");
        public By BookFormat => By.XPath("//select[@id='pa_format']");
        public By AddToCart => By.XPath("//button[@type='submit' and text()='Add to cart']");

        public By CountryDropDown => By.XPath("//span[@class='selection']/*");
        public By NorthMacedoniaOption => By.XPath("//li[text()='North Macedonia']");
        public By StreetAddressField => By.XPath("//input[@id='billing_address_1']");
        public By CityField => By.XPath("//input[@id='billing_city']");
        public By StateField => By.XPath("//input[@id='billing_state']");
        public By PostCodeField => By.XPath("//input[@id='billing_postcode']");
        public By PhoneField => By.XPath("//input[@id='billing_phone']");
        public By EmailField => By.XPath("//input[@id='billing_email']");
        public By PlaceOrderButton => By.XPath("//button[@id='place_order']");
        public By OrderReceivedMessage => By.XPath("//h6[text()='Order received']");

    }
}
