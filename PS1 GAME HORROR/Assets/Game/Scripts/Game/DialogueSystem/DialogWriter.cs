using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem
{
    public sealed class DialogWriter : MonoBehaviour
    {
        [Header("Writer Components")]
        [SerializeField] UnityEvent @event;
        [SerializeField] TextMeshProUGUI DialogText;
        [SerializeField] TextMeshProUGUI DialogProfileText;
        [Space]

        [SerializeField] float delay;
        [SerializeField] float timeForSkip = 2;

        string text;

        public Action finishiSpeech;

        public void StartWriter(string dialogs, string profileText)
        {
            text = dialogs;
            DialogProfileText.text = profileText;
            StartCoroutine(Speech());
        }

        public void Skip()
        {
            StopCoroutine(Speech());
            DialogText.text = text;
            finishiSpeech?.Invoke();
        }

        public void End()
        {
            DialogText.text = string.Empty;
            DialogProfileText.text = string.Empty;
            @event?.Invoke();
        }

        IEnumerator Speech()
        {
            DialogText.text = "";
            foreach (var i in text.ToCharArray())
            {
                DialogText.text += i;

                yield return new WaitForSeconds(delay);
            }

            finishiSpeech?.Invoke();
        }
    }
}