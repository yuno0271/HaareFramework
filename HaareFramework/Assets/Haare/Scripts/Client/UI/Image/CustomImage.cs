using Cysharp.Threading.Tasks;
using Haare.Client.Routine;
using UnityEngine;
using UnityEngine.UI;

namespace Haare.Client.UI
{
    public class CustomImage : MonoRoutine
    {
        public Image _image;
        
        [SerializeField]
        private Sprite CommonSprite;
        [SerializeField]
        private Sprite HoveredSprite;

        protected override void Constructor()
        {
            base.Constructor();
            _image = GetComponent<Image>();
        }
        
        public override async UniTask Initialize()
        {
            await base.Initialize();
            SetupImage();
        }

        private void SetupImage()
        {
            _image.sprite = CommonSprite;
        }

        public void ChangeRGB(int r, int g, int b)
        {
            _image.color = new Color(r, g, b);
        }
        
        public async UniTask Fade(float startAlpha,float targetAlpha, float duration)
        {
            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                float progress = time / duration;
                ChangeAlpha(Mathf.Lerp(startAlpha, targetAlpha, progress));
                await UniTask.Yield();
            }
            ChangeAlpha(targetAlpha);
        }
        public void ChangeAlpha(float alpha)
        {
            _image.color = new Color(255, 255, 255, alpha);
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