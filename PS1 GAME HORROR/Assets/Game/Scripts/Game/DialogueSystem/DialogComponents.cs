using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "STNGAME/DIALOG")]
    public sealed class DialogComponents : ScriptableObject
    {
        public List<Components> dialog;
    }

    [System.Serializable]
    public struct Components
    {
        public string profile;
        public string text;
    }
}