using UnityEngine;

namespace Player.Inventory
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "STNGAME/INVENTORY/SLOT")]
    [System.Serializable]
    public class SlotItemSO : ScriptableObject
    {
        public InventoryObject inventoryObject;
    }
}