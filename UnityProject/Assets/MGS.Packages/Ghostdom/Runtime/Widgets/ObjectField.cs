/*************************************************************************
 *  Copyright © #COPYRIGHTYEAR# Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompntField.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  #CREATEDATE#
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
        protected Object obj;

        public void Refresh(Object obj)
        {
            this.obj = obj;
            nameTxt.text = obj.GetType().Name;
            var properties = obj.GetType().GetProperties();
            Refresh(properties);
        }

        protected void Refresh(ICollection<PropertyInfo> properties)
        {
            collector.RequireItems(properties.Count);

            var i = 0;
            foreach (var property in properties)
            {
                var item = collector.GetItem<PropertyField>(i);
                item.Refresh(property);
                item.OnValueChanged = Property_OnValueChanged;
                item.gameObject.SetActive(true);
                i++;
            }
        }

        protected void Property_OnValueChanged(PropertyInfo property, object value)
        {
            property.SetValue(obj, value, null);
        }
    }
}