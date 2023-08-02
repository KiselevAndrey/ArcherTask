using CodeBase.Infrastructure;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    [Serializable]
    public class CountdownBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField, Range(1, 5)] private int _time;

        public void Start(ICoroutineRunner runner, Action onCountdownEnded)
        {
            runner.StartCoroutine(Run(onCountdownEnded));
        }

        private IEnumerator Run(Action onCountdownEnded)
        {
            WaitForSecondsRealtime second = new(1);
            var timeLeft = _time;
            while(timeLeft > 0)
            {
                _text.text = timeLeft.ToString();
                yield return second;
                timeLeft--;
            }

            _text.text = "";
            onCountdownEnded?.Invoke();
        }
    }
}