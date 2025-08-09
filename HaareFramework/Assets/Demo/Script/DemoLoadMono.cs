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
 
    public class DemoLoadMono : MonoRoutine
    {
        public override async UniTask Initialize()
        {
            await base.Initialize();
            UnityEngine.Debug.Log("DemoLoadInit");
            ChangeSceneLog().Forget();
        }

        private async UniTask ChangeSceneLog()
        {     
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            DemoNative native = new DemoNative();
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            //SceneRoutine.Instance.LoadScene(SceneName.LobbyScene);
        }
    }
       
}