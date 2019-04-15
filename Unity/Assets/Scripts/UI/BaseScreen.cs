using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    public bool IsDone { get; protected set; }
    public virtual void Show()
    {
        gameObject.SetActive(true);
        IsDone = false;
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
