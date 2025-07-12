using Interactions;
using Player;
using Player.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Itens
{
    public sealed class Crates : ItemBehaviour
    {
        [Header("Crates")]
        [SerializeField] Queue<ItemBehaviour> itens = new Queue<ItemBehaviour>();
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

            if (itemInCrates == null)
                return;

            itemInCrates.Interact(player);
            gameObject.layer = derfaultLayer.value;
            Looking(false);
            itemInCrates = null;
        }

        public void AddItemToPlayer(PlayerController player)
        {
            player.inventory.AddKeys(key);
            Destroy(gameObject);
        }
    }
}