using Interactive;
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
            //playerHand = GetComponentInChildren<PlayerHand>();
            inventoryObject = new InventoryObject(0);

            if (inventoryObject.slots.Count > 0 && inventoryObject.slots != null)
            for (int i = 0; i < inventoryObject.slots.Count; i++)
            {
                if (inventoryObject.slots[i].item != null)
                {
                    playerHand.Additem(Game.main.itemData.ItemBehaviour(inventoryObject.slots[i].item));           
                }
            }

            VerifySlots();
        }

        private void VerifySlots()
        {
            if ((inventoryObject.slots.Count > 0 && inventoryObject.slots != null))
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

            if (playerHand.itensInHand.Count == playerHand.maxCapaxity)
                NullSlot = false;
        }

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
    }

    [System.Serializable]
    public struct InventoryObject
    {
        public List<SlotObject> slots;
        public SlotObject clips;
        public List<ItemSO> papers;

        public InventoryObject(int i)
        {
            slots = new List<SlotObject>();
            clips = new SlotObject();
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