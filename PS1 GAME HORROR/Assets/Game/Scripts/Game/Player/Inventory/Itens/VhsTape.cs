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
        public UnityEvent vhsPlayed;
        public AudioVHS audioVhs;

        public override void Use(PlayerController player)
        {
            var obj = (CassetePlayer)player.interactiveObject;

            if (obj == null || obj.item == null)
                return;

            obj.Interact(player ,this);
        }

        public override void Interact(PlayerController player)
        {
            //base.Interact(player);

            if (!player.inventory.NullSlot)
                return;

            // atrib this vhs in prefab
            var vhs = (VhsTape)prefab;
            vhs.prefab = prefab;
            vhs.audioVhs = audioVhs;
            vhs.vhsPlayed = vhsPlayed;

            vhs.SetEstate(EstateItemBehaviour.Hand);
            Destroy(vhs.coll);
            player.inventory.AddItem(vhs);
            Destroy(gameObject);
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