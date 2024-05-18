using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace MonoScripts.Menus
{
    public class TimerUI : MonoBehaviour

    {
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private string format;

        public float ElapsedTime { get; private set; }

        public float TotalSeconds => ElapsedTime / 1000;
        public float TotalMinutes => TotalSeconds / 60;
        
        public int Milliseconds => (int)(ElapsedTime % 1000);
        public int Seconds => (int)(TotalSeconds % 60);
        public int Minutes => (int)(TotalMinutes % 60);

        void Update()
        {
            ElapsedTime += Time.deltaTime * 1000;
            tmpText.text = string.Format(format, Minutes, Seconds, Milliseconds);
        }

        public void Reset()
        {
            ElapsedTime = 0;
        }
    }
}