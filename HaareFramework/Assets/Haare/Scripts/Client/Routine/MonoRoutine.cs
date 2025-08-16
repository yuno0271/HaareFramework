using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using Haare.Client.Core;
using Haare.Util.LogHelper;

namespace Haare.Client.Routine
{
    /// <summary>
    ///  Gameobject에 할당이 가능한 Routine
    /// 일반적인 MonoBehaviour 와 동일한 역할입니다.
    /// </summary>
    public class MonoRoutine : MonoBehaviour ,IRoutine
    {
        public virtual bool isRegistered => true;
        public virtual bool isInSceneOnly { get; protected set; }
        public bool isInitialized { get; private set; }
        public Func<UniTask> Oninitialize { get; protected set; } = async () => await UniTask.CompletedTask;
        public Func<UniTask> Onfinalize { get; protected set;} = async () => await UniTask.CompletedTask;
        public Subject<Unit> Onupdate { get; protected set; } = new Subject<Unit>();
        public Subject<Unit> OnLateupdate { get; protected set; } = new Subject<Unit>();
        public Subject<Unit> OnFixedupdate { get; protected set; } = new Subject<Unit>();
        
        public CompositeDisposable disposables = new CompositeDisposable();
        private bool _isFinalized = false; 

        protected async void Awake()
        {
            if ( !isRegistered ){ return; }	
            Constructor();

            await UniTask.WaitUntil( () => Processer.Instance.isInitialized);
           
            await Processer.Instance.Register(this);

            disposables.Add(Processer.Instance.PROCESSING.Subscribe(_ =>
            {
                if (_)
                {
                    OnRestartProcess();
                }
                else
                {
                    OnStopProcess();
                }
            }));

            
            disposables.Add(Processer.Instance.Onupdate.Subscribe( _ => {
                UpdateProcess();
            }));
            disposables.Add(Processer.Instance.OnLateupdate.Subscribe( _ => {
                LateUpdateProcess();
            }));           
            disposables.Add(Processer.Instance.OnFixedupdate.Subscribe( _ => {
                FixedUpdateProcess();
            }));
        }
        
        
        protected virtual void Constructor() {
            
        }
        public virtual async UniTask Initialize()
        {
            await Oninitialize();
            isInitialized = true;
        }
        protected virtual void OnStopProcess()
        {
            
        }
        protected virtual void OnRestartProcess()
        {
            
        }
        protected virtual void UpdateProcess() {
            Onupdate.OnNext( Unit.Default );
        }
        protected virtual void LateUpdateProcess() {
            OnLateupdate.OnNext( Unit.Default );
        }  
        protected virtual void FixedUpdateProcess() {
            OnFixedupdate.OnNext( Unit.Default );
        }
        
        public virtual async UniTask Finalize()
        {
            if (_isFinalized) return;
            _isFinalized = true;
            disposables.Clear();
            await Onfinalize();
            LogHelper.Log(LogHelper.FRAMEWORK,this.GetType()+" Disposed");
        }
        private void OnApplicationQuit()
        {
            disposables.Clear();
        }
        private void OnDestroy()
        {
            if (_isFinalized) return;
            
            if (Processer.isCreated)
            {
                Processer.Instance.UnRegister(this);
            }
            LogHelper.Log(LogHelper.FRAMEWORK,this.GetType()+" Distroyed");
        }

 
    }
}