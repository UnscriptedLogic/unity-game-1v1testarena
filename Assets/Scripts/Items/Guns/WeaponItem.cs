using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class WeaponItem : ItemBase
    {
        public override void UseItem()
        {
            Debug.Log("Fire");
        }
    }
}