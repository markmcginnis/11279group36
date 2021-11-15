using IBM.Watson.TextToSpeech.V1;
using IBM.Watson.TextToSpeech.V1.Model;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IBM.Cloud.SDK;
using UnityEngine.SceneManagement;


namespace IBM.Watson.Examples
{
    public class UserGuide : MonoBehaviour
    {
        [Space(10)]
        [Tooltip("The IAM apikey.")]
        [SerializeField]
        private string iamApikey = "LyTynKWgaW1Kbf_xjwXy-gekLQV7gMZRTSTEVp1jR0-q";
        [Tooltip("The service URL (optional). This defaults to \"https://api.us-south.text-to-speech.watson.cloud.ibm.com\"")]
        [SerializeField]
        private string serviceUrl;
        private TextToSpeechService service;
        public string textInput = "Welcome to the Fishery Manager Simulator! This simulator will help you determine optimal fishery practices for profits and sustainability that can help you run a successful fishery in real life. The factors you can change in the simulation are water filtering power, antibiotic concentration, quantity to harvest, and feeding amount. Water filtering power will impact the fecal matter levels in the water. Antibiotic concentration impacts the health of the fish. The harvesting amount impacts the population density. The feeding amount impacts fish health and size. Use the economic dashboard to see the current profits or losses based on your management style. Use the fishery tab to see the fishery statistics and graphs about the fishery trends. Thats all for now, good luck managing your fishery!";

        private void Start()
        {
            LogSystem.InstallDefaultReactors();
            Runnable.Run(CreateService());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Runnable.Run(ExampleSynthesize(textInput));
            }
        }
        
        private IEnumerator CreateService()
        {
            IamAuthenticator authenticator = new IamAuthenticator(apikey: "LyTynKWgaW1Kbf_xjwXy-gekLQV7gMZRTSTEVp1jR0-q");

            while (!authenticator.CanAuthenticate())
            {
                yield return null;
            }

            service = new TextToSpeechService(authenticator);
            if (!string.IsNullOrEmpty(serviceUrl))
            {
                service.SetServiceUrl(serviceUrl);
            }
        }

        #region Synthesize Example
        private IEnumerator ExampleSynthesize(string text)
        {
            byte[] synthesizeResponse = null;
            AudioClip clip = null;
            service.Synthesize(
                callback: (DetailedResponse<byte[]> response, IBMError error) =>
                {
                    synthesizeResponse = response.Result;
                    Log.Debug("ExampleTextToSpeechV1", "Synthesize done!");
                    clip = WaveFile.ParseWAV("myClip", synthesizeResponse);
                    PlayClip(clip);
                },
                text: textInput,
                voice: "en-US_AllisonV3Voice",
                accept: "audio/wav"
            );

            while (synthesizeResponse == null)
                yield return null;

            yield return new WaitForSeconds(clip.length);
        }
        #endregion

        #region PlayClip
        private void PlayClip(AudioClip clip)
        {
            if (Application.isPlaying && clip != null)
            {
                GameObject audioObject = new GameObject("AudioObject");
                AudioSource source = audioObject.AddComponent<AudioSource>();
                source.spatialBlend = 0.0f;
                source.loop = false;
                source.clip = clip;
                source.Play();

                GameObject.Destroy(audioObject, clip.length);
            }
        }
        #endregion
        public void returnToMenu()
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}