using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Globalization;
using Windows.Media.SpeechRecognition;
using Windows.Storage;

namespace UWP_Mircrosotf_Speek
{
    public class SpeechRecognizerUnit
    {
        private static uint HResultPrivacyStatementDeclined = 0x80045509;

        private SpeechRecognizer speechRecognizer;
        private IAsyncOperation<SpeechRecognitionResult> recognitionOperation;
        private ResourceContext speechContext;
        private ResourceMap speechResourceMap;
        private static string LanguageStr = "en-US";

        /// <summary>
        /// 开启语音识别服务
        /// </summary>
        public async void StartSpeechRecognizerService()
        {
            bool permissionGained = await AudioCapturePermissions.RequestMicrophonePermission();
            if (permissionGained)
            {
                Language speechLanguage = new Language(LanguageStr);
                speechContext = ResourceContext.GetForCurrentView();
                speechContext.Languages = new string[] { speechLanguage.LanguageTag };

                speechResourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("LocalizationSpeechResources");

                if (speechRecognizer != null)
                {
                    this.speechRecognizer.Dispose();
                    this.speechRecognizer = null;
                }

                speechRecognizer = new SpeechRecognizer(speechLanguage);

                var dictationConstraint = new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.Dictation, "dictation");
                speechRecognizer.Constraints.Add(dictationConstraint);
                SpeechRecognitionCompilationResult compilationResult = await speechRecognizer.CompileConstraintsAsync();

                if (compilationResult.Status != SpeechRecognitionResultStatus.Success)
                {
                    WriteLog(compilationResult.Status.ToString(), 0);
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// 关闭语音识别服务
        /// </summary>
        public void StopSpeechRecognizerService()
        {
            if (speechRecognizer != null)
            {
                if (speechRecognizer.State != SpeechRecognizerState.Idle)
                {
                    if (recognitionOperation != null)
                    {
                        recognitionOperation.Cancel();
                        recognitionOperation = null;
                    }
                }

                this.speechRecognizer.Dispose();
                this.speechRecognizer = null;
            }
        }

        public async void SpeechRecognizerSpeechToText()
        {
            try
            {
                recognitionOperation = speechRecognizer.RecognizeAsync();
                SpeechRecognitionResult speechRecognitionResult = await recognitionOperation;

                if (speechRecognitionResult.Status == SpeechRecognitionResultStatus.Success)
                {
                    WriteLog(speechRecognitionResult.Text, 1);
                }
                else
                {
                    WriteLog(speechRecognitionResult.Status.ToString(), 2);
                }
            }
            catch (Exception exception)
            {

            }
        }

        private static void WriteLog(string log, int name)
        {

            string savePath = ApplicationData.Current.LocalFolder.Path + "/" + name + ".txt";  //本地保存路径

            using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
            {
                var buff = Encoding.Unicode.GetBytes(log);
                fs.Write(buff, 0, buff.Length);
            }
        }
    }
}
