using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Haare.Client.Routine;
using Haare.Client.UI.BaseImage;
using UnityEngine;
using UnityEngine.UI;

namespace Haare.Client.UI.BaseImage
{
    public class CustomImage : MonoRoutine
    {
        public Image _image;
        
        [SerializeField]
        private Sprite CommonSprite;
        [SerializeField]
        private Sprite HoveredSprite;
        
        public override async UniTask Initialize()
        {
            await base.Initialize();
            _image = GetComponent<Image>();
            SetupImage();
        }

        private void SetupImage()
        {
            _image.sprite = CommonSprite;
        }
        
        public void ChangeCommonImage()
        {
            _image.sprite = CommonSprite;
        }

        public void ChangeHoverImage()
        {
            _image.sprite = HoveredSprite;
        }

        public void ChangeImage(Sprite value)
        {
            _image.sprite = value;
        }
        
    }
}