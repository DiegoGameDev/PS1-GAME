using Itens;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Single
{
    public class GameInput : MonoBehaviour
    {
        public PlayerInput playerInput;

        private void Awake()
        {
            playerInput = new PlayerInput();
            playerInput.Player.Enable();

            playerInput.Player.Move.performed += Move;
            playerInput.Player.Move.canceled += Move;

            playerInput.Player.Run.performed += Run;
            playerInput.Player.Run.canceled += Run;

            playerInput.Player.Interact.performed += Interact;
            playerInput.Player.Interact.canceled += Interact;

            //playerInput.Player.Crouch.performed += Run;
            //playerInput.Player.Interact.canceled += Run;

            playerInput.Player.Fire1.performed += Use;
            playerInput.Player.Fire2.performed += InteractItem;

            //dialog
            playerInput.Dialog.Skip.performed += SkipDialog;
            playerInput.Dialog.SkipAll.performed += SkipDialog;
        }

        public void EnablePlayerNormal()
        {
            playerInput.Player.Enable();
            playerInput.Dialog.Disable();
        }

        public void EnableDialogNormal()
        {
            playerInput.Dialog.Enable();
            playerInput.Player.Disable();
        }

        private void SkipDialog(InputAction.CallbackContext context)
        {
            FindFirstObjectByType<CassetePlayer>().Next();
            Game.main.dialogManager.Ready();
        }

        private void Use(InputAction.CallbackContext context)
        {
            Game.main.Player.inventory.playerHand.UseItem();
        }

        private void InteractItem(InputAction.CallbackContext context)
        {
            Game.main.Player.inventory.playerHand.InteractItem();
        }

        private void Move(InputAction.CallbackContext context)
        {
            Game.main.Player.axis = context.ReadValue<Vector2>();
        }

        private void Run(InputAction.CallbackContext context)
        {
            Game.main.Player.Run = context.performed ? true : false;
        }

        private void Interact(InputAction.CallbackContext context)
        {
            Game.main.Player.InteractButton = context.performed ? true : false;
        }
    }
}