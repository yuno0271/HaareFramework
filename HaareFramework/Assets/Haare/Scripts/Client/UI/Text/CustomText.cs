using Haare.Client.Routine;
using Haare.Client.UI.BaseImage;
using TMPro;

namespace Haare.Client.UI.BaseText
{
    public class CustomText : MonoRoutine
    {
        public TMP_Text _Text;
        
        public void ChangeText(string value)
        {
            _Text.text = value;
        }
    }
}