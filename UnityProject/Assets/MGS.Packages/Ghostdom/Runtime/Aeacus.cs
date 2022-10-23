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
    public class Aeacus : Window
    {
        protected class AToolNames : ToolNames
        {
            public const string Refresh = "Refresh";
            public const string Search = "Search";
        }

        [SerializeField]
        protected Collector collector;
        protected List<GameObject> gos = new List<GameObject>();

        public event Action<GameObject, bool> OnGOSelected;

        protected override void Awake()
        {
            base.Awake();
            Refresh();
        }

        protected override void Toolbar_OnButtonClick(string btnName)
        {
            if (btnName == AToolNames.Refresh)
            {
                Refresh();
            }
            else
            {
                base.Toolbar_OnButtonClick(btnName);
            }
        }

        protected override void Toolbar_OnInputValueChanged(string iptName, string value)
        {
            if (iptName == AToolNames.Search)
            {
                Refresh(value);
            }
        }

        protected void Refresh()
        {
            FindGos();
            Refresh(string.Empty);
        }

        protected void FindGos()
        {
            gos.Clear();
            var allGos = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (var go in allGos)
            {
                if (go.hideFlags == HideFlags.None)
                {
                    gos.Add(go.gameObject);
                }
            }
        }

        protected void Refresh(string keyword)
        {
            var gos = FilterGos(keyword);
            var isAllowFold = string.IsNullOrEmpty(keyword);
            Refresh(gos, isAllowFold);
        }

        protected List<GameObject> FilterGos(string keyword)
        {
            var selects = new List<GameObject>();
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (var go in gos)
                {
                    if (go.transform.parent == null)
                    {
                        selects.Add(go.gameObject);
                    }
                }
            }
            else
            {
                keyword = keyword.ToLower();
                foreach (var go in gos)
                {
                    if (go.name.ToLower().Contains(keyword))
                    {
                        selects.Add(go.gameObject);
                    }
                }
            }
            return selects;
        }

        protected void Refresh(ICollection<GameObject> gos, bool isAllowFold)
        {
            collector.RequireItems(gos.Count);

            var i = 0;
            foreach (var go in gos)
            {
                var goField = collector.GetItem<GOField>(i);
                goField.collector.prefab = collector.prefab;

                goField.Refresh(go, isAllowFold);
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