using System;
using System.Collections;
using System.Collections.Generic;
using Haare.Client.Routine;
using Haare.Client.UI.BaseImage;
using UnityEngine;
using UnityEngine.UI;

namespace Haare.Client.UI.BaseImage
{
    public class BaseImage : MonoRoutine, IBaseImage
    {
        public Image MyImage;
        
        [SerializeField]
        private Sprite CommonSprite;
        [SerializeField]
        private Sprite HoveredSprite;

        public void ChangeCommonImage()
        {
            MyImage.sprite = CommonSprite;
        }

        public void ChangeHoverImage()
        {
            MyImage.sprite = HoveredSprite;
        }

        public void ChangeImage(Sprite value)
        {
            MyImage.sprite = value;
        }
        
    }
}