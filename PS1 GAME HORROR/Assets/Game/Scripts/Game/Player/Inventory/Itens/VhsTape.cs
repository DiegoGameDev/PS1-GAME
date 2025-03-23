using DialogueSystem;
using Interactive;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Itens
{
    public sealed class VhsTape : ItemBehaviour
    {
        [Header("VHS")]
        public AudioClip VHSAudio;
        public DialogComponents DialogComponents;

        public UnityEvent vhsPlayed;

        public override void Use(PlayerController player)
        {
            var obj = (CassetePlayer)player.item;

            if (obj == null || obj.item == null)
                return;

            obj.Interact(player ,this);
            print(1);
        }

        public override void Interact(PlayerController player)
        {
            //base.Interact(player);

            if (!player.inventory.NullSlot)
                return;

            // atrib this vhs in prefab
            var vhs = (VhsTape)prefab;
            vhs.VHSAudio = VHSAudio;
            vhs.DialogComponents = DialogComponents;
            vhs.vhsPlayed = vhsPlayed;

            vhs.SetEstate(EstateItemBehaviour.Hand);
            Destroy(vhs.coll);
            player.inventory.AddItem(vhs);
            Destroy(gameObject);
        }
    }
}