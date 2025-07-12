using Interactions;
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
        public PlayerInventory inventory { get; set; }

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
        public IInteractive interactiveObject { get; private set; }

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
                if (interactiveObject != hit.collider.GetComponent<IInteractive>() && interactiveObject != null)
                {
                    interactiveObject.Looking(false);
                    interactiveObject = hit.collider.GetComponent<IInteractive>();
                }


                if (interactiveObject == null)
                {
                    interactiveObject = hit.collider.GetComponent<IInteractive>();
                }
                    interactiveObject.Looking(true);
                if (Game.main.gameInput.playerInput.Player.Interact.WasPressedThisFrame() && interactiveObject != null && !Interactive.interacting)
                {
                    interactiveObject.Interact(this);
                }
            }
            else
            {
                if (interactiveObject != null)
                    interactiveObject.Looking(false);

                interactiveObject = null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (cam != null)
                Gizmos.DrawRay(transform.position, cam.transform.forward * distanceRay);
        }
    }
}