using UnityEngine;

namespace Haare.Util.Prefab
{
    public static class PrefabUtil
    {
        public static T InstantiatePrefab<T>(Transform parent, PrefabParam param) where T : Component
        {
            var prefab = Resources.Load(param.PrefabPath) as GameObject;
            prefab.SetActive(false);
            var go = GameObject.Instantiate(prefab);
            go.transform.SetParent(parent, false);
            return go.GetComponent<T>();
        }
    }
    
    public class PrefabParam
    {
        public readonly string PrefabPath;

        public PrefabParam(string prefabPath)
        {
            PrefabPath = prefabPath;
        }
    }
}