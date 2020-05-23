using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ChatSystem
{
    public class ConversationQueue
    {
        private SortedList<ChatPriority, IConversation> pendingConversations = new SortedList<ChatPriority, IConversation>(new ChatPriorityComparer());

        private SortedList<ChatPriority, IConversation> interruptedConversations = new SortedList<ChatPriority, IConversation>(new ChatPriorityComparer());

        public IConversation GetNextConversation()
        {
            IConversation conversation = null;

            if (pendingConversations.Count > 0)
            {
                conversation = pendingConversations[pendingConversations.Keys[0]];
            }

            if (interruptedConversations.Count > 0)
            {
                var interruptedConversation = interruptedConversations[interruptedConversations.Keys[0]];

                var comparer = new ChatPriorityComparer();

                if (conversation == null || comparer.Compare(conversation.Priority, interruptedConversation.Priority) <= 0)
                {
                    return interruptedConversation;
                }
            }

            return conversation;
        }

        public void PushConversation(IConversation conversation)
        {
            pendingConversations.Add(conversation.Priority, conversation);
        }

        public void PushInterruptedConversation(IConversation conversation)
        {
            interruptedConversations.Add(conversation.Priority, conversation);
        }
    }

    internal class ChatPriorityComparer : IComparer<ChatPriority>
    {
        public int Compare(ChatPriority x, ChatPriority y)
        {
            return x < y ? 1 : x > y? -1 : 0;
        }
    }
}
