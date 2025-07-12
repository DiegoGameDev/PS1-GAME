using UnityEngine;

namespace Player.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "STNGAME/INVENTORY/ITEM/KEY")]
    public class Key : ItemSO
    {
        private void Awake()
        {
            typeItem = TypeItem.Key;
        }
    }
}