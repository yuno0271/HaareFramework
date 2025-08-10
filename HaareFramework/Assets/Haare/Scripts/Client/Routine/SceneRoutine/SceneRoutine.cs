using System;
using System.Threading.Tasks;
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
        TitleScene,
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
    public class SceneRoutine : NativeRoutine
    {  
        
        SceneName sceneToUnload;
        
        public override bool isInSceneOnly => false;

        private readonly ReactiveProperty<SceneLoadPhase> currentPhaseReactive = new ReactiveProperty<SceneLoadPhase>(SceneLoadPhase.UnloadCurrent);

        public ReactiveProperty<SceneLoadPhase> CurrentPhase => currentPhaseReactive;
        
        private readonly ReactiveProperty<float> _loadProgress = new ReactiveProperty<float>(0f);
        public ReadOnlyReactiveProperty<float> LoadProgress => _loadProgress;

        public SceneLoadRequest LoadSceneRequest;
        /// <summary>
        /// 초기화
        /// </summary>
        public override async UniTask Initialize()
        {
            //await base.Initialize();
            Debug.Log("SceneRoutine Initialize");
            await UniTask.CompletedTask;
        }
        
        /// <summary>
        /// Load를 끼는 씬이동
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        public async UniTask LoadSceneWithLoad(SceneName scene, LoadSceneMode mode = LoadSceneMode.Additive)
        {
            SceneLoadRequest req = new SceneLoadRequest(SceneName.LoadScene,mode,scene);
            LoadSceneRequest = new  SceneLoadRequest(scene,mode);
            await LoadSceneInternal(req,false);
        }
        /// <summary>
        /// 직접적 씬이동 (LoadScene이 아닌이상 비추천)
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        public async void LoadScene()
        {
            await LoadSceneInternal(LoadSceneRequest,true);
        }
        
        /// <summary>
        /// Scene 이동의 공통처리
        /// </summary>
        /// <param name="request"></param>
        private async UniTask LoadSceneInternal(SceneLoadRequest request,bool withLoad)
        {
            Debug.Log(">>> Phase: StartLoad");
            currentPhaseReactive.Value = SceneLoadPhase.StartLoad;

            if(request.Mode==LoadSceneMode.Additive){
                if (Enum.TryParse<SceneName>(SceneManager.GetActiveScene().name, out var initialScene))
                {
                     sceneToUnload = initialScene;
                }
            }
            
            
            Debug.Log(">>> Phase: Loading");
            UnityAction<Scene, LoadSceneMode> sceneLoaded = (loadedScene, sceneMode) =>
            {
                OnSceneLoadedHandler(loadedScene, request.Argument);
            };
            SceneManager.sceneLoaded += sceneLoaded;
            
            currentPhaseReactive.Value = SceneLoadPhase.Loading;

            var loadOperation = SceneManager.LoadSceneAsync(request.Scene.ToString(), request.Mode);
            loadOperation.allowSceneActivation = false;

            if(withLoad){
                var loadSceneTask = LoadSceneProgressTask(loadOperation);
                var minTimeTask = FakeLoadingProgressTask(1.5f);
                await UniTask.WhenAll(loadSceneTask , minTimeTask);
            }
            else
            {
                await LoadSceneProgressTask(loadOperation);
            }
            
            await UniTask.Delay(1000); // 1초 대기
            loadOperation.allowSceneActivation = true;
            await loadOperation; // 씬 활성화가 완료될 때까지 대기

            Debug.Log(">>> UnLoadCurrent Scene");
            if (request.Mode==LoadSceneMode.Additive&&
                SceneManager.GetSceneByName(sceneToUnload.ToString()).isLoaded)
            {
                await SceneManager.UnloadSceneAsync(sceneToUnload.ToString());
            }
            Debug.Log(">>> Phase: EndLoad");
            currentPhaseReactive.Value = SceneLoadPhase.EndLoad;
            
            SceneManager.sceneLoaded -= sceneLoaded;
        }

        private async UniTask LoadSceneProgressTask(AsyncOperation loadOperation)
        {
            while (loadOperation.progress < 0.9f)
            {
                var progress = loadOperation.progress / 0.9f;
//                _loadProgress.Value = progress;

                await UniTask.Yield();
            } 
            //          _loadProgress.Value = 1f; 
        }
        /// <summary>
        /// 지정된 시간 동안 _loadProgress 값을 0에서 1로 부드럽게 증가시키는 Task입니다.
        /// </summary>
        private async UniTask FakeLoadingProgressTask(float duration)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                _loadProgress.Value = Mathf.Clamp01(elapsedTime / duration);
                await UniTask.Yield();
            }
            _loadProgress.Value = 1f;
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
