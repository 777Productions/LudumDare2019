using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.ChatSystem
{
    public class ChatSystem : MonoBehaviour
    {
        public List<Character> characters;
        public ChatBox chatBox;

        private ConversationQueue conversationQueue = new ConversationQueue();

        private IConversation currentConversation = null;
        private IConversation interruptingConversation = null;

        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            if (chatBox != null)
            {
                chatBox.DialogFinished += ChatBox_DialogFinished;
                chatBox.DialogInterrupted += ChatBox_DialogInterrupted;
            }
            else
            {
                Debug.LogError("ChatBox is required for ChatSystem to work.");
            }
        }

        private void ChatBox_DialogInterrupted(object sender, EventArgs e)
        {
            currentConversation.OnInterrupted();
            conversationQueue.PushInterruptedConversation(currentConversation);

            StartConversation(interruptingConversation);
            interruptingConversation = null;
        }

        private void StartConversation(IConversation conversation)
        {
            SetChatVisibility(true);
            currentConversation = conversation;

            if (conversation.WasInterrupted)
            {
                conversation.OnResumed();
            }
            else
            {
                conversation.OnStarted();
            }

            PlayDialog(conversation.GetNextDialog());
        }

        private void PlayDialog(IDialog dialog)
        {
            SetTalkingCharacter(dialog.Character);
            chatBox.DisplayDialog(dialog);
        }

        private void SetTalkingCharacter(CharacterNames character)
        {
            characters.ForEach(a => a.SetTalking(a.CharacterName == character));
        }

        private void ChatBox_DialogFinished(object sender, EventArgs e)
        {
            var nextDialog = currentConversation.GetNextDialog();

            if (nextDialog != null)
            {
                PlayDialog(nextDialog);
            }
            else
            {
                currentConversation.OnFinished();
                currentConversation = null;

                var nextConversation = conversationQueue.GetNextConversation();

                if (nextConversation != null)
                {
                    StartConversation(nextConversation);
                }
                else
                {
                    SetChatVisibility(false);
                }
            }
        }

        private void SetChatVisibility(bool visible)
        {
            characters.ForEach(a => a.SetVisibility(visible));
            chatBox.SetVisibility(visible);
        }

        /// <summary>
        /// Adds a conversation to the queue. (Useful for low priority conversations which aren't context specific).
        /// </summary>
        /// <param name="conversation"></param>
        public void QueueConversation(IConversation conversation)
        {
            if (currentConversation == null)
            {
                StartConversation(conversation);
            }
            else
            {
                conversationQueue.PushConversation(conversation);
            }
        }

        /// <summary>
        /// Plays a conversation immediately, interrupting any which are currently playing. (Useful for situational conversations).
        /// </summary>
        /// <param name="conversation"></param>
        public void PlayConversationNow(IConversation conversation)
        {
            if (currentConversation == null)
            {
                StartConversation(conversation);
            }
            else
            {
                interruptingConversation = conversation;

                chatBox.Interrupt();
            }
        }
    }
}
