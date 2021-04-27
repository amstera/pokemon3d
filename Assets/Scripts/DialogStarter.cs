using UnityEngine;

public class DialogStarter : MonoBehaviour
{
    public GameObject Player;
    public string[] Dialogs;
    private bool _hasShownDialog;
    private int _dialogIndex = -1;

    void Update()
    {
        if (_hasShownDialog)
        {
            return;
        }

        if (Vector3.Distance(transform.position, Player.transform.position) < 3.5f)
        {
            _hasShownDialog = true;
            ShowLabDialog();
        }
    }

    private void ShowLabDialog()
    {
        if (_dialogIndex >= Dialogs.Length - 1)
        {
            return;
        }
        _dialogIndex++;
        DialogBox.Instance.ShowDialog(Dialogs[_dialogIndex], ShowLabDialog, true);
    }
}
