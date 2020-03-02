using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoFacebook
{
    class Program
    {
        static async Task Main(string[] args)
        {
            dynamic cookie;
            using(StreamReader r = new StreamReader("../../cookie.json"))
            {
                string json = r.ReadToEnd();
                cookie = JsonConvert.DeserializeObject<dynamic>(json);
            }
            //Console.WriteLine(cookie.nicks);
            Thread ts = new Thread(async () => await LoginFacebook(cookie, "sypvudn_bowersescu_1533740491@tfbnw.net", "m1yfrtkg1eu", 0));
            ts.Start();

            Thread ts1 = new Thread(async () => await LoginFacebook(cookie, "dcxjatc_putnamstein_1533740513@tfbnw.net", "nj77rawk5yc", 1));
            ts1.Start();

            Console.ReadKey();
        }

        static async Task<int> LoginFacebook(dynamic cookie, string username, string password, int threadSeq)
        {
            ChromeOptions op = new ChromeOptions();
            var set = new ChromeMobileEmulationDeviceSettings();
            set.Height = 350;
            set.Width = 219;
            set.PixelRatio = 2.0;
            set.UserAgent = "Mozilla/5.0 (Linux; U; Android 4.3; en-us; SM-N900T Build/JSS15J) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30";
            set.EnableTouchEvents = true;

            op.EnableMobileEmulation(set);
            op.AddArgument("--disable-notifications");
            op.AddArgument("--app=https://m.facebook.com");
            op.AddExcludedArgument("enable-automation");
            op.AddAdditionalCapability("useAutomationExtension", false);
            op.AddUserProfilePreference("credentials_enable_service", false);
            op.AddUserProfilePreference("profile.password_manager_enabled", false);
            op.AddArgument("window-size=234,350");
            op.AddArgument("window-position=" + (-2 + threadSeq*223) + ",7");
            IWebDriver dr = new ChromeDriver(op);
            dr.Url = ("http://m.facebook.com");
            dr.FindElement(By.Id("m_login_email")).SendKeys(username);
            dr.FindElement(By.Id("m_login_password")).SendKeys(password);
            dr.FindElement(By.Id("u_0_5")).Click();
            Thread.Sleep(3000);
            dr.FindElement(By.ClassName("_2pii")).Click();

            return 1;
        }
    }
}
