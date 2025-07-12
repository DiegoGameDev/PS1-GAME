using UnityEngine;
using Player;
using System;
using Player.Inventory;
using UI;
using DialogueSystem;
using Itens;

namespace Single
{
    public class Game : MonoBehaviour
    {
        //singles
        public static Game main;
        public GameInput gameInput;
        public GameMessage gameMessage;

        //UI
        public ControllerSlots controllerSlots;

        //Game
        public Camera mainCam;
        public PlayerController Player;
        public DialogManager dialogManager;
        //scenes
        public CassetePlayer cassetePlayer;

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
            gameData = SaveManager.LoadData();

            if (gameData == null)
            {
                gameData = new();
                SaveManager.SaveData(gameData);
            }

            Player.inventory.inventoryObject = gameData.inventoryObject;

            mainCam = FindFirstObjectByType<Camera>();
            Player = FindFirstObjectByType<PlayerController>();
            dialogManager = FindFirstObjectByType<DialogManager>();
            cassetePlayer = FindFirstObjectByType<CassetePlayer>();
            gameInput = GetComponent<GameInput>();

            QuitAndSave += Player.inventory.SaveInventory;

            //gameData.inventoryObject = slotItemSO.inventoryObject;
        }

        private void OnApplicationQuit()
        {
            QuitAndSave?.Invoke();
        }

        public void SaveInventory(InventoryObject inventory)
        {
            gameData.inventoryObject = inventory;
            SaveManager.SaveData(gameData);
        }
        
        public InventoryObject LoadInventory()
        {
            return gameData.inventoryObject;
        }
    }

    [System.Serializable]
    public class GameData
    {
        public InventoryObject inventoryObject;

        public GameData()
        {
            inventoryObject = new InventoryObject();
        }
    }
}