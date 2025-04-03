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
        UnityEvent EndDialog;
        public Action SkipDialog { get; private set; }
        public Action SkipallDialog { get; private set; }

        private void Awake()
        {
            //dialogWriter.finishiSpeech += FinishSpeech;
        }

        public void StartDialog(DialogComponents dialogComponents, UnityEvent @event)
        {
            Game.main.gameInput.EnablePlayerNormal();
            EndDialog = @event;
            this.dialogComponents = dialogComponents;
            estateDialog = EstateDialog.Writing;
            dialogWriter.StartWriter(dialogComponents.dialog[index].text, dialogComponents.dialog[index].profile);
        }

        public void Ready()
        {
            if (estateDialog == EstateDialog.Ready && index < dialogComponents.dialog.Count)
            {
                index++;
                dialogWriter.StartWriter(dialogComponents.dialog[index].text, dialogComponents.dialog[index].profile);
                estateDialog = EstateDialog.Writing;
                SkipDialog?.Invoke();
            }
            else if (index >= dialogComponents.dialog.Count)
            {
                index = 0;
                estateDialog = EstateDialog.Finish;
                EndDialog?.Invoke();
                dialogWriter.End();
            }
        }

        public void FinishSpeech()
        {
            dialogWriter.Skip();
            estateDialog = EstateDialog.Ready;
        }
    }
}