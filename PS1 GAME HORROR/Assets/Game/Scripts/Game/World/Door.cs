using Interactions;
using Player;
using Player.Inventory;
using Single;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace World
{
    public sealed class Door : ItemBehaviour
    {
        [SerializeField] bool otherScene = true;

        [SerializeField] Vector3 PositionInWorld;

        [Space]
        [SerializeField] SceneObject sceneObject;
        [SerializeField] ItemSO key;
        [Space]

        [SerializeField] GameObject DoorTrackOpening;
        [SerializeField] GameObject Cam;
        [Space]

        [SerializeField] GameObject map;

        public override void Interact(PlayerController player)
        {
            if (player.inventory.HaveKey(key) && key != null)
            {
                DoorTrackOpening.SetActive(true);
                Cam.SetActive(true);
                map.SetActive(false);
            }
            if (key == null)
            {
                DoorTrackOpening.SetActive(true);
                Cam.SetActive(true);
                map.SetActive(false);
            }
        }

        public void LoadScene()
        {
            if (otherScene)
                SceneManager.LoadScene(sceneObject.index);
            else
            {
                Game.main.Player.transform.position = PositionInWorld;
                Cam.SetActive(false);
                DoorTrackOpening.SetActive(false);
                map.SetActive(true);
            }
        }
    }

}