using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;

    public GameObject[] cursors;

    void Awake()
    {
        instance = this;
    }

    public void ChangeCursor(int cursorID)
    {
        if (cursors[cursorID].activeSelf) return;

        for (int i = 0; i < cursors.Length; i++)
        {
            cursors[i].SetActive(false);
        }

        cursors[cursorID].SetActive(true);
    }
}
