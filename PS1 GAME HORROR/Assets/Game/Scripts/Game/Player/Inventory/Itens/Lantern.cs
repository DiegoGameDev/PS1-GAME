using Interactive;
using Player;
using UnityEngine;

namespace Itens
{
    public sealed class Lantern : ItemBehaviour
    {
        [Header("Lantern")]
        [SerializeField] Light lightComponent;
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

            DestroyColliders();
            prefab.SetEstate(EstateItemBehaviour.Hand);
            //prefab.Looking(false);
            player.inventory.AddItem(prefab);
            Destroy(gameObject);
        }
    }
}
