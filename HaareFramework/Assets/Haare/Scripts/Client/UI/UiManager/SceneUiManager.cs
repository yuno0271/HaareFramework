using System;
using System.Collections.Generic;
using Haare.Client.Routine;
using Haare.Client.UI.Panel;
using Haare.Util.Prefab;
using UnityEngine;

namespace Haare.Client.UI.UiManager
{
    public abstract class SceneUiManager : MonoRoutine
    {
        private readonly Dictionary<Type, IBasePanel> PanelDic = new Dictionary<Type, IBasePanel>();
        private readonly Stack<TypePanel> TypePanelStack;
        protected abstract Dictionary<Type, PrefabParam> GetPrefabParamDic();

        public void RegisterPanel<T>() where T : IBasePanel
        {
            
        }
        
        /// <summary>
        /// IBasePanel의 파생을 가져오기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T RentPanel<T>() where T : Component, IBasePanel
        {
            var pageTypeToRent = typeof(T);

            if (!PanelDic.ContainsKey(pageTypeToRent))
            {
                Register<T>();
            }

            return PanelDic[pageTypeToRent] as T;
        }
        /// <summary>
        /// Register Panel!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private  T Register<T>() where T : Component, IBasePanel
        {
            var pageTypeToRegister = typeof(T);
            var param = GetPrefabParamDic()[pageTypeToRegister];
            var component = PrefabUtil.InstantiatePrefab<T>(this.transform, param);

            component.SetSceneUiManager(this);

            if (PanelDic.ContainsKey(pageTypeToRegister))
            {
                throw new Exception(string.Format("Already Exist UIPage:{0} name:{1}", 
                    pageTypeToRegister, component.name));
            }
            else
            {
                PanelDic.Add(pageTypeToRegister, component);
            }
            return component;
        }
        
        /// <summary>
        /// get PeekPanel Type
        /// </summary>
        /// <returns></returns>
        public Type PeekPanel()
        {
            if (TypePanelStack.Count ==0)
            {
                return null;
            }
            return TypePanelStack.Peek().PageType;
        }
        
        /// <summary>
        /// OpenPanel
        /// </summary>
        /// <param name="isOverlay"></param>
        /// <typeparam name="T"></typeparam>
        public void OpenPanel<T>(bool isOverlay = false) where T : IBasePanel
        {
            var PageTypeToStack = typeof(T);
        }
        
        private class TypePanel
        {
            public readonly Type PageType;
            public readonly bool IsOverlay;

            public TypePanel(Type pageType, bool isOverlay)
            {
                PageType = pageType;
                IsOverlay = isOverlay;
            }

        }
    }
}