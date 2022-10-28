/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ObjectField.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/27/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Ghostdoms
{
    public class ObjectField : MonoBehaviour
    {
        [SerializeField]
        protected Text nameTxt;

        [SerializeField]
        protected Collector collector;
        protected object obj;

        public void Refresh(object obj)
        {
            this.obj = obj;
            nameTxt.text = obj.GetType().Name;
            var properties = obj.GetType().GetProperties();
            Refresh(properties);
        }

        protected void Refresh(ICollection<PropertyInfo> properties)
        {
            var vs = new List<object>();
            var ps = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(Matrix4x4))
                {
                    continue;
                }

                try
                {
                    vs.Add(property.GetValue(obj, null));
                    ps.Add(property);
                }
                catch { continue; }
            }

            collector.RequireItems(ps.Count);

            var i = 0;
            foreach (var property in ps)
            {
                var value = property.GetValue(obj, null);
                var item = collector.GetItem<PropertyField>(i);
                item.Refresh(property, vs[i]);
                item.OnValueChanged = Property_OnValueChanged;
                item.gameObject.SetActive(true);
                i++;
            }
        }

        protected void Property_OnValueChanged(PropertyInfo property, object value)
        {
            try
            {
                property.SetValue(obj, value, null);
            }
            catch { }
        }
    }
}