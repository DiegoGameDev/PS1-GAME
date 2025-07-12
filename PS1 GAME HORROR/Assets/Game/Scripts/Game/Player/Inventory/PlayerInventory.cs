using Interactions;
using Single;
using UnityEngine;
using System.Collections.Generic;

namespace Player.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        public InventoryObject inventoryObject = new InventoryObject();
        [field : SerializeField]
        public PlayerHand playerHand { get; private set; }
        public bool NullSlot { get; private set; }


        private void Awake()
        {
            if (inventoryObject == null)
                inventoryObject = new InventoryObject();

            if (inventoryObject.slots != null)
            {    
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
            if (inventoryObject.slots != null)
            {
                for (int i = 0; i < inventoryObject.slots.Count; i++)
                {
                    if (inventoryObject.slots[i].item == item)
                    {
                        if (inventoryObject.slots[i].quantity < item.item.QuantityMax)
                        {
                            inventoryObject.slots[i].AddQuantity(1);
                        }

                        Destroy(item.gameObject);
                        VerifySlots();
                        return;
                    }
                }
            }
            else
            {
                inventoryObject.slots = new List<SlotObject>();
            }

            inventoryObject.slots.Add(new SlotObject(item.item, 1));
            playerHand.Additem(item);
            VerifySlots();
        }

        public void RemoveItem(ItemBehaviour item)
        {
            playerHand.RemoveItem(item);
            VerifySlots();
        }

        public void AddKeys(Key key)
        {
            if (inventoryObject.keys == null)
                inventoryObject.keys = new List<Key>();
            inventoryObject.keys.Add(key);
        }

        public bool HaveKey(Key key)
        {
            if (inventoryObject.keys == null)
            {
                inventoryObject.keys = new List<Key>();
                return false;
            }
                

            for (int i = 0; i < inventoryObject.keys.Count; i++)
            {
                if (inventoryObject.keys[i] == key)
                    return true;
            }

            return false;
        }
    }

    [System.Serializable]
    public class InventoryObject
    {
        public List<SlotObject> slots;
        public SlotObject clips;
        public List<Key> keys;
        public List<ItemSO> papers;

        public InventoryObject()
        {
            slots = new List<SlotObject>();
            clips = new SlotObject();
            keys = new List<Key>();
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

        public int AddQuantity(int quantity)
        {
            this.quantity += quantity;

            if (this.quantity >= item.QuantityMax)
                this.quantity = item.QuantityMax;

            return this.quantity;
        }
    }
}