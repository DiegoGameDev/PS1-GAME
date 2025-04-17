using UnityEngine;

namespace Player.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "STNGAME/INVENTORY/ITEM")]
    public class ItemSO : ScriptableObject
    {
        [field: SerializeField]
        public string NameItem { get; protected set; }
        [field: SerializeField]
        public string Description { get; protected set; }
        public int ID { get; set; }
        [field: SerializeField]
        public Sprite spriteItem { get; protected set; }
        [field: SerializeField]
        public TypeItem typeItem { get; protected set; }
    }
    public enum TypeItem { Default, Papers, Key, Clips}
}