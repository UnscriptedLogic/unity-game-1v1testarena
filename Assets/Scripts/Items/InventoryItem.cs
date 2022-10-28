using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class InventoryItem
    {
        private string itemID;
        private string itemName;
        private int maxStack;
        private GameObject prefab;
        private string desc;

        public string ID => itemID;
        public string Name => itemName;
        public int MaxStack => maxStack;
        public string Description => desc;
        public GameObject Prefab => prefab;

        public InventoryItem(ItemSO itemSO)
        {
            itemID = itemSO.ID;
            itemName = itemSO.Name;
            maxStack = itemSO.MaxStack;
            prefab = itemSO.Prefab;
            desc = itemSO.Description;
        }
    }
}