/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Collector.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/22/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Ghostdoms
{
    public class Collector : MonoBehaviour
    {
        public GameObject prefab;

        [SerializeField]
        protected Transform locker;

        [SerializeField]
        protected int reserved = 1;

        public void RequireItems(int count)
        {
            var items = locker.childCount - reserved;
            while (items > count)
            {
                Destroy(locker.GetChild(items).gameObject);
                items--;
            }
            while (items < count)
            {
                Instantiate(prefab, locker);
                items++;
            }
        }

        public GameObject CreateItem()
        {
            return Instantiate(prefab, locker);
        }

        public T CreateItem<T>()
        {
            return CreateItem().GetComponent<T>();
        }

        public GameObject GetItem(int index)
        {
            return locker.GetChild(index + reserved).gameObject;
        }

        public T GetItem<T>(int index)
        {
            return GetItem(index).GetComponent<T>();
        }
    }
}