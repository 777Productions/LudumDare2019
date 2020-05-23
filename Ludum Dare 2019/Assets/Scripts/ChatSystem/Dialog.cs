using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ChatSystem
{
    public class Dialog : IDialog
    {
        public string Text { get; private set; }

        public CharacterNames Character { get; private set; }

        public float CPM { get; private set; }

        public event EventHandler DialogStarted;
        public event EventHandler DialogFinished;

        public void OnStart()
        {
            DialogStarted?.Invoke(this, EventArgs.Empty);
        }

        public void OnFinish()
        {
            DialogFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}
