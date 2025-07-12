using System.Collections;
using TMPro;
using UnityEngine;

namespace Single
{
    public sealed class GameMessage : MonoBehaviour
    {
        public enum TypeMessage { error, message, monodialog}
        [Header("Canvas Components")]
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] float timeInScreen = 2;

        bool TextInScreen;

        public void ShowMessage(string text, TypeMessage type)
        {
            TextInScreen = true;
            this.text.text = text;
            StartCoroutine(ShowText());
        }
        public void ShowMessage(string text, TypeMessage type, float time)
        {
            TextInScreen = true;
            this.text.text = text;
            StartCoroutine(ShowText());
            timeInScreen = time;
        }

        IEnumerator ShowText()
        {
            float countTime = timeInScreen;
            while (TextInScreen)
            {
                float value = Mathf.Lerp(1, 0, countTime);
                text.color = new Color(1, 1, 1, value);

                TextInScreen = countTime <= 0 ? false : true;

                countTime -= Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            text.text = string.Empty;
            timeInScreen = 0;
        }
    }
}