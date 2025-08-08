using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Haare.Client.Routine.SceneRoutine;
using Haare.Client;
using Haare.Client.Container;


public class HaareClient
{
    [RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSceneLoad )]
    static async void Main() {
        
        Debug.Log("Start Haare Framework");
        await Task.Delay( 1 );
        await Processer.WaitForCreation();
        
        var coreGo = new GameObject("CoreProcesser");
        Object.DontDestroyOnLoad(coreGo);
        var scope = coreGo.AddComponent<CoreLifetimeScope>(); 
        // VContainer 강제 초기화
        scope.Build(); 
        
        await Processer.Instance.Constructor(InitializePlugin, RegisterProcesses);
        
    }

    static async UniTask InitializePlugin() {
        await UniTask.Delay( 0 );
    }
    

    static async UniTask RegisterProcesses()
    {
        Debug.Log("RegisterProcesses");
        //await SceneRoutine.WaitForCreation();
  
        await UniTask.DelayFrame( 1 );
        Debug.Log("RegisterProcesses -> end");
    }
    
}
