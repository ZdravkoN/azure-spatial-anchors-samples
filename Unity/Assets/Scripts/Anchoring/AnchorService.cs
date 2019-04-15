using Microsoft.Azure.SpatialAnchors;
using UnityEngine;

namespace SharingService.Anchoring
{
    public class AnchorService: MonoBehaviour
    {
        [SerializeField]
        private string _accountId;
        [SerializeField]
        private string _accountKey;

        private CloudSpatialAnchorSession _cloudAnchorSession;

        public bool IsReady { get; private set; }

        public float ReadyProgress { get; private set; }

        public string SessionFeedback { get; private set; }

        private void Awake()
        {
            _cloudAnchorSession = new CloudSpatialAnchorSession();

            _cloudAnchorSession.Configuration.AccountId = _accountId.Trim();
            _cloudAnchorSession.Configuration.AccountKey = _accountKey.Trim();
        }

        private void Start()
        {
            _cloudAnchorSession.Start();
            _cloudAnchorSession.TokenRequired += _cloudAnchorSession_TokenRequired;
            _cloudAnchorSession.SessionUpdated += _cloudAnchorSession_SessionUpdated;
            _cloudAnchorSession.Error += _cloudAnchorSession_Error;
        }

        private void _cloudAnchorSession_Error(object sender, SessionErrorEventArgs args)
        {
            SessionFeedback = args.ErrorMessage;
        }

        private void _cloudAnchorSession_SessionUpdated(object sender, SessionUpdatedEventArgs args)
        {
            var status = args.Status;

            var userFeedback = GetSessionFeedback(status.UserFeedback);
            SessionFeedback = userFeedback ?? SessionFeedback;
            IsReady = status.RecommendedForCreateProgress >= 1.0;
            ReadyProgress = Mathf.Min(status.RecommendedForCreateProgress, 1.0f);
        }

        private void _cloudAnchorSession_TokenRequired(object sender, TokenRequiredEventArgs args)
        {
            SessionFeedback = "Token required for cloud anchors";
        }

        private string GetSessionFeedback(SessionUserFeedback userFeedback)
        {
            string result;
            switch (userFeedback)
            {
                case SessionUserFeedback.None:
                    result = null;
                    break;
                case SessionUserFeedback.MotionTooQuick:
                    result = "You are moving too quickly. Slow down.";
                    break;
                case SessionUserFeedback.NotEnoughFeatures:
                    result = "There aren't enough features. Move around to another area.";
                    break;
                case SessionUserFeedback.NotEnoughMotion:
                    result = "Please move around.";
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }
    }
}
