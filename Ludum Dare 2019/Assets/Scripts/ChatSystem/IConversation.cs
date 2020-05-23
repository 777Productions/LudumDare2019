using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ChatSystem
{
    public interface IConversation
    {
        ChatPriority Priority { get; }
        int Position { get; }
        bool WasInterrupted { get; }

        event EventHandler ConversationStarted;
        event EventHandler ConversationFinished;
        event EventHandler ConversationInterrupted;
        event EventHandler ConversationResumed;

        void OnStarted();
        void OnFinished();
        void OnInterrupted();
        void OnResumed();

        IDialog GetNextDialog();
    }
}
