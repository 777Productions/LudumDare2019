using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.ChatSystem
{
    public class ChatBox : MonoBehaviour
    {
        public event EventHandler DialogFinished;
        public event EventHandler DialogInterrupted;

        private bool interrupted = false;

        private Text text;

        protected virtual void OnDialogStarted(IDialog dialog)
        {
            dialog.OnStart();
        }

        protected virtual void OnDialogFinished(IDialog dialog)
        {
            dialog.OnFinish();
            DialogFinished?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnDialogInterrupted(IDialog dialog)
        {
            interrupted = false;
            DialogInterrupted?.Invoke(this, EventArgs.Empty);
        }

        public void Interrupt()
        {
            interrupted = true;
        }

        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public void DisplayDialog(IDialog dialog)
        {
            StartCoroutine("Run", dialog);
        }

        private IEnumerator Run(IDialog dialog)
        {
            OnDialogStarted(dialog);

            text.text = string.Empty;
            
            foreach (var character in dialog.Text)
            {
                if (interrupted) { break; }

                text.text += character;
                yield return new WaitForSeconds(60 / dialog.CPM);
            }

            if (!interrupted)
            {
                OnDialogFinished(dialog);
            }
            else
            {
                OnDialogInterrupted(dialog);
            }

            yield return null;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        private void Awake()
        {
            text = GetComponentInChildren<Text>();
        }
    }
}
