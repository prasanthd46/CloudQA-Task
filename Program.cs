
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{

    static void TestNameField(string[] names, IWebElement nameField)
    {
        foreach (string name in names)
        {
            bool isValid = true;
            try
            {
                nameField.Clear();
                nameField.SendKeys(name);
                if (nameField.GetAttribute("value") == null)
                {
                    throw new Exception("Field value is null after sending keys");
                }
                string actualValue = nameField.GetAttribute("value");
                IsValidName(name);
            }
            catch (Exception ex)
            {
                isValid = false;
                Console.WriteLine($"Test case failed for value '{name}': failure cause {ex.Message}");
            }
            finally
            {
                if (isValid)
                {
                    Console.WriteLine($"Test case passed for value :'{name}'");
                }
            }
        }
    }
    static void TestNumField(string[] nums, IWebElement numField)
    {
        foreach (string num in nums) {
            bool isValid = true;
            try
            {
                numField.Clear();
                numField.SendKeys(num);
                if (numField.GetAttribute("value") == null)
                {
                    throw new Exception("Field value is null after sending keys");
                }
                isValidNum(num);
            }
            catch (Exception ex)
            {
                isValid = false;
                Console.WriteLine($"Test case failed for value '{num}': failure cause {ex.Message}");
            }
            finally
            {
                if (isValid)
                {
                    Console.WriteLine($"Test case passed for value :'{num}'");
                }
            }
        }
    }
    private static void isValidNum(string num) {

        if (string.IsNullOrWhiteSpace(num))
            throw new ArgumentException("Number cannot be null or whitespace");
        if (num.Contains(" ")) 
            throw new ArgumentException("Number cannot contain spaces");
        if (num.Any(char.IsLetter)) 
            throw new ArgumentException("Number cannot contain letters");
        if (!num.All(c => char.IsDigit(c)))
            throw new ArgumentException("Number can only contain digits");
        if(num.Length != 10)
            throw new ArgumentException("Number must be 10 digits long");
    }

    private static void IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or whitespace");
        if (name.Contains(" "))
            throw new ArgumentException("Name cannot contain spaces");
        if (name.Any(char.IsDigit))
            throw new ArgumentException("Name cannot contain digits");
        if (!name.All(c => char.IsLetter(c)))
            throw new ArgumentException("Name can only contain letters");
        if (name.Length < 2 || name.Length > 50)
            throw new ArgumentException("Name must be between 2 and 50 characters long");
    }

    static void Main(string[] args)
    {
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://app.cloudqa.io/home/AutomationPracticeForm");
        driver.Manage().Window.Maximize();
        IWebElement firstName = driver.FindElement(By.XPath("//*[@id='fname']"));
        IWebElement lastName = driver.FindElement(By.XPath("//*[@id='lname']"));
        IWebElement mobile = driver.FindElement(By.XPath("//*[@id='mobile']"));
        TestNameField(["John123", "JaneDoe", "Alice123", "Bob Smith", "Charlie*", "Diana", "Eve", "Franklin", "Grace", "Hannah Bavi"], firstName);
        TestNameField(["Doe", "Smith", "Johnson", "Brown", "Williams", "Jones", "Garcia", "Martinez", "Davis", "Rodriguez"], lastName);
        TestNumField(["1234567890", "0987654321", "123456789", "12345678901", "12345 67890", "123456789a", "12345678b0", "1234567890!"], mobile);
        Console.WriteLine("\n\n All tests completed.");
        driver.Quit();
    }
}