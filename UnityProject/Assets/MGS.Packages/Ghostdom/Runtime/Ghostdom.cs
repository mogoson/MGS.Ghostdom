/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Ghostdom.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/22/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Ghostdoms
{
    public class Ghostdom : MonoBehaviour
    {
        [SerializeField]
        protected Cerberus cerberus;

        [SerializeField]
        protected Aeacus aeacus;

        [SerializeField]
        protected Rhadamanthys rhadamanthys;

        [SerializeField]
        protected Minos minos;

        protected List<Vector3> layouts = new List<Vector3>
        {
            new Vector3(0.15f, 0.2f, 0.25f),
            new Vector3(0.35f, 0.35f, 0.1f),
            new Vector3(0.1f, 0.1f, 0.45f)
        };
        protected int layout = 0;

        protected virtual void Awake()
        {
            cerberus.OnHeadShake += Cerberus_OnHeadShake;
            aeacus.OnGOSelected += Aeacus_OnGOSelected;
            minos.OnSizeChanged += Minos_OnSizeChanged;

            FitToLayout(layouts[layout]);
        }

        protected void Cerberus_OnHeadShake(Cerberus.HeadID headID)
        {
            switch (headID)
            {
                case Cerberus.HeadID.Head_Aeacus:
                    aeacus.gameObject.SetActive(!aeacus.gameObject.activeSelf);
                    break;

                case Cerberus.HeadID.Head_Rhadamanthys:
                    rhadamanthys.gameObject.SetActive(!rhadamanthys.gameObject.activeSelf);
                    break;

                case Cerberus.HeadID.Head_Lyre:
                    var judgment = aeacus.transform.parent.gameObject;
                    judgment.SetActive(!judgment.activeSelf);
                    break;

                case Cerberus.HeadID.Head_Minos:
                    minos.gameObject.SetActive(!minos.gameObject.activeSelf);
                    break;

                case Cerberus.HeadID.Head_Trident:
                    {
                        layout++;
                        if (layout >= layouts.Count)
                        {
                            layout = 0;
                        }
                        FitToLayout(layouts[layout]);
                    }
                    break;
            }
        }

        protected void Aeacus_OnGOSelected(GameObject go, bool isSelect)
        {
            Debug.LogFormat("Aeacus_OnGOSelected {0} {1}", go.name, isSelect);
        }

        protected void Minos_OnSizeChanged(RectTransform.Axis axis, float size)
        {
            if (axis == RectTransform.Axis.Vertical)
            {
                var rect = (aeacus.transform.parent as RectTransform).rect;
                var aeacusHeight = rect.height - size - 15;
                aeacus.SetSize(RectTransform.Axis.Vertical, aeacusHeight);
                rhadamanthys.SetSize(RectTransform.Axis.Vertical, aeacusHeight);
            }
        }

        protected void FitToLayout(Vector3 layout)
        {
            var rect = (aeacus.transform.parent as RectTransform).rect;
            var aeacusWidth = rect.width * layout.x;
            var rhadamanthysWidth = rect.width * layout.y;
            aeacus.SetSize(RectTransform.Axis.Horizontal, aeacusWidth);
            rhadamanthys.SetSize(RectTransform.Axis.Horizontal, rhadamanthysWidth);

            var minosHeight = rect.height * layout.z;
            minos.SetSize(RectTransform.Axis.Vertical, minosHeight);
        }
    }
}