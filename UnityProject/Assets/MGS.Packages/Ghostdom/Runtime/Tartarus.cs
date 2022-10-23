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
            var eyes = Object.FindObjectOfType<Ghostdom>();
            if (eyes == null)
            {
                var prefab = Resources.Load<Ghostdom>("Ghostdom");
                eyes = Object.Instantiate(prefab);
            }
            //eyes.gameObject.hideFlags = HideFlags.HideInHierarchy;
            Object.DontDestroyOnLoad(eyes);

            var eSys = Object.FindObjectOfType<EventSystem>();
            if (eSys == null)
            {
                eSys = new GameObject("EventSystem").AddComponent<EventSystem>();
                eSys.gameObject.AddComponent<StandaloneInputModule>();
            }
            Object.DontDestroyOnLoad(eSys);
        }
#endif
    }
}