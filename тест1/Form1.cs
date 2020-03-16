﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium; //подключили selenium
using OpenQA.Selenium.Support.UI;

namespace тест1
{
    public partial class Form1 : Form
    {
        IWebDriver Browser; // объявили переменную

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Browser.Navigate().GoToUrl("http://google.com");

            IWebElement SearchInput = Browser.FindElement(By.Id("lst-ib"));

            SearchInput.SendKeys("как вырастить гомункула" + OpenQA.Selenium.Keys.Enter);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Browser.Quit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //  OpenQA.Selenium.Chrome.ChromeOptions co = new OpenQA.Selenium.Chrome.ChromeOptions();
            //  co.AddArgument(@"user-data-dir=c:\Users\user\AppData\Local\Google\Chrome\User Data\");
            Browser = new OpenQA.Selenium.Chrome.ChromeDriver(/*co*/);

            Browser.Manage().Window.Maximize();

            // co.AddArgument(@"user-data-dir=c:\Users\user\AppData\Local\Google\Chrome\User Data\");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IWebElement element;
            //поиск по ИД
            //element = Browser.FindElement(By.Id("text"));
            //element.SendKeys("тест");

            //поиск по имени класса
            //element = Browser.FindElement(By.ClassName("football__close"));
            //element.Click();

            //поиск по тексту ссылки
            //element = Browser.FindElement(By.LinkText("Картинки"));
            //element.Click();

            //поиск по частичному тексту ссылки
            element = Browser.FindElement(By.PartialLinkText("ревод"));
            element.Click();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<IWebElement> News = Browser.FindElements(By.CssSelector("#tabnews_newsc a")).ToList();

            for (int i = 0; i < News.Count; i++)
            {
                String s = News[i].Text;

                if (s.StartsWith("ФСБ"))
                {
                    textBox1.AppendText("Новость № " + (i + 1).ToString() + "  начинается с текста 'ФСБ'" + "\r\n");
                }


                if (s.EndsWith("Кремля"))
                {
                    textBox1.AppendText("Новость № " + (i + 1).ToString() + "  заканчивается текстом 'Кремля'" + "\r\n");
                }

                if (s.Contains("вывести"))
                {
                    textBox1.AppendText("Новость № " + (i + 1).ToString() + "  содержит текст 'вывести'" + "\r\n");
                    News[i].Click();
                    break;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            IJavaScriptExecutor jse = Browser as IJavaScriptExecutor;
            jse.ExecuteScript(textBox1.Text);
        }


        private string FindWindow(String Url)
        {
            String StartWindow = Browser.CurrentWindowHandle;
            String Result = "";

            for (int i = 0; i < Browser.WindowHandles.Count; i++)
            {
                if (Browser.WindowHandles[i] != StartWindow)
                {
                    Browser.SwitchTo().Window(Browser.WindowHandles[i]);
                    if (Browser.Url.Contains(Url))
                    {
                        Result = Browser.WindowHandles[i];
                        break;
                    }
                }
            }


            Browser.SwitchTo().Window(StartWindow);
            return Result;
        }


        private void button7_Click(object sender, EventArgs e)
        {
            /*  Browser.SwitchTo().Window( Browser.WindowHandles[1] );
              System.Windows.Forms.MessageBox.Show(Browser.Title + "\r\n" + Browser.Url);

              Browser.SwitchTo().Window(Browser.WindowHandles[0]);
              System.Windows.Forms.MessageBox.Show(Browser.Title + "\r\n" + Browser.Url);

              Browser.SwitchTo().Window(Browser.WindowHandles[2]);
              System.Windows.Forms.MessageBox.Show(Browser.Title + "\r\n" + Browser.Url);*/

            /*String HabrWindow = FindWindow("habr");
            Browser.SwitchTo().Window(HabrWindow);
            System.Windows.Forms.MessageBox.Show(Browser.Title + "\r\n" + Browser.Url);*/

            List<String> BeforeTabs = Browser.WindowHandles.ToList();
            //делаем что то - открывается одна новая вкладка
            //....
            List<String> AfterTabs = Browser.WindowHandles.ToList();
            //вкладки до - вкладки после = новая вкладка
            List<String> OneNewTab = AfterTabs.Except(BeforeTabs).ToList();
            Browser.SwitchTo().Window(OneNewTab[0]);
            System.Windows.Forms.MessageBox.Show(Browser.Title + "\r\n" + Browser.Url);
        }

        private void button8_Click(object sender, EventArgs e)
        {

            Browser.Navigate().GoToUrl("http://www.degraeve.com/reference/simple-ajax-example.php");



            IWebElement Button = Browser.FindElement(By.CssSelector("input[value='Go']"));
            Button.Click();

            WebDriverWait ww = new WebDriverWait(Browser, TimeSpan.FromSeconds(10));
            IWebElement txt = ww.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result  p")));


            textBox1.Text = txt.Text;

            // System.Threading.Thread.Sleep(5000);
            //Browser.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            /* WebDriverWait ww = new WebDriverWait(Browser, TimeSpan.FromSeconds(10));
             IWebElement txt = ww.Until(ExpectedConditions.ElementIsVisible( By.CssSelector("#result  p") ));*/
        }

        IWebElement GetElement(By locator)
        {
            List<IWebElement> elements = Browser.FindElements(locator).ToList();
            if (elements.Count > 0) return elements[0];
            else return null;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Browser.Navigate().GoToUrl("http://www.yandex.ru");
            IWebElement login = GetElement(By.ClassName("fdsada"));
            if (login != null)
            {
                login.Click();
            }
        }
    }
}