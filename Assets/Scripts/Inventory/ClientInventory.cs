using Item;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class ClientInventory : NetworkBehaviour
    {
        [SerializeField] private List<ItemSO> possibleItems;
        [SerializeField] private Transform itemHold;
        
        private InputManager inputManager;
        private ItemFactory itemFactory;
        private List<ItemBase> inventory = new List<ItemBase>();
        private int currentItemIndex;

        public List<ItemBase> Inventory => inventory;
        public int CurrentItemIndex => currentItemIndex;

        public override void OnNetworkSpawn()
        {
            inputManager = InputManager.instance;
            inputManager.OnUseItemPerformed += StartUseItem;
            inputManager.OnUseItemCancelled += EndUseItem;

            itemFactory = new ItemFactory(possibleItems);

            CreateItem("Rifle");
        }

        private void CreateItem(string itemID)
        {
            InventoryItem item = itemFactory.GetItem(itemID);
            GameObject itemObject = Instantiate(item.Prefab, itemHold.position, itemHold.rotation, itemHold);
            inventory.Add(itemObject.GetComponent<ItemBase>());
            currentItemIndex = inventory.Count - 1;
        }

        public void StartUseItem()
        {
            if (inventory.Count > 0)
            {
                inventory[currentItemIndex].UseItemStart();
            }
        }


        private void EndUseItem()
        {
            if (inventory.Count > 0)
            {
                inventory[currentItemIndex].UseItemEnd();
            }
        }
    }
}