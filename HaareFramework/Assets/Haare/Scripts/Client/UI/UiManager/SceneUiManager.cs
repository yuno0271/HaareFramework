using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Script.UI;
using Haare.Client.Routine;
using Haare.Client.UI.Panel;
using Haare.Util.Prefab;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Haare.Client.UI.UiManager
{
    public abstract class SceneUIManager : MonoRoutine
    {
        // UI Manager 가 시현한 Panel들
        private readonly Dictionary<PanelType, ICustomPanel> PanelDic =
            new Dictionary<PanelType, ICustomPanel>();
        
        // UI Manager 가 시현중인 Panel이지만 Stack로 관리되는 개체들
        private readonly Stack<PanelType> TypePanelStack = new Stack<PanelType>();

        public Subject<ICustomPanel> OnOpenedNewPannel { get; } = new Subject<ICustomPanel>();
        
        /// <summary>
        /// ICustomPanel의 파생을 가져오기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T RentPanel<T>(int instanceID = 0) where T : Component, ICustomPanel
        {
            var findRentPanel = typeof(T);
            if (instanceID != 0)
            {
                return PanelDic[GetKeybyinstanceID<T>(instanceID)]as T;
            }
            else
            {
                return PanelDic[GetKeybyinstanceID<T>()]as T;
            }
        }
        
        /// <summary>
        /// Register Panel!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private T Register<T>() where T : Component, ICustomPanel
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
        public async Task<int> OpenPanel<T>(bool isOverlay = false,bool isStack = true) where T : Component, ICustomPanel
        {
            var pageType = typeof(T);

            // 1. 새 패널 가져오기 (없으면 생성)
            var panel = Register<T>();
            
            int instanceID = panel.GetInstanceID();
            var panelType = new PanelType(pageType, instanceID, isStack);
            // 3. 새 패널 보이기
            panel.OpenPanel();
            // 4. 스택에 새 패널 정보 저장
            PanelDic.Add(panelType,panel);

            if(isStack)
                TypePanelStack.Push(panelType);
            
            OnOpenedNewPannel.OnNext(panel);
            return instanceID;
        }
        
        /// <summary>
        /// ClosePanel
        /// </summary>
        public void ClosePanel<T>(bool isOverlay = false) where T : Component, ICustomPanel
        { 
            var panel = RentPanel<T>();
            panel.ClosePanel();
            TypePanelStack.Pop();
            
            PanelDic.Remove(GetKeybyinstanceID<T>());
            if (TypePanelStack.Count == 0)
            {
                Debug.Log("Empty UI Panel");
            }
            else
            {
                Debug.Log("UI Stack Count"+TypePanelStack.Count);
            }
            if (PanelDic.Count == 0)
            {
                Debug.Log("Empty UI Dic");
            }
            else
            {
                foreach (var kv in PanelDic)
                {
                    Debug.Log(($"DICT : {kv.Key} : {kv.Value}"));
                }
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
                Debug.Log("Empty UI Panel");
            }
            if (PanelDic.Count == 0)
            {
                Debug.Log("Empty UI Panel");
            }
            foreach (var item in TypePanelStack)
            {
                Debug.Log(item);
            }
            foreach (var kv in PanelDic)
            {
                Debug.Log(($"{kv.Key} : {kv.Value}"));
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
  
    }
}