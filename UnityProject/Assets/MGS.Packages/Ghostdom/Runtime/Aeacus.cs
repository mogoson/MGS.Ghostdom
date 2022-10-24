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
using UnityEngine.UI;

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
        protected ToggleGroup group;

        protected List<GameObject> gos = new List<GameObject>();
        protected string keyword = string.Empty;

        public event Action<GameObject> OnGOSelected;

        protected override void Awake()
        {
            base.Awake();

            group = collector.GetComponent<ToggleGroup>();
            Refresh(keyword, true);
        }

        protected override void Toolbar_OnButtonClick(string btnName)
        {
            if (btnName == AToolNames.Refresh)
            {
                Refresh(keyword, true);
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
                keyword = string.IsNullOrEmpty(value) ? value : value.ToLower();
                Refresh(keyword);
            }
        }

        protected void Refresh(string keyword, bool reloadGos = false)
        {
            if (reloadGos)
            {
                ReloadGos();
            }

            var gos = FilterGos(keyword);
            var isAllowFold = string.IsNullOrEmpty(keyword);
            Refresh(gos, isAllowFold);
        }

        protected void ReloadGos()
        {
            gos.Clear();
            var allGos = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (var go in allGos)
            {
                if (go.hideFlags != HideFlags.None)
                {
                    continue;
                }
                if (go.transform.root.name == "Ghostdom")
                {
                    continue;
                }
                gos.Add(go.gameObject);
            }
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
                        selects.Add(go);
                    }
                }
            }
            else
            {
                foreach (var go in gos)
                {
                    if (go.name.ToLower().Contains(keyword))
                    {
                        selects.Add(go);
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
                goField.OnSelected = GO_OnSelected;
                goField.gameObject.SetActive(true);
                i++;
            }

            GO_OnSelected(null, false);
        }

        protected void GO_OnSelected(GameObject go, bool isSelect)
        {
            if (!isSelect)
            {
                if (group.AnyTogglesOn())
                {
                    return;
                }
                go = null;
            }

            if (OnGOSelected != null)
            {
                OnGOSelected.Invoke(go);
            }
        }
    }
}