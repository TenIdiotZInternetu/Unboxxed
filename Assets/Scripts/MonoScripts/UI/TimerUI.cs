using System;
using TMPro;
using UnityEngine;

namespace MonoScripts.Menus
{
    [RequireComponent(typeof(TMP_Text))]
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private string format;

        public float ElapsedTime { get; private set; }

        public float TotalSeconds => ElapsedTime / 1000;
        public float TotalMinutes => TotalSeconds / 60;
        public float TotalHours => TotalMinutes / 60;
        
        public float Milliseconds => ElapsedTime % 1000;
        public float Seconds => TotalSeconds % 60;
        public float Minutes => TotalMinutes % 60;
        

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        void Update()
        {
            ElapsedTime += Time.deltaTime;
            _text.text = string.Format(format, TotalHours, Minutes, Seconds, Milliseconds);
        }

        public void Reset()
        {
            ElapsedTime = 0;
        }
    }
}