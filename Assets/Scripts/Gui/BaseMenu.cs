using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gui
{
    public abstract class BaseMenu : MonoBehaviour
    {
        private const int Height = 30;
        private readonly int _top = Screen.height/2 - (Height/2);
        public GUISkin Skin;

        protected void ShowTwoButton(string topButtonText, Action topButtonCallback, string bottomButtonText,
            Action bottomButtonCallback)
        {
            var width = GetWidth(topButtonText, bottomButtonText);
            var left = Screen.width/2 - (width/2);

            GUI.skin = Skin;
            if (GUI.Button(new Rect(left, _top - 20, width, Height), topButtonText))
                topButtonCallback();

            if (GUI.Button(new Rect(left, _top + 20, width, Height), bottomButtonText))
                bottomButtonCallback();
        }

        protected void ShowSingleButton(string topButtonText, Action topButtonCallback)
        {
            var width = GetWidth(topButtonText);
            var left = Screen.width/2 - (width/2);

            if (GUI.Button(new Rect(left, _top, width, Height), topButtonText))
                topButtonCallback();
        }

        private static int GetWidth(params string[] texts)
        {
            return texts.Max(t => t.Length)*15;
        }
    }
}