using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class ItemFactory
    {
        private List<InventoryItem> items;
        
        public ItemFactory(List<ItemSO> itemScriptables)
        {
            items = new List<InventoryItem>();
            for (int i = 0; i < itemScriptables.Count; i++)
            {
                items.Add(new InventoryItem(itemScriptables[i]));
            }
        }

        public InventoryItem GetItem(string itemID)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == itemID)
                {
                    return items[i];
                }
            }
            return null;
        }
    }
}