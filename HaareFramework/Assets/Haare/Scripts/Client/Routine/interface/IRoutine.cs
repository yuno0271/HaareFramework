using System;
using Cysharp.Threading.Tasks;

namespace Haare.Client.Routine
{
    public interface IRoutine
    {
        //Processer Register?
        bool isRegistered	{ get; }
        //SceneOnly
        bool isInSceneOnly { get; }
        //Init?
        bool isInitialized	{ get; }
        
        Func<UniTask> Oninitialize  { get; }
        Func<UniTask> Onfinalize	{ get; }
        
        UniTask Initialize();
        UniTask Finalize();
    }
}