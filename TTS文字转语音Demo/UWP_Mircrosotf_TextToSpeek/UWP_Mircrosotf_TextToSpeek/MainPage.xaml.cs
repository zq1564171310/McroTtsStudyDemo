using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace UWP_Mircrosotf_TextToSpeek
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SpeechSynthesizer synthesizer;
        private ResourceContext speechContext;
        private ResourceMap speechResourceMap;
        //private static string LanguageStr = "en-US";
        private static string LanguageStr = "zh-CN";

        public MainPage()
        {
            this.InitializeComponent();
            synthesizer = new SpeechSynthesizer();

            speechContext = ResourceContext.GetForCurrentView();

            Language speechLanguage = new Language(LanguageStr);
            speechContext.Languages = new string[] { speechLanguage.LanguageTag };

            speechResourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("LocalizationTTSResources");

            Speak_Click();
        }

        private async void Speak_Click()
        {
            //string text = "C# is an object-oriented, advanced programming language running on.NET Framework issued by Microsoft Corp. And is scheduled to appear on the Microsoft career developers Forum (PDC). C# is the latest achievement of Anders Hejlsberg, a Microsoft Corp researcher. C# looks surprisingly similar to Java; it includes a process such as single inheritance, interfaces, almost the same syntax with Java, and compiled into intermediate code to run again. But C# is quite different from Java, and it draws on one of the features of Delphi, which is directly integrated with the COM (component object model), and it is the leading role of the Microsoft Corp.NET windows network framework ";

            string text = "C#是微软公司发布的一种面向对象的、运行于.NET Framework之上的高级程序设计语言。并定于在微软职业开发者论坛(PDC)上登台亮相。C#是微软公司研究员Anders Hejlsberg的最新成果。C#看起来与Java有着惊人的相似；它包括了诸如单一继承、接口、与Java几乎同样的语法和编译成中间代码再运行的过程。但是C#与Java有着明显的不同，它借鉴了Delphi的一个特点，与COM（组件对象模型）是直接集成的，而且它是微软公司 .NET windows网络框架的主角";

            if (!String.IsNullOrEmpty(text))
            {
                try
                {
                    SpeechSynthesisStream synthesisStream = await synthesizer.SynthesizeTextToStreamAsync(text);
                    media.AutoPlay = true;
                    media.SetSource(synthesisStream, synthesisStream.ContentType);
                    media.Play();
                }
                catch (Exception)
                {

                }
            }
        }
    }

}
