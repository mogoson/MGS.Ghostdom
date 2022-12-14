/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Rhadamanthys.cs
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
    public class Rhadamanthys : Window
    {
        protected class RToolNames : ToolNames
        {
            public const string Refresh = "Refresh";
            public const string Search = "Search";
        }

        [SerializeField]
        protected Collector collector;
        protected GameObject go;
        protected string keyword = string.Empty;

        public void Refresh(GameObject go)
        {
            this.go = go;
            if (go == null)
            {
                collector.RequireItems(0);
            }
            else
            {
                Refresh(keyword);
            }
        }

        protected void Refresh(string keyword)
        {
            var objs = new List<object>();
            if (go != null)
            {
                if (go.GetType().Name.ToLower().Contains(keyword))
                {
                    objs.Add(go);
                }

                var cpnts = go.GetComponents<Component>();
                foreach (var cpnt in cpnts)
                {
                    if (cpnt == null)
                    {
                        Debug.LogWarningFormat("A Component is missing in the {0}.", go.name);
                        continue;
                    }
                    if (cpnt.GetType().Name.ToLower().Contains(keyword))
                    {
                        objs.Add(cpnt);
                    }
                }
            }
            Refresh(objs);
        }

        protected void Refresh(ICollection<object> objs)
        {
            collector.RequireItems(objs.Count);

            var i = 0;
            foreach (var obj in objs)
            {
                var item = collector.GetItem<ObjectField>(i);
                item.Refresh(obj);
                item.gameObject.SetActive(true);
                i++;
            }
        }

        protected override void Toolbar_OnButtonClick(string btnName)
        {
            if (btnName == RToolNames.Refresh)
            {
                Refresh(keyword);
            }
            else
            {
                base.Toolbar_OnButtonClick(btnName);
            }
        }

        protected override void Toolbar_OnInputValueChanged(string iptName, string value)
        {
            if (iptName == RToolNames.Search)
            {
                keyword = string.IsNullOrEmpty(value) ? value : value.ToLower();
                Refresh(keyword);
            }
        }
    }
}