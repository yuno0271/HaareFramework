using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Haare.Client.Singleton;


namespace Haare.Client.Routine.SceneRoutine
{   
    public enum SceneName
    {
        BootScene,
        LoadScene,
        LobbyScene
    }
    
    public enum SceneLoadPhase
    {
        UnloadCurrent,
        StartLoad,
        Loading,
        EndLoad
    }
    
    /// <summary>
    /// Scene의 변경에 대한 모든 관리가 진행되는 Routine -> Singleton입니다
    /// </summary>
    public class SceneRoutine :  ISceneRoutine
    {  
        public bool isInSceneOnly => false;

        private readonly ReactiveProperty<SceneLoadPhase> currentPhaseReactive = new ReactiveProperty<SceneLoadPhase>(SceneLoadPhase.UnloadCurrent);

        public ReactiveProperty<SceneLoadPhase> CurrentPhase => currentPhaseReactive;

        /// <summary>
        /// 초기화
        /// </summary>
        public async UniTask Initialize()
        {
            //await base.Initialize();
            Debug.Log("SceneRoutine Initialize");
        }
        
        /// <summary>
        /// Load를 끼는 씬이동
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        public async void LoadSceneWithLoad(SceneName scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneLoadRequest req = new SceneLoadRequest(SceneName.LoadScene,mode,scene);
            await LoadSceneInternal(req);
        }
        /// <summary>
        /// 직접적 씬이동 (LoadScene이 아닌이상 비추천)
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        public async void LoadScene(SceneName scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            
            SceneLoadRequest req = new SceneLoadRequest(scene,mode);
            await LoadSceneInternal(req);
        }
        
        /// <summary>
        /// Scene 이동의 공통처리
        /// </summary>
        /// <param name="request"></param>
        private async UniTask LoadSceneInternal(SceneLoadRequest request)
        {
            Debug.Log(">>> Phase: StartLoad");
            currentPhaseReactive.Value = SceneLoadPhase.StartLoad;
            
            UnityAction<Scene, LoadSceneMode> sceneLoaded = (loadedScene, sceneMode) =>
            {
                OnSceneLoadedHandler(loadedScene, request.Argument);
            };

            SceneManager.sceneLoaded += sceneLoaded;
            Debug.Log(">>> Phase: Loading");
            currentPhaseReactive.Value = SceneLoadPhase.Loading;

            await SceneManager.LoadSceneAsync(request.Scene.ToString(), request.Mode);
            
            Debug.Log(">>> Phase: EndLoad");
            currentPhaseReactive.Value = SceneLoadPhase.EndLoad;
            
            SceneManager.sceneLoaded -= sceneLoaded;
        }
        
        /// <summary>
        /// Scene이 새로 로드되었을때 ISceneWasLoaded 상속클래스들의 공통 트리거 마련
        /// </summary>
        /// <param name="loadedscene"></param>
        /// <param name="Argument"></param>
        private void OnSceneLoadedHandler(Scene loadedscene, object Argument)
        {
            foreach (var root in loadedscene.GetRootGameObjects())
            {
                ExecuteEvents.Execute<ISceneWasLoaded>(root, null,
                    (receiver, e) => receiver.OnSceneWasLoaded(Argument));
            }
        }
    }
        
}
