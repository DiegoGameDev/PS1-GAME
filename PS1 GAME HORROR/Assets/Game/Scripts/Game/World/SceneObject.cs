using UnityEngine;

namespace World
{
    [CreateAssetMenu(fileName = "SceneObject", menuName = "STNGAME/SceneObject")]
    public sealed class SceneObject : ScriptableObject
    {
        public string nameScene;
        public int index;
    }
}