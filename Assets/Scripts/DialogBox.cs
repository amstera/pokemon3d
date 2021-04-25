using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public static DialogBox Instance;
    public Player Player;
    public Text Text;
    public RawImage Box;
    public RawImage Arrow;
    public int MaxCharLengthPerBox = 80;

    private char[] _currentDialog;
    private int _dialogIndex;
    private bool _isPrinting;
    private bool _canFinish;
    private bool _isFinished = true;
    private float _showDialogTimeout;
    private Action _callback;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (_isPrinting)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canFinish)
            {
                if (Box.enabled)
                {
                    HideDialog();
                }
            }
            else if (!_isFinished)
            {
                StartCoroutine(PrintText());
            }
        }
    }

    public void ShowDialog(string text, Action callback = null, bool forceMessage = false)
    {
        if (!_isFinished || _isPrinting || (!forceMessage && (Time.time - _showDialogTimeout) < 0.5f))
        {
            return;
        }
        Player.IsFrozen = true;
        _isFinished = false;
        Arrow.enabled = false;
        Box.enabled = true;
        _dialogIndex = 0;
        _callback = callback;
        _currentDialog = text.ToCharArray();
        StartCoroutine(PrintText());
    }

    private void HideDialog()
    {
        Player.IsFrozen = false;
        Box.enabled = false;
        Arrow.enabled = false;
        Text.text = string.Empty;
        _canFinish = false;
        _showDialogTimeout = Time.time;
        _isFinished = true;
        _callback?.Invoke();
    }

    private IEnumerator PrintText()
    {
        Text.text = string.Empty;
        if (_currentDialog.Length < (_dialogIndex + 1) * MaxCharLengthPerBox)
        {
            Arrow.enabled = false;
            _isPrinting = true;
            for (int i = _dialogIndex * MaxCharLengthPerBox; i < _currentDialog.Length; i++)
            {
                Text.text += _currentDialog[i];
                yield return new WaitForSeconds(0.01f);
            }
            _isPrinting = false;
            _canFinish = true;
        }
        else
        {
            Arrow.enabled = true;
            _isPrinting = true;
            for (int i = _dialogIndex * MaxCharLengthPerBox; i < (_dialogIndex + 1) * MaxCharLengthPerBox; i++)
            {
                Text.text += _currentDialog[i];
                yield return new WaitForSeconds(0.01f);
            }
            _isPrinting = false;
            _dialogIndex++;
        }
    }
}
