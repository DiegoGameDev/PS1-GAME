using Interactive;
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

        // a posição que a fita ficara após a ser tocada
        [SerializeField] Transform VHSPositionEndPlay;

        float lenghtAudio;
        float countTime;

        public override void Interact(PlayerController player, ItemBehaviour item)
        {
            VhsTape vhs = (VhsTape)item;
            print(2);
            // fazer um script que exibe uma mensagem de erro temporariamente
            if (vhs.VHSAudio == null)
                return;
            print(3);

            audioPlayer.clip = vhs.VHSAudio;
            audioPlayer.Play();

            lenghtAudio = audioPlayer.clip.length;
            countTime = 0;

            player.inventory.RemoveItem(item);
            Destroy(item.gameObject);

            var newTape = (VhsTape)Instantiate(item, new Vector3(0,0,0), Quaternion.identity);
            newTape.transform.parent = VHSPositionEndPlay;
            newTape.enabled = true;
            StartCoroutine(playTape(newTape));
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
            audioPlayer.clip = null;
            vhs.gameObject.SetActive(true);
            //vhs.transform.position = new Vector3(0, 0, 0);
        }
    }
}