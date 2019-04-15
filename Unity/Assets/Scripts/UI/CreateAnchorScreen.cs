using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class CreateAnchorScreen : BaseScreen
{
    public GameObject Preview;

    public Text Hint;

    private bool _canBePlaced = false;
    private GestureRecognizer _gestureRecognizer;

    public override void Show()
    {
        base.Show();
        Preview.SetActive(true);
        _canBePlaced = false;
        SetHintText();

        if (_gestureRecognizer != null)
        {
            _gestureRecognizer = new GestureRecognizer();
            _gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
            _gestureRecognizer.Tapped += _gestureRecognizer_Tapped;
        }
        _gestureRecognizer.StartCapturingGestures();
    }

    private void _gestureRecognizer_Tapped(TappedEventArgs obj)
    {
        if (_canBePlaced)
        {
            IsDone = true;
        }
    }

    public override void Hide()
    {
        if (_gestureRecognizer != null)
        {
            _gestureRecognizer.StopCapturingGestures();
        }
        Preview.SetActive(false);
        base.Hide();
        
    }

    private void Update()
    {
        RaycastHit target;
        if (TryGazeHitTest(out target))
        {
            var rotation = Quaternion.FromToRotation(Vector3.up, target.normal);
            Preview.transform.position = target.point;
            Preview.transform.rotation = rotation;
            _canBePlaced = true;
            SetHintText();
        }
    }

    private void SetHintText()
    {
        Hint.text = _canBePlaced ? "Please Air tap to create the anchor" : "Please move around until a surface is detected";
    }

    private bool TryGazeHitTest(out RaycastHit target)
    {
        Camera mainCamera = Camera.main;

        return Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out target);
    }
}
