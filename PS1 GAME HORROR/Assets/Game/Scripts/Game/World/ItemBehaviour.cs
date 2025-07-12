using Player;
using Player.Inventory;
using Single;
using UnityEngine;

namespace Interactions
{
    public abstract class ItemBehaviour : Interactive
    {
        [Header("Settings Object")]
        public EstateItemBehaviour estateItemBehaviour;
        public ItemSO item;

        protected Collider coll;

        [Space]
        public TransformItemInHand TransformItemInHand = new TransformItemInHand();

        private void Awake()
        {
            coll = GetComponent<Collider>();
            materialBase = new System.Collections.Generic.List<UnityEngine.Material>();
            foreach (var i in mesh.materials)
            {
                materialBase.Add(i);
            }
            SetEstate(EstateItemBehaviour.Scene);
        }

        private void OnEnable()
        {
            //prefab = Game.main.itemData.ItemBehaviour(item);
        }

        public void DestroyColliders()
        {
            if (coll != null)
                Destroy(coll);
        }
        public void ActiveColliders() => coll.enabled = true;

        public void SetEstate(EstateItemBehaviour estate) => estateItemBehaviour = estate;

        public virtual void Use(PlayerController player)
        {

        }

        public virtual void DropItem(PlayerController player)
        {

        }

        public virtual void InteractItem()
        {

        }
    }
    public enum EstateItemBehaviour { Hand, Scene}
    [System.Serializable]
    public struct TransformItemInHand
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
    }
}