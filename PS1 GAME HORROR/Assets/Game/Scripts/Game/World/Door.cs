using Interactions;
using Player;
using Player.Inventory;
using Single;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace World
{
    public class Door : MonoBehaviour, IInteractive
    {
        [SerializeField] bool otherScene = true;

        [SerializeField] Vector3 PositionInWorld;

        [Space]
        [SerializeField] SceneObject sceneObject;
        [SerializeField] Key key;
        [Space]

        [SerializeField] GameObject DoorTrackOpening;
        [SerializeField] GameObject Cam;
        [Space]

        [SerializeField] GameObject map;
        [Space]
        [SerializeField] string messageNoKey;

        public virtual void Interact(PlayerController player)
        {
            if (player.inventory.HaveKey(key) && key != null)
            {
                DoorTrackOpening.SetActive(true);
                Cam.SetActive(true);
                map.SetActive(false);

                Invoke("LoadScene", 2.1f);
            }
            else
            {
                Game.main.gameMessage.ShowMessage(messageNoKey, GameMessage.TypeMessage.message);
            }

            if (key == null)
            {
                DoorTrackOpening.SetActive(true);
                Cam.SetActive(true);
                map.SetActive(false);
                Invoke("LoadScene", 2.1f);
            }
        }

        public void LoadScene()
        {
            if (otherScene)
            {
                Game.main.Player.inventory.SaveInventory();
                SceneManager.LoadScene(sceneObject.index);
            }
            else
            {
                Game.main.Player.transform.position = PositionInWorld;
                Cam.SetActive(false);
                DoorTrackOpening.SetActive(false);
                map.SetActive(true);
            }
        }

        public void Looking(bool looking)
        {
            
        }
    }

}