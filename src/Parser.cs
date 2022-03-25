using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.PhantomJS;
using System.Text.RegularExpressions;
using System.Runtime;

namespace parser
{
    public static class Parser
    {
        public static string[][] wildberriesParse(string[][] data)
        {
            string[][] output = new string[0][];
            var options = new ChromeOptions();
            options.AddArguments("headless");
            //options.AddArguments("--port 3307");
            IWebDriver driver = new PhantomJSDriver();

            driver.Navigate().GoToUrl("https://www.wildberries.ru/");
            for (int row = 0; row < data.Length; ++row)
            {
                string[] currentQueries = data[row][2].Split(", ");
                string results = "";
                for (int i = 0; i < currentQueries.Length; ++i)
                {
                    driver.Navigate().GoToUrl("https://www.wildberries.ru/");
                    Thread.Sleep(1000);
                    Console.WriteLine("OK");
                    driver.FindElement(By.ClassName("search-catalog__input")).SendKeys(currentQueries[i] + Keys.Return);
                    Console.WriteLine("Работаем. ID: " + data[row][1] + ". Запрос: " + currentQueries[i]);
                    int currentPosition = 0;
                    bool isFound = false;
                    for (int page = 0; page < 1000; ++page)
                    {
                        string html = "";
                        for (int tryNumber = 0; tryNumber < 25; tryNumber++)
                        {
                            try
                            {
                                html = driver.FindElement(By.ClassName("product-card-list")).GetAttribute("innerHTML");
                                break;
                            } catch (Exception)
                            {
                                Thread.Sleep(200);
                            }
                            
                        }
                        if (html == "") throw new Exception("Поле с товарами не найдено");
                        string[] positions = Regex.Matches(html, @"(?<=id=""c)(\d*)(?="")")
                            .Cast<Match>()
                            .Select(m => m.Value)
                            .ToArray();
                        if (Array.IndexOf(positions, data[row][1].ToString()) != -1)
                        {
                            isFound = true;
                            currentPosition += (Array.IndexOf(positions, data[row][1]) + 1);
                            Console.WriteLine("Позиция найдена: " + currentPosition.ToString());
                            break;
                        }
                        currentPosition += positions.Length;
                        bool isNextArrow = false;
                        for (int tryNumber = 0; tryNumber < 15; tryNumber++)
                        {
                            try
                            {
                                driver.FindElement(By.CssSelector("span[class='arrow next']")).Click();
                                isNextArrow = true;
                                break;
                            }
                            catch (Exception)
                            {
                                Thread.Sleep(200);
                            }

                        }
                        if (!isNextArrow) break;
                    }
                    if (!isFound) currentPosition = -1;
                    results += currentPosition.ToString() + ", ";
                }
                string[] currentResult = { data[row][1], results.Substring(0, results.Length - 2)};
                output = output.Concat(new[] { currentResult }).ToArray();
                Console.WriteLine(output[row][0]);
                Console.WriteLine(output[row][1]);
            }
            driver.Close();
            return output;
        }
    }
}
