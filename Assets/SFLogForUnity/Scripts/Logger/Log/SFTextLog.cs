using UnityEngine;
using UnityEngine.UI;

namespace SFLogForUnity.Scripts.Logger.Log
{
    public class SFTextLog : SFLog
    {
        [SerializeField] private Text textComponent;

        protected override void Print(string text)
        {
            textComponent.text = text;
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
