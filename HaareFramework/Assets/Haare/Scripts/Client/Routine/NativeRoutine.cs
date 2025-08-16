using System;
using Cysharp.Threading.Tasks;

using R3;
using UnityEngine;


using Haare.Client.Core;
using Haare.Util.LogHelper;

namespace Haare.Client.Routine
{
    public class NativeRoutine : INativeRoutine , IDisposable
    {
        public virtual bool isRegistered => true;
        public virtual bool isInSceneOnly { get; protected set; }
        public bool isInitialized { get; private set; }

        public Func<UniTask> Oninitialize { get; protected set;} = async () => await UniTask.CompletedTask;
        public Func<UniTask> Onfinalize { get; protected set;} = async () => await UniTask.CompletedTask;
        public Subject<Unit> Onupdate { get; protected set; } = new Subject<Unit>();
        
        protected NativeRoutine() {
            Constructor().Forget();
        }
        async UniTask Constructor() {
            if ( !isRegistered )	{ return; }	
            
            LogHelper.Log(LogHelper.FRAMEWORK,"Custruct of Native Routine");
            await UniTask.Delay( 1 );
            await UniTask.WaitUntil( () => Processer.Instance.isInitialized );
            await Processer.Instance.Register( this );
        }

        
        public virtual async UniTask Initialize()
        {
            await Oninitialize();
            isInitialized = true;
        }
        public virtual void UpdateProcess() {

        }
        
        public void Dispose()
        {
            Finalize().Forget();
        }

        public virtual async UniTask Finalize()
        {
            await Onfinalize();
        }
    }
}