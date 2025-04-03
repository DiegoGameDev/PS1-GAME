using DialogueSystem;
using Interactive;
using Player;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Itens
{
    public sealed class VhsTape : ItemBehaviour
    {
        [Header("VHS")]
        public AudioClip VHSAudio;
        public DialogComponents DialogComponents;
        [field : SerializeField]
        public PlayableDirector timeLine { get; private set; }
        [field : SerializeField]
        public SignalAsset signalAsset { get; private set; }
        [Space]

        public UnityEvent vhsPlayed;

        //events
        public Action<bool,bool> signal;

        [field : SerializeField]
        public bool IsDialog { get; private set; }
        bool first = true;

        private void Start()
        {
            IsDialog = true;
            vhsPlayed.AddListener(End);
        }
        void End() => index = 0;
        int index = 0;

        public void SignalRecept()
        {
            index++;
            if (first)
            {
                first = false;
                signal.Invoke(true, false);
                return;
            }
            signal.Invoke(false, index == DialogComponents.dialog.Count -1);
        }

        public override void Use(PlayerController player)
        {
            var obj = (CassetePlayer)player.item;

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
            vhs.VHSAudio = VHSAudio;
            vhs.DialogComponents = DialogComponents;
            vhs.vhsPlayed = vhsPlayed;

            vhs.SetEstate(EstateItemBehaviour.Hand);
            Destroy(vhs.coll);
            player.inventory.AddItem(vhs);
            Destroy(gameObject);
        }

        public void CopyTape(VhsTape vhs)
        {
            prefab = vhs.prefab;
            signalAsset = vhs.signalAsset;
            IsDialog = vhs.IsDialog;
            SignalReceiver signal = GetComponent<SignalReceiver>();
            var eventt = new UnityEvent();
            eventt.AddListener(SignalRecept);
            signal.AddReaction(signalAsset, eventt);
        }
    }
}