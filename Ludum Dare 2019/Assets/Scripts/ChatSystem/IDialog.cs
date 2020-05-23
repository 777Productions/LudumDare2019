using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ChatSystem
{
    public interface IDialog
    {
        string Text { get; }

        CharacterNames Character { get; }

        event EventHandler DialogStarted;
        event EventHandler DialogFinished;

        void OnStart();
        void OnFinish();

        float CPM { get; }
    }
}
