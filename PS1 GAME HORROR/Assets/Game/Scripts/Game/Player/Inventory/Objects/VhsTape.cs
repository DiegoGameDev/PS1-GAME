using DialogueSystem;
using Interactions;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Itens
{
    public sealed class VhsTape : ItemBehaviour
    {
        [Header("VHS")]
        public UnityEvent<PlayerController> vhsPlayed;
        public AudioVHS audioVhs;

        public override void Use(PlayerController player)
        {
            var obj = (CassetePlayer)player.interactiveObject;

            if (obj == null || obj.item == null)
                return;

            obj.Interact(player ,this);
            player.inventory.RemoveItem(this);
        }

        public override void Interact(PlayerController player)
        {
            if (!player.inventory.NullSlot)
                return;

            // atrib this vhs in prefab
            transform.rotation = prefab.transform.rotation;
            transform.position = prefab.transform.position;

            SetEstate(EstateItemBehaviour.Hand);
            DestroyColliders();
            Looking(false);
            player.inventory.AddItem(this);
        }

        public void CopyTape(VhsTape vhs)
        {
            prefab = vhs.prefab;
        }
    }

    [System.Serializable]
    public struct AudioVHS
    {
        public AudioClip[] clips;
        public DialogComponents dialogComponents;
    }
}