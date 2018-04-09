using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Bot_Application3.Dialogs
{

    [LuisModel("de176698-a85b-4e2c-9e7d-58d3d690e11f", "1fee7c75cfa74d99a9aabd81e2d40a59")]
    [Serializable]
    public class MengmengDialog : LuisDialog<object>
    {
        public MengmengDialog()
        {
        }
        public MengmengDialog(ILuisService service)
        : base(service)
        {
        }
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Luis失败！";//+ string.Join(", ", result.Intents.Select(i => i.Intent));

            string location = "";
            string replyString = "";

            replyString = await GetLuisResult(location);
            await context.PostAsync(replyString);
            context.Wait(MessageReceived);
        }
        public bool TryToFindLocation(LuisResult result, out String location)
        {
            location = "";
            EntityRecommendation title;
            if (result.TryFindEntity("Answer", out title))
            {
                location = title.Entity;
            }
            else
            {
                location = "";
            }
            return !location.Equals("");
        }

        [LuisIntent("PatientDialogue")]
        public async Task QueryWeather(IDialogContext context, LuisResult result)
        {
            string location = "";
            string replyString = "";
            if (TryToFindLocation(result, out location))
            {
                replyString = await GetLuisResult(location);
                await context.PostAsync(replyString);
                context.Wait(MessageReceived);
            }
            else
            {
                await context.PostAsync("亲你要查询哪个地方的天气信息呢，快把城市的名字发给我吧!");
                context.Wait(MessageReceived);
            }
        }

        //[LuisIntent("Test")]
        public async Task QueryTest(IDialogContext context, LuisResult result)
        {
            string location = "";
            string replyString = "";
            // if (TryToFindLocation(result, out location))
            {
                replyString = await GetLuisResult(location);
                //replyString = (string)result.Entities[0].Resolution["values"];
                await context.PostAsync(replyString);
                context.Wait(MessageReceived);
            }
            //else
            {
                //await context.PostAsync("亲你要查询哪个地方的天气信息呢，快把城市的名字发给我吧!");
                //context.Wait(MessageReceived);
            }
        }

        //[LuisIntent("DialoguaTest")]
        public async Task QueryDialoguaTest(IDialogContext context, LuisResult result)
        {
            string location = "";
            string replyString = "";
            // if (TryToFindLocation(result, out location))
            {
                replyString = await GetLuisResult(location);
                //replyString = (string)result.Entities[0].Resolution["values"];
                await context.PostAsync(replyString);
                context.Wait(MessageReceived);
            }
            //else
            {
                //await context.PostAsync("亲你要查询哪个地方的天气信息呢，快把城市的名字发给我吧!");
                //context.Wait(MessageReceived);
            }
        }

        /// <summary>
        /// 获取Luis服务器上的回答数据
        /// </summary>
        /// <param name="askString"></param>
        /// <returns></returns>
        private async Task<string> GetLuisResult(string askString)
        {
            string url = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/de176698-a85b-4e2c-9e7d-58d3d690e11f?subscription-key=1fee7c75cfa74d99a9aabd81e2d40a59&verbose=true&timezoneOffset=0&q=";
            string result = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url + askString);
                    if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                    }
                }
                catch (HttpRequestException ex)
                {
                    //error
                }
            }
            return result;
        }

        private string GetLuisValue(string jsonString)
        {
            string valueString = null;

            return valueString;
        }

    }
}