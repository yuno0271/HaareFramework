using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Haare.Client.Routine.SceneRoutine;
using Haare.Client.Routine;
using Haare.Util;
using R3;
using UnityEngine;

namespace  Demo.Script
{
 
    public class DemoNative : NativeRoutine
    {
        public override bool isInSceneOnly => true;

        public override async UniTask Initialize()
        {
            await base.Initialize();
            Debug.Log("DemoNativeInit");
        }

        public override void UpdateProcess()
        {
            //CustomLogger.Log("DemoNative Update");
        }

        public override async UniTask Finalize()
        {
            Debug.Log("Finished DemoNative");
        }
    }
       
}