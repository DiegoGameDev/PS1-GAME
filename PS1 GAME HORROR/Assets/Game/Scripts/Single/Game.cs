using UnityEngine;
using Player;
using System;
using Player.Inventory;

namespace Single
{
    public class Game : MonoBehaviour
    {
        //singles
        public static Game main;
        public GameInput gameInput;

        //Game
        public Camera mainCam;
        public PlayerController Player;

        //Data
        public ItemData itemData;
        public SlotItemSO slotItemSO;

        public GameData gameData;

        //Events
        public Action QuitAndSave;
        public Action QuitWithoutSave;

        private void Awake()
        {
            main = this;
            mainCam = FindFirstObjectByType<Camera>();
            Player = FindFirstObjectByType<PlayerController>();
            gameInput = GetComponent<GameInput>();
            gameData.inventoryObject = slotItemSO.inventoryObject;
        }

        private void OnApplicationQuit()
        {
            QuitAndSave?.Invoke();
        }
    }

    [System.Serializable]
    public struct GameData
    {
        public InventoryObject inventoryObject;
    }
}