using SharingService.Anchoring;
using UnityEngine;
using UnityEngine.UI;

public class ScanningScreen : BaseScreen
{
    [SerializeField]
    private Text _message;
    [SerializeField]
    private Text _progress;

    public AnchorService AnchorService;

    // Update is called once per frame
    void Update()
    {
        _message.text = string.Format("{0}", AnchorService.SessionFeedback ?? "");
        _progress.text = string.Format("Progress: {0}%", AnchorService.ReadyProgress * 100.0f);
        IsDone = AnchorService.IsReady;
    }
}
