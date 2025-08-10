using System;
using System.Collections;
using System.Collections.Generic;
using Haare.Client.Routine;
using Haare.Client.UI.HaareImage;
using Haare.Client.UI.HaareText;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Haare.Client.UI.HaareButton
{
    public class CustomButton : MonoRoutine,
        IPointerClickHandler,  
        IPointerDownHandler,  
        IPointerExitHandler  
    {
        [Header("Text Field")] public CustomText ButtonText;
        [Header("Image Field")] public CustomImage ButtonImage;
        
        public Subject<Unit> Onclicked { get; } = new Subject<Unit>();
        public Subject<Unit> Onhovered { get; }= new Subject<Unit>();
        public Subject<Unit> Onexited { get; }= new Subject<Unit>();
        
        
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