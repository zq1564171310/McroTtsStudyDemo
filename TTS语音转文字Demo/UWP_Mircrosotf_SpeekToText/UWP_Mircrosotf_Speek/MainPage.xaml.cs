using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.Media.SpeechRecognition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace UWP_Mircrosotf_Speek
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SpeechRecognizerUnit _SpeechRecognizerUnit;

        public MainPage()
        {
            this.InitializeComponent();
            _SpeechRecognizerUnit = new SpeechRecognizerUnit();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _SpeechRecognizerUnit.StartSpeechRecognizerService();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _SpeechRecognizerUnit.StopSpeechRecognizerService();
        }


        private void RecognizeWithUIDictationGrammar_Click(object sender, RoutedEventArgs e)
        {
            _SpeechRecognizerUnit.SpeechRecognizerSpeechToText();
        }
    }
}
