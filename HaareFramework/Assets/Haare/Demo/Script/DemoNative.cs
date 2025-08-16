using Cysharp.Threading.Tasks;
using Haare.Client.Routine;
using Haare.Util.LogHelper;
using UnityEngine;

namespace Demo.Service
{
 
    public class DemoNative : NativeRoutine
    {
        public override bool isInSceneOnly => true;

        public override async UniTask Initialize()
        {
            await base.Initialize();
            LogHelper.Log(LogHelper.DEMO,"DemoNativeInit");
            await UniTask.CompletedTask;
        }

        public override void UpdateProcess()
        {
            //CustomLogger.Log("DemoNative Update");
        }

        public override async UniTask Finalize()
        {
            LogHelper.Log(LogHelper.DEMO,"Finished DemoNative");
            await UniTask.CompletedTask;
        }
    }
       
}