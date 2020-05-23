using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.ChatSystem
{

    public class ChatText : MonoBehaviour
    {
        private Text text;
        public bool IsDisplaying = false;
        private bool skip = false;

        //public Image Background { get; private set; }

        // Start is called before the first frame update
        void Awake()
        {
            text = GetComponentInChildren<Text>();
            //Background = GetComponentInChildren<Image>();
            text.text = string.Empty;
        }

        // Update is called once per frame
        void Update()
        {
            if (IsDisplaying && Input.GetKeyDown(KeyCode.Space))
            {
                skip = true;
            }
        }

        public void SetText(string text, float cpm)
        {
            StartCoroutine("DisplayText", new TextArgs { Text = text, CPM = cpm });
        }

        public void ClearText()
        {
            text.text = string.Empty;
        }

        private IEnumerator DisplayText(TextArgs args)
        {
            IsDisplaying = true;

            var words = args.Text.Split(' ');

            foreach (var character in args.Text)
            {
                if (skip)
                {
                    text.text = args.Text;
                    skip = false;
                    break;
                }

                text.text += character;

                float waitTime = 60 / args.CPM;
                if (character == '.')
                {
                    waitTime /= 2;
                }

                yield return new WaitForSeconds(waitTime);
            }

            IsDisplaying = false;
        }

        public void SetVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
    }

    public class TextArgs
    {
        public string Text;
        public float CPM;
    }
}
