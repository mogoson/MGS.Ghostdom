/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Chaos.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  10/22/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.IO;
using UnityEditor;
using UnityEngine;

namespace MGS.Ghostdoms.Editors
{
    public class Chaos
    {
        [MenuItem("Tool/Ghostdom/Active")]
        static void Active()
        {
            Reshape(true);
            Debug.Log("Ghostdom is actived.");
        }

        [MenuItem("Tool/Ghostdom/Inactive")]
        static void Inactive()
        {
            Reshape(false);
            Debug.Log("Ghostdom is inactived.");
        }

        static void Reshape(bool isActive)
        {
            ReshapeGhostdom(isActive);
            ReshapeTartarus(isActive);
            AssetDatabase.Refresh();
        }

        static void ReshapeGhostdom(bool isActive)
        {
            var ghostdom = string.Format("{0}/MGS.Packages/Ghostdom/", Application.dataPath);
            var inactive = string.Format("{0}/Editor/Resources", ghostdom);
            var active = string.Format("{0}/Resources", ghostdom);
            if (isActive)
            {
                if (!Directory.Exists(active))
                {
                    Directory.Move(inactive, active);
                }
            }
            else
            {
                if (!Directory.Exists(inactive))
                {
                    Directory.Move(active, inactive);
                }
            }
        }

        static void ReshapeTartarus(bool isActive)
        {
            var tartarus = string.Format("{0}/MGS.Packages/Ghostdom/Runtime/Tartarus.cs", Application.dataPath);
            var enlivener = File.ReadAllText(tartarus);
            var activeOracle = "#define GHOSTDOM_ACTIVE";
            var inActiveOracle = "#define GHOSTDOM_INACTIVE";
            if (isActive)
            {
                enlivener = enlivener.Replace(inActiveOracle, activeOracle);
            }
            else
            {
                enlivener = enlivener.Replace(activeOracle, inActiveOracle);
            }
            File.WriteAllText(tartarus, enlivener);
        }
    }
}