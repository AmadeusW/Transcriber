using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictateApp
{
    internal class Recognition
    {
        string _recoLanguage = "en-us";

        internal static void Start()
        {
            if (_micClient == null)
            {
                _micClient = CreateMicrophoneRecoClient(SpeechRecognitionMode.LongDictation, _recoLanguage, SubscriptionKey);
            }
            _micClient.StartMicAndRecognition();
        }

        internal static void Stop()
        {
            throw new NotImplementedException();
        }

    }
}
