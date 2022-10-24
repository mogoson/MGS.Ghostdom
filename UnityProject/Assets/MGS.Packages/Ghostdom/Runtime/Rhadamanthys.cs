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
using UnityEngine.UI;

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
            var cpnts = new List<Component>();
            var coms = go.GetComponents<Component>();
            foreach (var cpnt in coms)
            {
                if (cpnt.GetType().Name.ToLower().Contains(keyword))
                {
                    cpnts.Add(cpnt);
                }
            }
            Refresh(cpnts);
        }

        protected void Refresh(ICollection<Component> cpnts)
        {
            collector.RequireItems(cpnts.Count);

            var i = 0;
            foreach (var cpnt in cpnts)
            {
                var item = collector.GetItem<Transform>(i);
                item.GetChild(0).GetComponent<Text>().text = cpnt.GetType().Name;
                item.GetChild(1).GetComponent<Text>().text = GetComponentInfo(cpnt);
                item.gameObject.SetActive(true);
                i++;
            }
        }

        protected string GetComponentInfo(Component cpnt)
        {
            var info = string.Empty;
            var properties = cpnt.GetType().GetProperties();
            foreach (var propertiy in properties)
            {
                var propertiyValue = string.Empty;
                try
                {
                    info += string.Format("{0}: {1}\r\n", propertiy.Name, propertiy.GetValue(cpnt, null));
                }
                catch { }
            }
            return info;
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