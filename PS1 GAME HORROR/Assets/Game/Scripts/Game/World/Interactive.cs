using Player;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public abstract class Interactive : MonoBehaviour, IInteractive
    {
        [Header("Interactive Settings")]
        public string NameObject;
        [SerializeField] protected MeshRenderer mesh;

        [SerializeField] protected List<Material> materialOutLine;
        protected List<Material> materialBase;
        [Space]
        [SerializeField] public ItemBehaviour prefab;

        public static Interactive interacting;

        bool rayInThisObject = false;

        public void Looking(bool looking)
        {
            if (looking && !rayInThisObject)
                mesh.SetMaterials(materialOutLine);
            rayInThisObject = looking;

            if (!looking && !rayInThisObject)
                mesh.SetMaterials(materialBase);
        }

        public virtual void ContinueDialog()
        {

        }

        public virtual void Interact(PlayerController player, ItemBehaviour item)
        {

        }
        public virtual void Interact(PlayerController player)
        {

        }
    }
}