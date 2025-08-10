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
 
    public class DemoTitleMono : MonoRoutine
    {
     
        public override async UniTask Initialize()
        {
            await base.Initialize();
            Debug.Log("DemoBootInit");
        }
        
    }
       
}