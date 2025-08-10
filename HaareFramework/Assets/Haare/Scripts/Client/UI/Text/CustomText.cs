using Haare.Client.Routine;
using Haare.Client.UI.HaareImage;
using TMPro;

namespace Haare.Client.UI.HaareText
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