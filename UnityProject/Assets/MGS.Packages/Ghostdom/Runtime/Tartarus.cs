/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Tartarus.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/22/2022
 *  Description  :  Initial development version.
 *************************************************************************/

#define GHOSTDOM_ACTIVE

using UnityEngine;
using UnityEngine.EventSystems;

namespace MGS.Ghostdoms
{
    public class Tartarus
    {
#if GHOSTDOM_ACTIVE
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Awake()
        {
            var ghostdom = Object.FindObjectOfType<Ghostdom>();
            if (ghostdom == null)
            {
                var prefab = Resources.Load<Ghostdom>("Ghostdom");
                ghostdom = Object.Instantiate(prefab);
            }
            //ghostdom.gameObject.hideFlags = HideFlags.HideInHierarchy;
            Object.DontDestroyOnLoad(ghostdom);

            var eventSystem = Object.FindObjectOfType<EventSystem>();
            if (eventSystem == null)
            {
                eventSystem = new GameObject("EventSystem").AddComponent<EventSystem>();
                eventSystem.gameObject.AddComponent<StandaloneInputModule>();
            }
            Object.DontDestroyOnLoad(eventSystem);
        }
#endif
    }
}