using System;
using System.Collections;
using System.Collections.Generic;
using Haare.Client.Routine;
using Haare.Client.UI.BaseImage;
using Haare.Client.UI.BaseText;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Haare.Client.UI.BaseButton
{
    public class BaseButton : MonoRoutine,
        IBaseButton,
        IPointerClickHandler,  
        IPointerDownHandler,  
        IPointerExitHandler  
    {
        [Header("Text Field")] public IBaseText ButtonText;
        [Header("Image Field")] public IBaseImage ButtonImage;
        
        public Subject<Unit> Onclicked { get; }
        public Subject<Unit> Onhovered { get; }
        public Subject<Unit> Onexited { get; }
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Onclicked.OnNext(Unit.Default);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            Onhovered.OnNext(Unit.Default);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            Onexited.OnNext(Unit.Default);
        }
    }
}