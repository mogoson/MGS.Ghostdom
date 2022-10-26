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
            var objs = new List<Object>();
            if (go != null)
            {
                objs.Add(go);
                var cpnts = go.GetComponents<Component>();
                foreach (var cpnt in cpnts)
                {
                    if (cpnt.GetType().Name.ToLower().Contains(keyword))
                    {
                        objs.Add(cpnt);
                    }
                }
            }
            Refresh(objs);
        }

        protected void Refresh(ICollection<Object> objs)
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

        protected string GetObjectInfo(Object obj)
        {
            var lines = new List<string>();

            var fields = obj.GetType().GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(Matrix4x4))
                {
                    continue;
                }
                try
                {
                    lines.Add(string.Format("{0}: {1}", field.Name, field.GetValue(obj)));
                }
                catch { }
            }

            var properties = obj.GetType().GetProperties();
            foreach (var propertiy in properties)
            {
                if (propertiy.PropertyType == typeof(Matrix4x4))
                {
                    continue;
                }
                try
                {
                    lines.Add(string.Format("{0}: {1}", propertiy.Name, propertiy.GetValue(obj, null)));
                }
                catch { }
            }

            return string.Join("\r\n", lines.ToArray());
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