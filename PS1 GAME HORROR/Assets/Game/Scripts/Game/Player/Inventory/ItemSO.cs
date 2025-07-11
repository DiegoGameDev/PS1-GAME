﻿using UnityEngine;

namespace Player.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "STNGAME/INVENTORY/ITEM/ITEM")]
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
        [field: SerializeField]
        public int QuantityMax { get; protected set; } = 1;
    }
    public enum TypeItem { Default, Papers, Key, Clips}
}