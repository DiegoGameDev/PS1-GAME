using DialogueSystem;
using Interactions;
using Player;
using Single;
using System.Collections;
using UnityEngine;

namespace Itens
{
    public sealed class CassetePlayer : ItemBehaviour
    {
        [Header("CassetePlayer")]
        [SerializeField]
        AudioSource audioPlayer;
        int lenghtVhs;
        int index = 0;
        VhsTape vhsTape;
        [SerializeField] Animator anim;
        [SerializeField] AudioSource backingTrackVHS;
        [Space]

        // a posição que a fita ficara após a ser tocada
        [SerializeField] Transform VHSPositionEndPlay;

        DialogManager dialogManager;


        [SerializeField] bool _canNext;

        float lenghtAudio;
        float countTime;

        private readonly int PLAYVHSANIM = Animator.StringToHash("PlayVHS");
        private readonly int ENDVHSANIM = Animator.StringToHash("EndVHS");

        private void Start()
        {
            dialogManager = Game.main.dialogManager;
        }

        public override void Interact(PlayerController player, ItemBehaviour item)
        {
            VhsTape vhs = (VhsTape)item;

            // fazer um script que exibe uma mensagem de erro temporariamente
            if (vhs.audioVhs.clips == null)
            {
                Game.main.gameMessage.ShowMessage("Insira uma fita cassete para usar o tocador de fitas", GameMessage.TypeMessage.message, 2);
                return;
            }
                Game.main.gameMessage.ShowMessage("Você inseriu a fita vhs", GameMessage.TypeMessage.message, 3);

            Game.main.gameInput.EnableDialogNormal();
            interacting = this;

            vhsTape = vhs;

            audioPlayer.clip = vhsTape.audioVhs.clips[index];
            lenghtVhs = vhsTape.audioVhs.clips.Length;

            lenghtAudio = audioPlayer.clip.length;
            countTime = 0;

            vhsTape = Instantiate(vhsTape, VHSPositionEndPlay.position, Quaternion.identity);
            vhsTape.gameObject.AddComponent<BoxCollider>();
            vhsTape.gameObject.SetActive(false);

            player.inventory.RemoveItem(item);

            vhsTape.enabled = true;
            anim.SetTrigger(PLAYVHSANIM);
            //Play();
        }
        // chamar no fim da animação da fita cassete entrando no tocador
        public void Play()
        {
            dialogManager.StartDialog(vhsTape.audioVhs.dialogComponents, vhsTape.vhsPlayed);
            audioPlayer.Play();
            StartCoroutine(playTape());
        }

        public override void ContinueDialog()
        {
            //base.ContinueDialog();
            if (!_canNext)
            {
                SkipProcess();
                return;
            }
            if (index == lenghtVhs - 1)
            {
                anim.SetTrigger(ENDVHSANIM);
                return;
            }

            _canNext = false;
            index++;
            countTime = 0;
            audioPlayer.clip = vhsTape.audioVhs.clips[index];
            dialogManager.Ready();
            audioPlayer.Play();
            StartCoroutine(playTape());
        }

        public void SkipProcess()
        {
            StopAllCoroutines();
            dialogManager.FinishSpeech();
            countTime = 0;
            _canNext = true;
        }
        IEnumerator playTape()
        {
            lenghtAudio = vhsTape.audioVhs.clips[index].length;

            while (countTime < lenghtAudio)
            {
                countTime += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            dialogManager.FinishSpeech();
            countTime = 0;
            _canNext = true;

            //vhs.transform.position = new Vector3(0, 0, 0);
            
        }

        public void End()
        {
            audioPlayer.Stop();
            dialogManager.Ready();
            vhsTape.gameObject.SetActive(true);
            vhsTape.vhsPlayed.Invoke();
            audioPlayer.clip = null;
            vhsTape.gameObject.SetActive(true);
            vhsTape = null;

            interacting = null;
            //Game.main.gameInput.EnablePlayerNormal();
        }
    }
}