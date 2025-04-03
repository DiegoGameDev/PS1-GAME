using Interactive;
using Single;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Player.Inventory
{
    public class PlayerHand : MonoBehaviour
    {
        public List<ItemBehaviour> itensInHand = new List<ItemBehaviour>();

        [field: SerializeField]
        public int maxCapaxity { get; private set; }
        int index = 0; // -1 para a mão do player

        //UI
        private ControllerSlots controllerSlots;
        private Action<int> InventoryUpdate;
        private Action<List<Sprite>> ItensSpritesUpdate;

        private void Start()
        {
            controllerSlots = Game.main.controllerSlots;
            InventoryUpdate += controllerSlots.CurrentSlot;
            ItensSpritesUpdate += controllerSlots.UpdateSlotsUI;
            index = -1;

        }

        public ItemBehaviour ItemInHand() => itensInHand[index];

        private void Update()
        {
            //transform.rotation = cam.rotation;
            //Vector3 targetPosition = cam.position + cam.forward * offset;
            //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
            //transform.rotation = Quaternion.Lerp(transform.rotation, .rotation, Time.deltaTime * 10f);

            if (Input.GetKeyDown(KeyCode.Alpha1))
                SetItem(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SetItem(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SetItem(2);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                SetItem(3);
        }

        public void UseItem()
        {
            if (index != -1)
            {
                itensInHand[index].Use(Game.main.Player);
            }
                
        }

        public void InteractItem()
        {
            if (index != -1)
                itensInHand[index].InteractItem();
        }

        public void MainItem() // ativa o objeto atual e desativa os outros
        {
            if (itensInHand.Count < 1)
                return;

            InventoryUpdate?.Invoke(index);

            if (index < itensInHand.Count)
                for (int i = 0; i < itensInHand.Count; i++)
                {
                    if (i != index)
                        itensInHand[i].gameObject.SetActive(false);
                    if (i == index)
                        itensInHand[i].gameObject.SetActive(true);
                }
            else
            {
                index = -1;
                MainItem();
            }
                
        }

        public void SetItem(int slot)
        {
            index = slot;
            MainItem();
        }

        //uma solução para o item vir na posição correta para a mãe é usar prefabs
        //quando pegamos o item ao inves de pegarmos o item diretamente pegamos o prefab
        // e deletamos o que está no cenario, e quando dropar usamos o que está na mão

        public void Additem(ItemBehaviour obj)
        {
            if (itensInHand.Count < maxCapaxity)
            {
                //obj.transform.parent = ItemTransform;
                // obj.transform.position = Vector3.zero;
                obj = Instantiate(obj, transform);
                obj.DestroyColliders();
                itensInHand.Add(obj);
                obj.Looking(false);
                index = itensInHand.Count - 1;
                MainItem();
            }

            if (itensInHand.Count > 0)
            {
                List<Sprite> sprites = new List<Sprite>();
                for (int i = 0; i < itensInHand.Count; i++)
                {
                    sprites.Add(itensInHand[i].item.spriteItem);
                }

                ItensSpritesUpdate?.Invoke(sprites);
            }
        }

        public void RemoveItem(int id)
        {
            Destroy(itensInHand[id].gameObject);
            itensInHand.RemoveAt(id);
            index = -1;
            MainItem();

            if (itensInHand.Count > 0)
            {
                List<Sprite> sprites = new List<Sprite>();
                for (int i = 0; i < itensInHand.Count; i++)
                {
                    sprites.Add(itensInHand[i].item.spriteItem);
                }

                ItensSpritesUpdate?.Invoke(sprites);
            }
        }

        public void RemoveItem(ItemBehaviour item)
        {
            for (int i = 0; i < itensInHand.Count; i++)
            {
                if (itensInHand[i] == item)
                    Destroy(itensInHand[i].gameObject);
            }

            itensInHand.Remove(item);
            index = -1;
            MainItem();

            if (itensInHand.Count > 0)
            {
                List<Sprite> sprites = new List<Sprite>();
                for (int i = 0; i < itensInHand.Count; i++)
                {
                    sprites.Add(itensInHand[i].item.spriteItem);
                }

                ItensSpritesUpdate?.Invoke(sprites);
            }
        }
    }
}