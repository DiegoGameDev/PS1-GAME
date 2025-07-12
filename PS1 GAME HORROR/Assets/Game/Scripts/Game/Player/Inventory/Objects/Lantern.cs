using Interactions;
using Player;
using UnityEngine;

namespace Itens
{
    public sealed class Lantern : ItemBehaviour
    {
        [Header("Lantern")]
        [SerializeField] Light lightComponent;

        public int energy;

        bool turnOn = true;

        public override void Use(PlayerController player)
        {
        }

        public override void InteractItem()
        {
            turnOn = !turnOn;

            lightComponent.gameObject.SetActive(turnOn);
        }

        public override void Interact(PlayerController player)
        {
            if (estateItemBehaviour == EstateItemBehaviour.Hand)
                return;

            if (!player.inventory.NullSlot)
                return;

            coll.enabled = false;
            //prefab.SetEstate(EstateItemBehaviour.Hand);

            transform.position = prefab.transform.position;
            transform.rotation = prefab.transform.rotation;

            DestroyColliders();
            SetEstate(EstateItemBehaviour.Hand);
            Looking(false);
            //prefab.Looking(false);
            player.inventory.AddItem(this);
            //Destroy(gameObject);
        }
    }
}
