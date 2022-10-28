using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class ItemBase : MonoBehaviour
    {
        [SerializeField] protected string itemID;
        [SerializeField] protected int maxStack;
        public string ID => itemID;
        public int MaxStack => maxStack;

        protected bool isUsingItem;

        public virtual void UseItemStart()
        {
            Debug.Log("Item Started Using");
            isUsingItem = true;
        }

        public virtual void UseItemEnd()
        {
            Debug.Log("Item Ended Used");
            isUsingItem = false;
        }

        protected virtual void Update()
        {
            if (isUsingItem)
            {
                UsingItem();
            }
        }

        public virtual void UsingItem()
        {
            Debug.Log("Using Item");
        }
    }
}