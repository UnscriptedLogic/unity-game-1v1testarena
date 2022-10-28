using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/New Item")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] private string itemID;
        [SerializeField] private string itemName;
        [SerializeField] private int maxStack;
        [SerializeField] private GameObject itemPrefab;

        [TextArea(5, 10)]
        [SerializeField] private string itemDesc;

        public string ID => itemID;
        public string Name => itemName;
        public int MaxStack => maxStack;
        public string Description => itemDesc;
        public GameObject Prefab => itemPrefab;
    }
}