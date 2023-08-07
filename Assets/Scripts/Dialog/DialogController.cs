using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public static DialogController instance;

    public DialogData _dialogData;
    private int dialogIndex;

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
        UpdateDialog();
    }

    private void UpdateDialog()
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            try
            {
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = _dialogData._dialogOptions[dialogIndex].dialogTexts[i];
                optionButtons[i].onClick.AddListener(() => UpdateResponse());
            }
            catch (System.Exception)
            {
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = "...";
                optionButtons[i].interactable = false;
            }
        }

        dialogIndex++;
    }

    public void UpdateResponse()
    {

    }
}
