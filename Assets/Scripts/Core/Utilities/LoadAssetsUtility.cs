using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssetsLoader
{
    T Load<T>(string path) where T:Object;
}

public class LoadAssetsUtility
{
    private static IAssetsLoader loader;
    static LoadAssetsUtility()
    {
#if UNITY_EDITOR
        loader = new ResoucesLoader();
#else
        loader = new AssetsBundleLoader();
#endif
    }
    public static T Load<T>(string path) where T : Object
    {
        return loader.Load<T>(path);
    }
}

public class ResoucesLoader : IAssetsLoader
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
}
public class AssetsBundleLoader : IAssetsLoader
{
    //todo
    public T Load<T>(string path) where T : Object
    {
        throw new System.NotImplementedException();
    }
}
