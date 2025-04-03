using DialogueSystem;
using Interactive;
using Player;
using Single;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Itens
{
    public sealed class CassetePlayer : ItemBehaviour
    {
        [Header("CassetePlayer")]
        [SerializeField]
        AudioSource audioPlayer;
        VhsTape vhsTape;
        [Space]

        // a posição que a fita ficara após a ser tocada
        [SerializeField] Transform VHSPositionEndPlay;
        [SerializeField] Animator anim;

        DialogManager dialogManager;

        private readonly int PlayVhs = Animator.StringToHash("PlayVHS");
        private readonly int EndVhs = Animator.StringToHash("EndVHS");

        float lenghtAudio;
        float countTime;

        private void Start()
        {
            dialogManager = Game.main.dialogManager;
        }

        public override void Interact(PlayerController player, ItemBehaviour item)
        {
            VhsTape vhs = (VhsTape)item;

            // fazer um script que exibe uma mensagem de erro temporariamente
            if (vhs.VHSAudio == null)
            {
                Game.main.gameMessage.ShowMessage("Insira uma fita cassete para usar o tocador de fitas", GameMessage.TypeMessage.message, 2);
                return;
            }
                Game.main.gameMessage.ShowMessage("Você inseriu a fita vhs", GameMessage.TypeMessage.message, 3);
                

            vhsTape = vhs;
            vhsTape.CopyTape(vhs);
            print(vhsTape.IsDialog);

            audioPlayer.clip = vhsTape.VHSAudio;
            lenghtAudio = audioPlayer.clip.length;
            countTime = 0;

            vhsTape = Instantiate(vhsTape, VHSPositionEndPlay.position, Quaternion.identity);
            vhsTape.gameObject.SetActive(false);

            player.inventory.RemoveItem(item);

            vhsTape.enabled = true;
            Play();
            //anim.SetTrigger(PlayVhs);
        }
        // chamar no fim da animação da fita cassete entrando no tocador
        private void Play()
        {
            //audioPlayer.Play();

            //dialog
            if (vhsTape.IsDialog)
            {
                //timelinePlayable.playableAsset = vhsTape.timeLine;
                vhsTape.name = vhsTape.NameObject;
                vhsTape.gameObject.AddComponent<BoxCollider>();
                vhsTape.signal += FrameEvent;
            vhsTape.timeLine.Play();
            }
            else
            {
                vhsTape.name = vhsTape.NameObject;
                vhsTape.gameObject.AddComponent<BoxCollider>();
                StartCoroutine(playTape(vhsTape));
            }
        }

        public void FrameEvent(bool first, bool last)
        {
            if (last)
            {
                Game.main.gameInput.EnablePlayerNormal();
                return;
            }
            if (first)
            {
                dialogManager.StartDialog(vhsTape.DialogComponents, vhsTape.vhsPlayed);
                return;
            }
            vhsTape.timeLine.Pause();
        }

        public void Next()
        {
            //if (vhsTape != null)

            dialogManager.Ready();
            vhsTape.timeLine.Resume();
        }

        IEnumerator playTape(VhsTape vhs)
        {
            vhs.gameObject.SetActive(false);
            while (countTime < lenghtAudio)
            {
                countTime += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            audioPlayer.Stop();
            vhsTape.gameObject.SetActive(true);
            vhsTape.vhsPlayed.Invoke();
            audioPlayer.clip = null;
            vhs.gameObject.SetActive(true);
            //vhs.transform.position = new Vector3(0, 0, 0);
        }
    }
}