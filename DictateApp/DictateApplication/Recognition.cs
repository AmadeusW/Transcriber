using DictateApplication.Properties;
using Microsoft.ProjectOxford.SpeechRecognition;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictateApplication
{
    public class Recognition : PropertyChangedBase
    {
        private MicrophoneRecognitionClient _micClient;
        string _recoLanguage = "en-us";

        public Recognition()
        {
            _recognitions = new ObservableCollection<RecognitionResult>();
            _recognitions.CollectionChanged += (s, e) => NotifyPropertyChanged(nameof(Results));
        }

        public List<string> Results
        {
            get
            {
                List<string> results = Recognitions.Select(n =>
                {
                    RecognizedPhrase x;
                    if (_desiredQuality == 1)
                        x = n.Results.OrderByDescending(r => r.Confidence).FirstOrDefault();
                    else
                        x = n.Results.OrderBy(r => r.Confidence).FirstOrDefault();

                    return x.DisplayText + n.RecognitionStatus.ToString();
                }).ToList();

                results.Add(CurrentRecognition);
                return results;
            }
        }

        private string _currentRecognition;
        public string CurrentRecognition
        {
            get
            {
                return _currentRecognition;
            }
            set
            {
                if (_currentRecognition != value)
                {
                    _currentRecognition = value;
                    NotifyPropertyChanged(nameof(CurrentRecognition));
                    NotifyPropertyChanged(nameof(Results));
                }
            }
        }

        private ObservableCollection<RecognitionResult> _recognitions;
        public ObservableCollection<RecognitionResult> Recognitions
        {
            get
            {
                return _recognitions;
            }
            set
            {
                if (_recognitions != value)
                {
                    _recognitions = value;
                    NotifyPropertyChanged(nameof(Results));
                }
            }
        }

        private int _desiredQuality;
        public int DesiredQuality
        {
            get
            {
                return _desiredQuality;
            }
            set
            {
                if (_desiredQuality != value)
                {
                    _desiredQuality = value;
                    NotifyPropertyChanged(nameof(Results));
                }
            }
        }

        internal void Start()
        {
            if (_micClient == null)
            {
                var key = Resources.ResourceManager.GetString("SpeechAPIKey");
                _micClient = CreateMicrophoneRecoClient(SpeechRecognitionMode.LongDictation, _recoLanguage, key);
            }
            _micClient.StartMicAndRecognition();
        }

        internal void Stop()
        {
            _micClient?.EndMicAndRecognition();
        }

        private MicrophoneRecognitionClient CreateMicrophoneRecoClient(SpeechRecognitionMode recoMode, string language, string subscriptionKey)
        {
            MicrophoneRecognitionClient micClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(
                recoMode,
                language,
                subscriptionKey);

            // Event handlers for speech recognition results
            micClient.OnPartialResponseReceived += OnPartialResponseReceivedHandler;
            micClient.OnResponseReceived += OnMicDictationResponseReceivedHandler;
            micClient.OnConversationError += OnConversationErrorHandler;

            return micClient;
        }

        private void OnConversationErrorHandler(object sender, SpeechErrorEventArgs e)
        {
            CurrentRecognition = e.SpeechErrorText;
        }

        private void OnMicDictationResponseReceivedHandler(object sender, SpeechResponseEventArgs e)
        {
            if (e.PhraseResponse.RecognitionStatus == RecognitionStatus.EndOfDictation
                || e.PhraseResponse.RecognitionStatus == RecognitionStatus.DictationEndSilenceTimeout
                || e.PhraseResponse.RecognitionStatus == RecognitionStatus.Intermediate
                || e.PhraseResponse.RecognitionStatus == RecognitionStatus.RecognitionSuccess
                || e.PhraseResponse.RecognitionStatus == RecognitionStatus.RecognitionError)
            {
                CurrentRecognition = String.Empty;
                _recognitions.Add(e.PhraseResponse);
            }
        }

        private void OnPartialResponseReceivedHandler(object sender, PartialSpeechResponseEventArgs e)
        {
            CurrentRecognition = e.PartialResult;
        }
    }
}
