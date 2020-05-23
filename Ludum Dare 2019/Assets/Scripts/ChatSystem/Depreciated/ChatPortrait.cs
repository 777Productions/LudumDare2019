using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.ChatSystem.Depreciated
{
    public class ChatPortrait : MonoBehaviour
    {
        public Character Player;

        public Image Avatar { get; private set; }

        // Start is called before the first frame update
        void Awake()
        {
            Avatar = GetComponentInChildren<Image>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }

        public void SetSpeaker()
        {
            SetAlpha(1.0f);
            SetVisibility(true);
        }

        public void SetListener()
        {
            SetAlpha(0.5f);
            SetVisibility(true);
        }

        private void SetAlpha(float alpha)
        {
            var color = Avatar.color;
            color.a = alpha;
            Avatar.color = color;
        }
    }
}