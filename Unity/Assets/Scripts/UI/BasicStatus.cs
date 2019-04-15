using SharingService.Anchoring;
using UnityEngine;
using UnityEngine.UI;

namespace SharingService.UI
{
    public class BasicStatus : MonoBehaviour
    {
        [SerializeField]
        private Text _message;
        [SerializeField]
        private Text _progress;
        [SerializeField]
        private GameObject _readyParent;

        public AnchorService AnchorService;

        void Start()
        {
            _readyParent.SetActive(false);
        }

        void Update()
        {
            _message.text = string.Format("Message: {0}", AnchorService.SessionFeedback ?? "None");
            _progress.text = string.Format("Progress: {0}%", AnchorService.ReadyProgress * 100.0f);
            _readyParent.SetActive(AnchorService.IsReady);
        }
    }
}
