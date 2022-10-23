/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Aeacus.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/22/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGS.Ghostdoms
{
    public class Aeacus : LRTPanel
    {
        protected class ATBtnNames : LRTBtnNames
        {
            public const string Refresh = "Refresh";
        }

        [SerializeField]
        protected Collector collector;

        public event Action<GameObject, bool> OnGOSelected;

        protected override void Awake()
        {
            base.Awake();
            Refresh();
        }

        protected override void Toolbar_OnButtonClick(string btnName)
        {
            if (btnName == ATBtnNames.Refresh)
            {
                Refresh();
            }
            else
            {
                base.Toolbar_OnButtonClick(btnName);
            }
        }

        protected void Refresh()
        {
            var gos = Resources.FindObjectsOfTypeAll<Transform>();
            var topGos = new List<GameObject>();
            foreach (var go in gos)
            {
                if (go.hideFlags == HideFlags.None && go.transform.parent == null)
                {
                    topGos.Add(go.gameObject);
                }
            }
            Refresh(topGos);
        }

        protected void Refresh(ICollection<GameObject> gos)
        {
            collector.RequireItems(gos.Count);

            var i = 0;
            foreach (var go in gos)
            {
                var goField = collector.GetItem<GOField>(i);
                goField.collector.prefab = collector.prefab;

                goField.Refresh(go);
                goField.gameObject.name = go.name;

                goField.OnSelected = GO_OnSelected;
                goField.gameObject.SetActive(true);
                i++;
            }
        }

        protected void GO_OnSelected(GameObject go, bool isSelect)
        {
            if (OnGOSelected != null)
            {
                OnGOSelected.Invoke(go, isSelect);
            }
        }
    }
}