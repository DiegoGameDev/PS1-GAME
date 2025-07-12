using Player;
using Single;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem
{
    public sealed class DialogManager : MonoBehaviour
    {
        public enum EstateDialog { Ready, Writing, Finish}
        EstateDialog estateDialog = EstateDialog.Finish;

        [SerializeField] DialogWriter dialogWriter;

        DialogComponents dialogComponents;
        int index;

        //events
        UnityEvent<PlayerController> EndDialog;
        public Action SkipDialog { get; private set; }
        public Action SkipallDialog { get; private set; }

        [SerializeField] GameObject panel;

        public void StartDialog(DialogComponents dialogComponents, UnityEvent<PlayerController> @event)
        {
            panel.SetActive(true);
            dialogWriter.finishiSpeech += FinishSpeech;
            Game.main.gameInput.EnableDialogNormal();
            EndDialog = @event;
            this.dialogComponents = dialogComponents;
            estateDialog = EstateDialog.Writing;
            dialogWriter.StartWriter(dialogComponents.dialog[index].text, dialogComponents.dialog[index].profile);
        }

        public void Ready()
        {
            if (estateDialog == EstateDialog.Ready)
            {
                index++;
                if (index < dialogComponents.dialog.Count)
                {
                    dialogWriter.StartWriter(dialogComponents.dialog[index].text, dialogComponents.dialog[index].profile);
                    estateDialog = EstateDialog.Writing;
                    SkipDialog?.Invoke();
                }
                else if (index >= dialogComponents.dialog.Count)
                {
                    index = 0;
                    estateDialog = EstateDialog.Finish;
                    EndDialog?.Invoke(Game.main.Player);
                    dialogWriter.End();
                    panel.SetActive(false);
                    Game.main.gameInput.EnablePlayerNormal();
                }
            }
        }

        public void FinishSpeech()
        {
            //dialogWriter.Skip();
            estateDialog = EstateDialog.Ready;
        }
    }
}