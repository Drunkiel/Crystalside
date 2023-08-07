using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public static DialogController instance;

    public DialogData _dialogData;
    private int dialogIndex;
    public NpcController _npcController;

    public Button[] optionButtons;
    public TMP_Text responseText;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void StartDialog(DialogData dialogData)
    {
        _dialogData = dialogData;
        gameObject.GetComponent<UIController>().OpenCloseUI();
        _npcController.isNPCTalking = true;
        UpdateDialog();
    }

    public void ExitDialog()
    {
        _dialogData = null;
        _npcController.isNPCTalking = false;
        gameObject.GetComponent<UIController>().OpenCloseUI();
    }

    private void UpdateDialog()
    {
        if (dialogIndex >= _dialogData._dialogOptions.Count) dialogIndex = 0;

        //Set texts to buttons
        for (int i = 0; i < optionButtons.Length; i++)
        {
            try
            {
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = _dialogData._dialogOptions[dialogIndex].dialogTexts[i];
            }
            catch (System.Exception)
            {
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = "...";
                optionButtons[i].interactable = false;
            }
        }

        //Set events to button
        if (_dialogData._dialogActions.Count > 0)
        {
            for (int i = 0; i < _dialogData._dialogActions.Count; i++)
            {
                if (_dialogData._dialogActions[dialogIndex].isEvent[i]) optionButtons[i].onClick.AddListener(
                    () => _dialogData._dialogActions[dialogIndex].dialogEvents[i].Invoke());
            }
        }

        dialogIndex++;
    }

    public void UpdateResponse(int i)
    {
        responseText.text = _dialogData._dialogOptions[dialogIndex - 1].responseTexts[i];
        UpdateDialog();
    }
}
