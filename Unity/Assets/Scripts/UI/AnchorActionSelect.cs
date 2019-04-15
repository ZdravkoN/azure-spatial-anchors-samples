using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.Speech;

public enum AnchorActionSelectResult
{
    None,
    CreateAnchor,
    LoadAnchor
}

public class AnchorActionSelect : BaseScreen
{
    public AnchorActionSelectResult Result { get; protected set; } = AnchorActionSelectResult.None;

    private KeywordRecognizer _keywordRecognizer;
    private readonly Dictionary<string, System.Action> _keywords = new Dictionary<string, System.Action>();

    private void EnsureKeywords()
    {
        if (_keywords.Keys.Count == 0)
        {
            _keywords.Add("Create Anchor", CreateAnchor);
            _keywords.Add("Load Anchor", LoadAnchor);
        }
    }

    // Start is called before the first frame update
    public override void Show()
    {
        base.Show();
        Result = AnchorActionSelectResult.None;
        if (_keywordRecognizer == null)
        {
            EnsureKeywords();
            _keywordRecognizer = new KeywordRecognizer(_keywords.Keys.ToArray());
            _keywordRecognizer.OnPhraseRecognized += _keywordRecognizer_OnPhraseRecognized;
        }
        _keywordRecognizer.Start();
    }

    private void _keywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (_keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    public override void Hide()
    {
        if (_keywordRecognizer != null)
        {
            _keywordRecognizer.Stop();
        }
        base.Hide();
    }

    private void CreateAnchor()
    {
        Result = AnchorActionSelectResult.CreateAnchor;
        IsDone = true;
    }

    private void LoadAnchor()
    {
        Result = AnchorActionSelectResult.LoadAnchor;
        IsDone = true;
    }

}
