using Haare.Client.Routine;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Haare.Client.UI
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