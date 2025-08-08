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
using VContainer;
using VContainer.Unity;

namespace  Demo.Script
{
 
    public class DemoBootMono : MonoRoutine
    {
        [Inject] private ISceneRoutine _sceneRoutine; // VContainer가 주입
        public override async UniTask Initialize()
        {
            await base.Initialize();
            Debug.Log("DemoBootInit");
            ChangeSceneLog().Forget();
        }

        private async UniTask ChangeSceneLog()
        {     
            //await UniTask.Delay(TimeSpan.FromSeconds(1));
            //sDemoNative native = new DemoNative();
            //await UniTask.Delay(TimeSpan.FromSeconds(3));
            
            
            _sceneRoutine.LoadSceneWithLoad(SceneName.LobbyScene);
        }
    }
       
}