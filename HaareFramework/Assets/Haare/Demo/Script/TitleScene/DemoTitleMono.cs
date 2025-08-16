
using Cysharp.Threading.Tasks;
using Haare.Client.Routine;
using Haare.Util.LogHelper;
using UnityEngine;

namespace  Demo.TitleScene
{
 
    public class DemoTitleMono : MonoRoutine
    {
     
        public override async UniTask Initialize()
        {
            await base.Initialize();
            LogHelper.Log(LogHelper.DEMO,"DemoBootInit");
        }
        
    }
       
}