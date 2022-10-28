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

        public virtual void UseItem()
        {
            Debug.Log("Use Item");
        }
    }
}