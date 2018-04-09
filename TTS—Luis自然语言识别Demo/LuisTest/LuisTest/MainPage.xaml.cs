using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace LuisTest
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            try
            {
                string str = GetLuisResult("hierarchicalintent _ test1").Result;
                int a = 0;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private async Task<string> GetLuisResult(string askString)
        {
            string url = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/de176698-a85b-4e2c-9e7d-58d3d690e11f?subscription-key=1fee7c75cfa74d99a9aabd81e2d40a59&verbose=true&timezoneOffset=0&q=";
            string result = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(url + askString).Result;
                    if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                    }
                }
                catch (HttpRequestException ex)
                {

                }
            }
            return result;
        }
    }
}
