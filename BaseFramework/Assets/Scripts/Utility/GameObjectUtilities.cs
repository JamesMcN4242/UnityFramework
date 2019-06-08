////////////////////////////////////////////////////////////
/////   GameObjectUtilities.cs
/////   James McNeil - 2019
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtilities {

    /// <summary>
    /// Find a child with the specified name
    /// Searches for deep children as well
    /// </summary>
    /// <param name="go">Gameobject calling method</param>
    /// <param name="childName">name of the child</param>
    /// <returns>First instance of a child with the specified name, or null if not found</returns>
    public static GameObject FindChildByName(this GameObject go, string childName)
    {
        for(int i = 0; i < go.transform.childCount; i++)
        {
            GameObject child = go.transform.GetChild(i).gameObject;

            if(child.name == childName)
            {
                return child;
            }
            else
            {
                GameObject childObj = child.FindChildByName(childName);
                if(childObj != null)
                {
                    return childObj;
                }
            }
        }

        return null;
    }
}
