using System;
using Cysharp.Threading.Tasks;
using Haare.Client.Routine;
using Haare.Scripts.Client.UI.Animator;
using Haare.Util.LogHelper;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Haare.Client.UI
{
    public class CustomButton : MonoRoutine,
        IPointerClickHandler,  
        IPointerDownHandler,  
        IPointerExitHandler,
        IPointerEnterHandler
    {
        [Header("Text Field")] public CustomText ButtonText;
        [Header("Image Field")] public CustomImage ButtonImage;
        
        public Subject<Unit> Onclicked { get; } = new Subject<Unit>();
        public Subject<Unit> Onhovered { get; }= new Subject<Unit>();
        public Subject<Unit> Onexited { get; }= new Subject<Unit>();
        
        [Header("Option Field")]
        [SerializeField]
        public bool OPTION_HOVERIMAGE = true ;
        [SerializeField]
        public bool OPTION_ANIMATION = false;
        
        [SerializeField] public bool HOVERANIMATION = false;
        [SerializeField] public float hoverScale = 1.1f;
        [SerializeField] public float hoverDuration = 0.2f;

        [SerializeField] public bool CLICKANIMATION = false;
        [SerializeField] public float clickPunchScale = 0.3f;
        [SerializeField] public float clickDuration = 0.1f;

        
        private UIAnimator _animator;
        
        public override UniTask Initialize()
        {
            ButtonImage = GetComponentInChildren<CustomImage>();
            ButtonText = GetComponentInChildren<CustomText>();
            if (OPTION_ANIMATION)
            {
                _animator = new UIAnimator(
                    this.gameObject
                    );
            }
            return base.Initialize();
        }

        public override UniTask Finalize()
        {
            if (OPTION_ANIMATION)
                _animator.KillAllTweens();
            
            return base.Finalize();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Onclicked.OnNext(Unit.Default);

        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (OPTION_ANIMATION)
            { 
                if(CLICKANIMATION)
                    _animator.TriggerClick(clickPunchScale, clickDuration);
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            Onexited.OnNext(Unit.Default);
            if (OPTION_HOVERIMAGE)
            {
                ButtonImage.ChangeCommonImage();
            }

            if (OPTION_ANIMATION)
            {
                if(HOVERANIMATION)
                    _animator.TriggerHoverExit(hoverDuration);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Onhovered.OnNext(Unit.Default);
            if(OPTION_HOVERIMAGE){
                ButtonImage.ChangeHoverImage();  
            }
            if (OPTION_ANIMATION){
                if(HOVERANIMATION)
                    _animator.TriggerHoverEnter(hoverScale,hoverDuration);
            }
        }

    }
}