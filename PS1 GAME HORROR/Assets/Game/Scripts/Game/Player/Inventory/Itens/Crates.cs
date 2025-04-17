using Interactions;
using Player;
using UnityEngine;

namespace Itens
{
    public sealed class Crates : ItemBehaviour
    {
        [Header("Crates")]
        //[SerializeField] ItemBehaviour itemInCrates;
        [SerializeField] LayerMask derfaultLayer;

        public override void Interact(PlayerController player)
        {
            //base.Interact(player);

            if (estateItemBehaviour == EstateItemBehaviour.Hand)
                return;

            if (!player.inventory.NullSlot)
                return;

            prefab.SetEstate(EstateItemBehaviour.Hand);
            prefab.gameObject.SetActive(true);
            player.inventory.AddItem(prefab);
            gameObject.layer = derfaultLayer.value;
            Looking(false);
        }
    }
}