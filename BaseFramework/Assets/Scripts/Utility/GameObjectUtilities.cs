////////////////////////////////////////////////////////////
/////   GameObjectUtilities.cs
/////   James McNeil - 2019
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to hold utility methods and GameObject extension methods
/// </summary>
public static class GameObjectUtilities
{

    /// <summary>
    /// Find a child with the specified name
    /// Searches for deep children as well
    /// </summary>
    /// <param name="go">Gameobject calling method</param>
    /// <param name="childName">name of the child</param>
    /// <returns>First instance of a child with the specified name, or null if not found</returns>
    public static GameObject FindChildByName(this GameObject go, string childName)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            GameObject child = go.transform.GetChild(i).gameObject;

            if (child.name == childName)
            {
                return child;
            }
            else
            {
                GameObject childObj = child.FindChildByName(childName);
                if (childObj != null)
                {
                    return childObj;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Get a component from a specified child object
    /// </summary>
    /// <typeparam name="T">Component Type</typeparam>
    /// <param name="go">Calling GameObject</param>
    /// <param name="childName">Name of child</param>
    /// <returns>Found component, or null if child doesn't exist or contain component type T</returns>
    public static T GetComponentFromChild<T>(this GameObject go, string childName) where T : Component
    {
        GameObject child = go.FindChildByName(childName);

        if(child != null)
        {
            return child.GetComponent<T>();
        }

        return null;
    }

    /// <summary>
    /// Remove the given component type if it exists on the object
    /// </summary>
    /// <typeparam name="T">Component type to destroy</typeparam>
    /// <param name="go">Calling Gameobject</param>
    public static void RemoveComponent<T>(this GameObject go) where T : Component
    {
        T componentToRemove = go.GetComponent<T>();
        if(componentToRemove != null)
        {
           Object.Destroy(componentToRemove);
        }
    }

    /// <summary>
    /// Remove the given component type from a specified child object (if both the child and object exist)
    /// </summary>
    /// <typeparam name="T">Component of type to destroy</typeparam>
    /// <param name="go">Calling Gameobject</param>
    /// <param name="childName">Name of child to remove component from</param>
    public static void RemoveComponentFromChild<T>(this GameObject go, string childName) where T : Component
    {
        T componentToRemove = go.GetComponentFromChild<T>(childName);
        if(componentToRemove != null)
        {
            Object.Destroy(componentToRemove);
        }
    }
}
