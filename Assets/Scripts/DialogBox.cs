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
    public GameObject ChoiceBox;
    public GameObject ChoiceArrow;
    public int MaxCharLengthPerBox = 80;
    public AudioSource Press;

    private char[] _currentDialog;
    private int _dialogIndex;
    private bool _isPrinting;
    private bool _canFinish;
    private bool _isChoice;
    private bool _isChoosing;
    private bool _isFinished = true;
    private float _showDialogTimeout;
    private int _choice = 1;
    private Action _callback;
    private Action<bool> _choiceCallback;

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
        if (_isChoosing)
        {
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && _choice == 1)
            {
                ChoiceArrow.transform.position += Vector3.down * 50;
                _choice = -1;
            }
            else if ((Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.W)) && _choice == -1)
            {
                ChoiceArrow.transform.position += Vector3.up * 50;
                _choice = 1;
            }
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
            else if (_isChoosing)
            {
                _isChoosing = false;
                bool isChosen = _choice == 1;
                if (!isChosen)
                {
                    ChoiceArrow.transform.position += Vector3.up * 50;
                    _choice = 1;
                }
                HideDialog();
                _choiceCallback?.Invoke(isChosen);
            }
            else if (!_isFinished)
            {
                Press.Play();
                StartCoroutine(PrintText());
            }
        }
    }

    public void ShowChoiceDialog(string text, Action<bool> callback, bool forceMessage = false)
    {
        _isChoice = true;
        _choiceCallback = callback;
        ShowDialog(text, null, forceMessage);
    }

    public void ShowDialog(string text, Action callback = null, bool forceMessage = false)
    {
        if (!_isFinished || _isChoosing || _isPrinting || (!forceMessage && (Time.time - _showDialogTimeout) < 0.5f))
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
        Press.Play();
        ChoiceBox.gameObject.SetActive(false);
        Player.IsFrozen = false;
        Box.enabled = false;
        Arrow.enabled = false;
        Text.text = string.Empty;
        _isChoice = false;
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
            if (_isChoice)
            {
                ChoiceBox.SetActive(true);
                _isChoosing = true;
            }
            else
            {
                _canFinish = true;
            }
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
