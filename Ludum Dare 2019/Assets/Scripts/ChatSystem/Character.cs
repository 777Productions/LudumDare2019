using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.ChatSystem
{
    public class Character : MonoBehaviour
    {
        public CharacterNames CharacterName;

        private Image playerPortrait;

        private void Awake()
        {
            playerPortrait = GetComponentInChildren<Image>();
        }

        public void SetTalking(bool isTalking)
        {
            SetAlpha(isTalking ? 1.0f : 0.5f);
        }

        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }

        private void SetAlpha(float alpha)
        {
            var color = playerPortrait.color;
            color.a = alpha;
            playerPortrait.color = color;
        }
    }
}
