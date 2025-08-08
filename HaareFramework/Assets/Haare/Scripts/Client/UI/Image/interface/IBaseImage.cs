using UnityEngine;

namespace Haare.Client.UI.BaseImage
{
    public interface IBaseImage
    {
        public void ChangeCommonImage();
        public void ChangeHoverImage();
        public void ChangeImage(Sprite value);
    }
}