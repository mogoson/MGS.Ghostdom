/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Collection.cs
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
    public class Collection : MonoBehaviour
    {
        [SerializeField]
        protected GameObject prefab;

        [SerializeField]
        protected Transform collection;

        public void RequireItems(int count)
        {
            var items = collection.childCount - 1;
            while (items > count)
            {
                Destroy(collection.GetChild(items).gameObject);
                items--;
            }
            while (items < count)
            {
                Instantiate(prefab, collection);
                items++;
            }
        }

        public GameObject CreateItem()
        {
            return Instantiate(prefab, collection);
        }

        public T CreateItem<T>()
        {
            return CreateItem().GetComponent<T>();
        }

        public GameObject GetItem(int index)
        {
            return collection.GetChild(index + 1).gameObject;
        }

        public T GetItem<T>(int index)
        {
            return GetItem(index).GetComponent<T>();
        }
    }
}