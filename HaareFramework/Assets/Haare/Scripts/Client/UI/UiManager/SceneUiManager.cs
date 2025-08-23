using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Haare.Client.Routine;
using Haare.Client.Routine.Service.SceneService;
using Haare.Util.LogHelper;
using Haare.Util.Prefab;
using R3;
using UnityEngine;

namespace Haare.Client.UI
{
    public abstract class SceneUIManager : MonoRoutine , ISceneWasLoaded
    {
        // UI Manager 가 시현한 Panel들
        private readonly Dictionary<PanelType, ICustomPanel> PanelDic =
            new Dictionary<PanelType, ICustomPanel>();
        
        // UI Manager 가 시현중인 Panel이지만 Stack로 관리되는 개체들
        private readonly Stack<PanelType> TypePanelStack = new Stack<PanelType>();

        public Subject<ICustomPanel> OnOpenedNewPannel { get; } = new Subject<ICustomPanel>();
        
        public bool ILoadedScene = false;

        
        /// <summary>
        /// ICustomPanel의 파생을 가져오기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T RentPanel<T>(int instanceID = 0) where T : Component, ICustomPanel
        {
            PanelType key;
            if (instanceID != 0)
            {
                key = GetKeybyinstanceID<T>(instanceID);
            }
            else
            {
                key = GetKeybyinstanceID<T>();
            }

            if (key == null)
            {
                return null;
            }
            var findRentPanel = typeof(T);
            return PanelDic[key]as T;
            
        }
        
        /// <summary>
        /// Register Panel!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private UniTask<T> Register<T>() where T : Component, ICustomPanel
        {
            var pageTypeToRegister = typeof(T);
            var param = PrefabPath.PrefabDict[pageTypeToRegister];
            var component = PrefabUtil.InstantiatePrefab<T>(this.transform, param);

            return component;
        }
        
        /// <summary>
        /// get PeekPanel Type Information
        /// </summary>
        /// <returns></returns>
        public ICustomPanel PeekPanel()
        {
            if (TypePanelStack.Count ==0)
            {
                return null;
            }
            return  PanelDic[TypePanelStack.Peek()];
        }
        
        /// <summary>
        /// OpenPanel
        /// </summary>
        /// <param name="isOverlay"></param>
        /// <typeparam name="T"></typeparam>
        public async UniTask<int> OpenPanel<T>(bool isOverlay = false,bool isStack = true) where T : Component, ICustomPanel
        {
            var pageType = typeof(T);

            // 1. 새 패널 가져오기 (없으면 생성)
            var panel = await Register<T>();
            
            int instanceID = panel.GetInstanceID();
            var panelType = new PanelType(pageType, instanceID, isStack);
            // 3. 새 패널 보이기
            panel.OpenPanel();
            // 4. 스택에 새 패널 정보 저장
            PanelDic.Add(panelType,panel);

            if(isStack)
                TypePanelStack.Push(panelType);
            
            OnOpenedNewPannel.OnNext(panel);
            
            //의미없는 처리 (향후 await 하는 로드로 변경할경우 고려)
            await UniTask.CompletedTask;
            return instanceID;
        }
        
        /// <summary>
        /// ClosePanel
        /// </summary>
        public void ClosePanel<T>(bool isOverlay = false) where T : Component, ICustomPanel
        { 
            var panel = RentPanel<T>();
            if (panel == null)
            {
                return;
            }
            panel.ClosePanel();
            TypePanelStack.Pop();
            
            PanelDic.Remove(GetKeybyinstanceID<T>());
            if (TypePanelStack.Count == 0)
            {
                LogHelper.Log(LogHelper.FRAMEWORK,"Empty UI Panel");
            }
            else
            {
                PanelDic[TypePanelStack.Peek()].ReloadPanel();
                //LogHelper.Log("UI Stack Count"+TypePanelStack.Count);
            }
            if (PanelDic.Count == 0)
            {
                LogHelper.Log(LogHelper.FRAMEWORK,"Empty UI Dic");
            }
            else
            {
                // foreach (var kv in PanelDic)
                // {
                //     LogHelper.Log(($"DICT : {kv.Key} : {kv.Value}"));
                // }
            }
            
            Destroy(panel.panel);
        }
            
        /// <summary>
        /// ClosePeekPanel
        /// </summary>
        public void ClosePeekPanel()
        {
            var panel = PeekPanel();
            panel.ClosePanel();
            TypePanelStack.Pop();
            PanelDic.Remove(TypePanelStack.Peek()); 
            
            if (TypePanelStack.Count == 0)
            {
                LogHelper.Log(LogHelper.FRAMEWORK,"Empty UI Panel");
            }
            if (PanelDic.Count == 0)
            {
                LogHelper.Log(LogHelper.FRAMEWORK,"Empty UI Panel");
            }
            foreach (var item in TypePanelStack)
            {
                LogHelper.Log(LogHelper.FRAMEWORK,$"{item}");
            }
            foreach (var kv in PanelDic)
            {
                LogHelper.Log(LogHelper.FRAMEWORK,($"{kv.Key} : {kv.Value}"));
            }
            Destroy(panel.panel);
        }

        private PanelType GetKeybyinstanceID<T>(int instanceID) where T : Component, ICustomPanel
        {
            var matchingEntry = PanelDic
                .Where(kv => kv.Key.pageType == typeof(T) && kv.Key.instanceId == instanceID)
                .LastOrDefault();
            
            return matchingEntry.Key;
        }
        private PanelType GetKeybyinstanceID<T>() where T : Component, ICustomPanel
        {
            var matchingEntry = PanelDic
                .Where(kv => kv.Key.pageType == typeof(T))
                .LastOrDefault();
            
            return matchingEntry.Key;
        }
        
        private class PanelType
        {
            public readonly Type pageType;
            public readonly bool isOverlay;
            public readonly int instanceId;
            public PanelType(Type _pageType,int _instanceId, bool _isOverlay )
            {
                pageType = _pageType;
                isOverlay = _isOverlay;
                instanceId = _instanceId;
            }

        }

        public void OnSceneWasLoaded(object argument)
        {  
            ILoadedScene = true;
        }
    }
}