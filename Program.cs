using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

class CloudQATest 
{
    static void Main(string[] args)
    {
        IWebDriver driver = new ChromeDriver();
        
        try 
        {
            driver.Navigate().GoToUrl("https://app.cloudqa.io/home/AutomationPracticeForm");

            // Field 1: First Name Input
            var firstNameInput = driver.FindElement(By.Id("fname"));
            firstNameInput.SendKeys("Govind");
            string enteredName = firstNameInput.GetDomProperty("value");
            
            if(enteredName == "Govind")
                Console.WriteLine("First Name test passed!");
            else
                throw new Exception("First Name test failed!");


            // Field 2: Gender Select
            var genderMale = driver.FindElement(By.Id("male"));
            genderMale.Click();

            if(genderMale.Selected)
                Console.WriteLine("Gender test passed!");
            else
                throw new Exception("Gender test failed!");

            // Field 3: Country Input
            var countryInput = driver.FindElement(By.Id("country"));
            countryInput.SendKeys("India");

            var countrySuggestion = driver.FindElement(
                By.XPath("//div[@id='countryautocomplete-list']//div[contains(strong, 'India')]")
            );
            countrySuggestion.Click();

            string selectedCountry = countryInput.GetDomProperty("value");
            
            if(selectedCountry == "India")
                Console.WriteLine("Country autocomplete test passed!");
            else
                throw new Exception("Country autocomplete test failed!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
        }
        finally 
        {
            driver.Quit();
        }
    }
}