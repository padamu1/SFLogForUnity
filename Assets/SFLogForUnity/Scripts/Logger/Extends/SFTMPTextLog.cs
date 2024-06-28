using SFLogForUnity.Scripts.Logger.Log;

namespace SFLogForUnity.Scripts.Logger.Extends
{
    public class SFTMPTextLog : SFLog
    {
        // Import TMP_Text

        protected override void Print(string text)
        {
            //textComponent.text = text;
        }
        public override void SetRent()
        {
            this.gameObject.SetActive(true);
        }

        public override void SetReturn()
        {
            this.gameObject.SetActive(false);
        }
    }
}
