using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ChatSystem
{
    public class Conversation : IConversation
    {
        public ChatPriority Priority { get => throw new System.NotImplementedException(); }

        public int Position { get; private set; } = -1;

        public bool WasInterrupted { get; private set; } = false;

        private IList<IDialog> Dialog;

        public event EventHandler ConversationStarted;
        public event EventHandler ConversationFinished;
        public event EventHandler ConversationInterrupted;
        public event EventHandler ConversationResumed;

        public void OnStarted()
        {
            ConversationStarted?.Invoke(this, EventArgs.Empty);
        }

        public void OnFinished()
        {
            ConversationFinished?.Invoke(this, EventArgs.Empty);
        }

        public void OnInterrupted()
        {
            WasInterrupted = true;
            ConversationInterrupted?.Invoke(this, EventArgs.Empty);
        }

        public void OnResumed()
        {
            ConversationResumed?.Invoke(this, EventArgs.Empty);
        }

        public IDialog GetNextDialog()
        {
            Position++;
            if (Position >= Dialog.Count)
            {
                return null;
            }
            
            return Dialog[Position];
        }
    }
}
