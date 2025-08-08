using Haare.Client.Routine;
using Haare.Client.UI.BaseImage;
using TMPro;

namespace Haare.Client.UI.BaseText
{
    public class BaseText : MonoRoutine , IBaseText
    {
        public TMP_Text MyText;
        
        public void ChangeText(string value)
        {
            MyText.text = value;
        }
    }
}