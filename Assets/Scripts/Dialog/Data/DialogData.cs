using UnityEngine;
using UnityEngine.Events;

public class DialogOption
{
    [SerializeField] private string dialogText1;
    [SerializeField] private string dialogText2;
    [SerializeField] private string dialogText3;
    [SerializeField] private string dialogText4;

    [SerializeField] private string responseText1;
    [SerializeField] private string responseText2;
    [SerializeField] private string responseText3;
    [SerializeField] private string responseText4;
}

public class DialogAction
{
    [SerializeField] private bool dialogAction1;
    [SerializeField] private bool dialogAction2;
    [SerializeField] private bool dialogAction3;
    [SerializeField] private bool dialogAction4;

    public UnityEvent dialogEvent1;
    public UnityEvent dialogEvent2;
    public UnityEvent dialogEvent3;
    public UnityEvent dialogEvent4;
}

public class DialogData : ScriptableObject
{
    public DialogOption[] _dialogOptions;
    public DialogAction[] _dialogActions;
}
