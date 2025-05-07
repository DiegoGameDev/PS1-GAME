using Interactions;
using Single;
using UnityEngine;
using System.Collections.Generic;

namespace Player.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        public InventoryObject inventoryObject { get; private set; }
        [field : SerializeField]
        public PlayerHand playerHand { get; private set; }
        public bool NullSlot { get; private set; }


        private void Awake()
        {
            inventoryObject = Game.main.slotItemSO.inventoryObject;

            if (inventoryObject.slots != null)
            {
                inventoryObject = new InventoryObject();
                if (inventoryObject.slots.Count > 0)
                    for (int i = 0; i < inventoryObject.slots.Count; i++)
                    {
                        if (inventoryObject.slots[i].item != null)
                        {
                            playerHand.Additem(Game.main.itemData.ItemBehaviour(inventoryObject.slots[i].item));
                        }
                    }
            }

            VerifySlots();
        }

        private void VerifySlots()
        {
            if (inventoryObject.slots != null)
            {
                if (inventoryObject.slots.Count > 0)
                    for (int i = 0; i < inventoryObject.slots.Count; i++)
                    {
                        if (inventoryObject.slots[i].item == null)
                        {
                            NullSlot = true;
                            return;
                        }
                    }
                else
                    NullSlot = true;
            }
                NullSlot = true;

            if (playerHand.itensInHand.Count == playerHand.maxCapaxity)
                NullSlot = false;
        }

        public void SaveInventory() => Game.main.SaveInventory(inventoryObject);

        public void AddItem(ItemBehaviour item)
        {
            inventoryObject.slots.Add(new SlotObject(item.item, 1));
            playerHand.Additem(item);
            VerifySlots();
        }

        public void RemoveItem(ItemBehaviour item)
        {
            playerHand.RemoveItem(item);
            VerifySlots();
        }

        public bool HaveKey(ItemSO key)
        {
            for (int i = 0; i < inventoryObject.keys.Count; i++)
            {
                if (inventoryObject.keys[i] == key)
                    return true;
            }

            return false;
        }
    }

    [System.Serializable]
    public struct InventoryObject
    {
        public List<SlotObject> slots;
        public SlotObject clips;
        public List<ItemSO> keys;
        public List<ItemSO> papers;

        public InventoryObject(int i)
        {
            slots = new List<SlotObject>();
            clips = new SlotObject();
            keys = new List<ItemSO>();
            papers = new();
        }
    }

    [System.Serializable]
    public struct SlotObject
    {
        public ItemSO item;
        public int quantity;

        public SlotObject(ItemSO item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }
}