using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class ObjectInteractive : MonoBehaviour
    {
        public bool HasInteracted=false;

        private void OnDestroy()
        {

            if(InteractTip.TextMeshProUGUI!=null&&!InteractTip.TextMeshProUGUI.IsDestroyed())
                InteractTip.TextMeshProUGUI.enabled=false;
        }
        protected void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("MainCamera") ){
                InteractTip.TextMeshProUGUI.enabled = true;
            }
        }
        protected void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("MainCamera"))
            {
                InteractTip.TextMeshProUGUI.enabled = false;
            }
        }
        protected void SetTipMessage(string message)
        {
            if (message != null)
            {
                InteractTip.TextMeshProUGUI.text = message;
            }
        }
        abstract public bool Interact();

    }
}