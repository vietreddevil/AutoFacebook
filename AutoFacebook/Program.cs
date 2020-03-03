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
        public static int[] ThreadCookieSeq = { 0, 0 };
        static async Task Main(string[] args)
        {
            CustomObj cookie;
            using(StreamReader r = new StreamReader("../../cookie.json"))
            {
                string json = r.ReadToEnd();
                cookie = JsonConvert.DeserializeObject<CustomObj>(json);
            }

            Thread ts = new Thread(async () => await FacebookTool(cookie, "sypvudn_bowersescu_1533740491@tfbnw.net", "m1yfrtkg1eu", 0));
            ts.Start();

            Thread ts1 = new Thread(async () => await FacebookTool(cookie, "dcxjatc_putnamstein_1533740513@tfbnw.net", "nj77rawk5yc", 1));
            ts1.Start();

            Console.ReadKey();
        }

        static async Task<int> FacebookTool(CustomObj cookie, string username, string password, int threadSeq)
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
            await Login(dr, cookie, username, password, threadSeq);
            

            

            return 1;
        }

        static async Task<int> Login(IWebDriver dr, CustomObj cookie, string username, string password, int threadSeq)
        {
            bool isLoginByCookie = await IsLoginByCookie(cookie.Nicks, username);
            if(isLoginByCookie) // login by cookie
            {
                ThreadCookieSeq[threadSeq] = 1;
            }else //login by username - pwd
            {
                dr.Url = ("http://m.facebook.com");
                dr.FindElement(By.Id("m_login_email")).SendKeys(username);
                dr.FindElement(By.Id("m_login_password")).SendKeys(password);
                dr.FindElement(By.Id("u_0_5")).Click();
                Thread.Sleep(3000);
                dr.FindElement(By.ClassName("_2pii")).Click();
                var cookies = dr.Manage().Cookies.AllCookies;
                if(threadSeq == 0)
                {
                    await StoreCookie(cookies, username, password, cookie);
                    ThreadCookieSeq[threadSeq] = 1;
                }
                else
                {
                    while(ThreadCookieSeq[threadSeq - 1] == 0)
                    {
                        Thread.Sleep(500);
                        if(ThreadCookieSeq[threadSeq - 1] == 1)
                        {
                            break;
                        }
                    }
                    await StoreCookie(cookies, username, password, cookie);
                    ThreadCookieSeq[threadSeq] = 1;
                }
                
                Console.WriteLine(cookies);
            }
            return 1;
        }

        static async Task<int> StoreCookie(dynamic cookies, string username, string password, CustomObj cookie)
        {
            Nick nick = new Nick();
            string cookie_string = "";
            foreach(dynamic item in cookies)
            {
                cookie_string += Convert.ToString(item) + ";";
            }
            List<string> cookies_to_list = cookie_string.Split(';').ToList();
            foreach(string item in cookies_to_list)
            {
                Console.WriteLine(item);
                if(item.Contains("sb="))
                {
                    nick.Cookie.SB = item.Replace("sb=", "");
                }
                if (item.Contains("xs="))
                {
                    nick.Cookie.XS = item.Replace("xs=", "");
                }
                if (item.Contains("c_user="))
                {
                    nick.Cookie.C_User = item.Replace("c_user=", "");
                }
                if (item.Contains("fr="))
                {
                    nick.Cookie.FR = item.Replace("fr=", "");
                }
                if (item.Contains("datr="))
                {
                    nick.Cookie.DATR = item.Replace("datr=", "");
                }
            }
            for(int i = 0; i < cookie.Nicks.Count; i++)
            {
                if(cookie.Nicks[i].Username == username)
                {
                    cookie.Nicks[i].Cookie = nick.Cookie;
                }
            }
            string result = JsonConvert.SerializeObject(cookie);
            File.WriteAllText("../../cookie.json", result);
            return 1;
        }

        //check xem da co cookie cho tai khoan nay chua
        static async Task<bool> IsLoginByCookie(List<Nick> nicks, string username)
        {
            bool result = false;
            if (nicks.Count == 0)
            {
                return result;
            }
            
            foreach(Nick nick in nicks)
            {
                if(nick.Username == username)
                {
                    if(nick.Cookie.SB != "")
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
