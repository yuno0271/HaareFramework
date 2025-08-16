using TMPro;

using Haare.Client.Routine;

namespace Haare.Client.UI
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