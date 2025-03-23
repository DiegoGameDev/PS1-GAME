using Interactive;
using Player.Inventory;
using Single;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] Camera cam;

        [Header("Inventory")]
        [field : SerializeField]
        public PlayerInventory inventory { get; private set; }

        [Header("Player Settings")]
        [SerializeField] float velocityMove;
        [SerializeField] float velocityRun;

        [HideInInspector]
        public Vector2 axis;
        [HideInInspector]
        public bool Run;

        [Space]
        [SerializeField] float distanceRay = 2f;
        [SerializeField] LayerMask interactiveLayer;
        Ray ray;

        [Header("Components")]
        Rigidbody rb;


        [Header("Interact")]
        [HideInInspector]
        public bool InteractButton;

        [SerializeField] TMPro.TextMeshProUGUI text;
        [HideInInspector]
        public ItemBehaviour item { get; private set; }

        private void Awake()
        {
            //cam = Camera.main;
            rb = GetComponent<Rigidbody>();
            Cursor.visible = false;
        }

        void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            text.text = $"{(1 / Time.deltaTime).ToString("0")}";
            CheckObject();
        }

        private void Move()
        {
            Vector3 direction;

            if (Run)
                direction = new Vector3(axis.x, rb.linearVelocity.y, axis.y) * velocityRun;
            else
                direction = new Vector3(axis.x, rb.linearVelocity.y, axis.y) * velocityMove;

            //transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);

            direction = transform.TransformDirection(direction);

            rb.linearVelocity = direction;
        }

        private void CheckObject()
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward,out RaycastHit hit, distanceRay, interactiveLayer))
            {
                if (item != hit.collider.GetComponent<ItemBehaviour>() && item)
                {
                    item.Looking(false);
                    item = hit.collider.GetComponent<ItemBehaviour>();
                }


                if (!item) 
                    item = hit.collider.GetComponent<ItemBehaviour>();

                item.Looking(true);
                if (Game.main.gameInput.playerInput.Player.Interact.WasPressedThisFrame() && item != null)
                {
                    item.Interact(this);
                }
            }
            else
            {
                if (item)
                    item.Looking(false);

                item = null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (cam != null)
                Gizmos.DrawRay(transform.position, cam.transform.forward * distanceRay);
        }
    }
}