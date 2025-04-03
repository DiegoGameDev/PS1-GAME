using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace UI
{
    public sealed class ControllerSlots : MonoBehaviour
    {
        [Header("Slots")]
        [SerializeField] GameObject panel;
        [SerializeField] List<Image> slots = new List<Image>(4);
        [SerializeField] List<Image> slotsParent = new List<Image>(4);

        [Space]
        [SerializeField] float timeShow;

        public void UpdateSlotsUI(List<Sprite> sprites)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                slots[i].sprite = sprites[i];
            }
        }

        public void CurrentSlot(int index)
        {
            StopCoroutine(ShowInventory());
            StartCoroutine(ShowInventory());

            for (int i = 0; i < slots.Count; i++)
            {
                if (i == index)
                {
                    if (slots[i].sprite != null)
                        slots[i].color = new Color(1, 1, 1, 1);
                    slotsParent[i].color = new Color(0, 0, 0, 0.8f);
                }
                else
                {
                    if (slots[i].sprite != null)
                        slots[i].color = new Color(1, 1, 1, 1f);
                    slotsParent[i].color = new Color(0, 0, 0, 0.3f);
                }
            }
        }

        IEnumerator ShowInventory()
        {
            float countTime = 0;
            panel.SetActive(true);

            while (countTime < timeShow)
            {
                countTime += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            panel.SetActive(false);
        }
    }
}