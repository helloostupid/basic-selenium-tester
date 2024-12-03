using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

class CloudQATest 
{
    static IWebElement FindElementMultipleWays(IWebDriver driver, params By[] locators){
        foreach(By locator in locators){
            try{
                var elements = driver.FindElements(locator);
                if(elements.Count > 0){
                    return elements[0];
                }
            }
            catch { 

            }
        }

        throw new Exception("Could not find element");
    }

    static void Main(string[] args)
    {
        IWebDriver driver = new ChromeDriver();
        
        try 
        {
            driver.Navigate().GoToUrl("https://app.cloudqa.io/home/AutomationPracticeForm");

            // Field 1: First Name Input
            var firstNameInput = FindElementMultipleWays(driver, 
                By.CssSelector(".form-group input[type='text'][name='First Name']"),
                By.Id("fname"),
                By.Name("First Name"),
                By.XPath("//input[@placeholder='Name']")
            );

            firstNameInput.Clear();
            firstNameInput.SendKeys("Govind");

            string enteredName = firstNameInput.GetDomProperty("value");
            
            if(enteredName == "Govind")
                Console.WriteLine("First Name test passed!");
            else
                throw new Exception("First Name test failed!");


            // Field 2: Gender Select
            var genderMale = FindElementMultipleWays(driver,
                By.CssSelector("input[type='radio'][value='Male']"),
                By.Id("male"),
                By.Name("gender"),
                By.XPath("//input[@value='Male']")
            );

            genderMale.Click();

            if(genderMale.Selected)
                Console.WriteLine("Gender test passed!");
            else
                throw new Exception("Gender test failed!");


            // Field 3: Country Input
            var countryInput = FindElementMultipleWays(driver,
                By.CssSelector(".autocomplete input[type='text'][placeholder='Country']"),
                By.Id("country"),
                By.Name("Country"),
                By.XPath("//input[@placeholder='Country']")
            );

            countryInput.Clear();
            countryInput.SendKeys("United");

            var countrySuggestion = FindElementMultipleWays(driver,
                By.CssSelector(".autocomplete-items div input[type='hidden'][value='United Kingdom']/.."),
                By.XPath("//input[@type='hidden' and @value='United Kingdom']/..")
                // By.CssSelector(".autocomplete-items div input[type='hidden'][value='United Kingdom']")
                // By.XPath("//div[@id='countryautocomplete-list']//div[contains(strong, 'United Kingdom')]")
            );

            countrySuggestion.Click();

            string selectedCountry = countryInput.GetDomProperty("value");
            
            if(selectedCountry == "United Kingdom")
                Console.WriteLine("Country autocomplete test passed!");
            else
                throw new Exception("Country autocomplete test failed!");
        }
        catch(Exception ex){
            Console.WriteLine($"Test failed: {ex.Message}");
        }
        finally {
            driver.Quit();
        }
    }
}