using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.ChatSystem.Depreciated
{
    public class ChatInterface : MonoBehaviour
    {
        public float CharactersPerMinute = 1000;
        //public float HoldFor = 1;

        private ChatText chatText;
        private ChatPortrait[] portraits;
        private bool isSpeaking = false;

        public bool InConversation
        {
            get
            {
                return chatQueue.Count > 0;
            }
        }

        private Queue<ChatMessage> chatQueue = new Queue<ChatMessage>();

        private List<ChatMessage> chatHistory = new List<ChatMessage>();

        // Start is called before the first frame update
        void Start()
        {
            chatText = GetComponentInChildren<ChatText>();
            portraits = GetComponentsInChildren<ChatPortrait>();

            chatText.SetVisibility(false);

            foreach (var portrait in portraits)
            {
                portrait.SetVisibility(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!isSpeaking && chatQueue.Count > 0)
            {
                ProcessNextMessage(chatQueue.Dequeue());
            }
        }

        public void PushMessage(string text, Character playerSpeaking, float postDelay = 1.0f, Action callback = null)
        {
            var message = new ChatMessage
            {
                Text = text,
                PlayerSpeaking = playerSpeaking,
                PostDelay = postDelay,
                Callback = callback
            };

            chatQueue.Enqueue(message);
        }

        public void ProcessNextMessage(ChatMessage message)
        {
            StartCoroutine("SpeakTask", message);
            chatHistory.Add(message);
        }

        private IEnumerator SpeakTask(ChatMessage message)
        {
            // Wait for any current speech to finish.
            while (isSpeaking)
            {
                yield return null;
            }

            isSpeaking = true;
            var speakingCharacter = portraits.SingleOrDefault(p => p.Player == message.PlayerSpeaking);

            var listeningCharacters = portraits.Where(p => p.Player != message.PlayerSpeaking);

            foreach (var character in listeningCharacters)
            {
                character.SetListener();
            }

            if (speakingCharacter != null)
            {
                speakingCharacter.SetSpeaker();
            }

            chatText.ClearText();
            chatText.gameObject.SetActive(true);
            chatText.SetText(message.Text, CharactersPerMinute);

            while (chatText.IsDisplaying)
            {
                yield return null;
            }

            yield return new WaitForSeconds(message.PostDelay);

            if (chatQueue.Count == 0)
            {
                foreach (var character in portraits)
                {
                    character.SetVisibility(false);
                }

                chatText.SetVisibility(false);
            }

            isSpeaking = false;

            message.Callback?.Invoke();

            yield return null;
        }
    }

    public class ChatMessage
    {
        public string Text;
        public Character PlayerSpeaking;
        public float PostDelay;

        public Action Callback = null;
    }

}