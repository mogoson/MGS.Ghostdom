/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Cerberus.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/22/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Ghostdoms
{
    public class Cerberus : MonoBehaviour
    {
        public enum HeadID
        {
            Head_Aeacus,
            Head_Rhadamanthys,
            Head_Lyre,
            Head_Minos,
            Head_Pomegranate
        }

        public event Action<HeadID> OnHeadShake;
        protected bool isActive = false;

        protected virtual void Awake()
        {
            var heads = GetComponentsInChildren<Button>(true);
            foreach (var head in heads)
            {
                head.onClick.AddListener(() => Head_Click(head));
            }
        }

        protected void Head_Click(Button head)
        {
            var id = (HeadID)Enum.Parse(typeof(HeadID), head.name, true);
            if (id == HeadID.Head_Lyre)
            {
                isActive = !isActive;
                ToggleHeads(isActive);
            }
            OnHeadShake.Invoke(id);
        }

        protected void ToggleHeads(bool isActive)
        {
            foreach (Transform head in transform)
            {
                if (head.name != HeadID.Head_Lyre.ToString())
                {
                    head.gameObject.SetActive(isActive);
                }
            }
        }
    }
}