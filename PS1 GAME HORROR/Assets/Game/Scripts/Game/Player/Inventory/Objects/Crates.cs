using Interactions;
using Player;
using Player.Inventory;
using UnityEngine;

namespace Itens
{
    public sealed class Crates : ItemBehaviour
    {
        [Header("Crates")]
        [SerializeField] ItemBehaviour itemInCrates;
        [SerializeField] Key key;
        [Space]
        [SerializeField] LayerMask derfaultLayer;

        [SerializeField] bool itemIsKey = false;

        public override void Interact(PlayerController player)
        {
            //base.Interact(player);

            if (estateItemBehaviour == EstateItemBehaviour.Hand)
                return;

            if (!player.inventory.NullSlot)
                return;

            if (itemIsKey)
            {
                player.inventory.AddKeys(key);
                gameObject.layer = derfaultLayer.value;
                Looking(false);
                return;
            }

            player.inventory.AddItem(itemInCrates);
            gameObject.layer = derfaultLayer.value;
            Looking(false);
        }

        public void AddItemToPlayer(PlayerController player)
        {
            player.inventory.AddKeys(key);
        }
    }
}