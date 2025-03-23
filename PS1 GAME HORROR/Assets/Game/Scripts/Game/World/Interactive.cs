using Player;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive
{
    public abstract class Interactive : MonoBehaviour
    {
        [Header("Interactive Settings")]
        public string NameObject;
        [SerializeField] protected MeshRenderer mesh;

        [SerializeField] protected List<Material> materialOutLine;
        protected List<Material> materialBase;
        [Space]
        [SerializeField] protected ItemBehaviour prefab;

        bool rayInThisObject = false;
        

        public void Looking(bool looking)
        {
            if (looking && !rayInThisObject)
                mesh.SetMaterials(materialOutLine);
            rayInThisObject = looking;

            if (!looking && !rayInThisObject)
                mesh.SetMaterials(materialBase);
        }

        public virtual void Interact(PlayerController player, ItemBehaviour item)
        {

        }public virtual void Interact(PlayerController player)
        {

        }
    }
}