using Interactions;
using Single;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Inventory
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "STNGAME/DATA/ITEM DATA")]
    public class ItemData : ScriptableObject
    {
        public List<ItemBehaviour> itemBehaviour = new List<ItemBehaviour>();

        public Dictionary<ItemSO, ItemBehaviour> ItemObjectBySO;
        private Dictionary<int, ItemSO> itemByID;

        public Dictionary<int, ItemBehaviour> ItemObjectByID;

        public ItemBehaviour ItemBehaviour(ItemSO item)
        {
            ItemBehaviour obj;
            if (ItemObjectBySO.TryGetValue(item, out obj))
            {
                return obj;
            }

            return null;
        }

        public ItemBehaviour ItemBehaviour(int ID)
        {
            ItemBehaviour obj;
            if (ItemObjectByID.TryGetValue(ID, out obj))
            {
                return obj;
            }

            return null;
        }

        private void Awake()
        {
            ItemObjectBySO = new Dictionary<ItemSO, ItemBehaviour>();
            itemByID = new Dictionary<int, ItemSO>();
            ItemObjectByID = new Dictionary<int, ItemBehaviour>();

            for (int i = 0; i < itemBehaviour.Count; i++)
            {
                if (itemBehaviour.Count < 1)
                    return;

                ItemObjectBySO.Add(itemBehaviour[i].item, itemBehaviour[i]);
                itemBehaviour[i].item.ID = i + 1000;
                itemByID.Add(itemBehaviour[i].item.ID, itemBehaviour[i].item);
                ItemObjectByID.Add(itemBehaviour[i].item.ID, itemBehaviour[i]);
            }
        }

        private void OnValidate()
        {
            ItemObjectBySO = new Dictionary<ItemSO, ItemBehaviour>();
            itemByID = new Dictionary<int, ItemSO>();
            ItemObjectByID = new Dictionary<int, ItemBehaviour>();

            for (int i = 0; i < itemBehaviour.Count; i++)
            {
                if (itemBehaviour.Count < 1)
                    return;

                ItemObjectBySO.Add(itemBehaviour[i].item, itemBehaviour[i]);
                itemBehaviour[i].item.ID = i + 1000;
                itemByID.Add(itemBehaviour[i].item.ID, itemBehaviour[i].item);
                ItemObjectByID.Add(itemBehaviour[i].item.ID, itemBehaviour[i]);
            }
        }
    }
}