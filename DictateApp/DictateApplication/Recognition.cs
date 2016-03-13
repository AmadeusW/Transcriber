using Microsoft.ProjectOxford.SpeechRecognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictateApplication
{
    class Recognition
    {
        string _recoLanguage = "en-us";
        string _key = 

        internal void Start()
        {
            if (_micClient == null)
            {
                _micClient = CreateMicrophoneRecoClient(SpeechRecognitionMode.LongDictation, _recoLanguage, SubscriptionKey);
            }
            _micClient.StartMicAndRecognition();
        }

        internal void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
