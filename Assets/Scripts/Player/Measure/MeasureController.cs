using TMPro;
using UnityEngine;

public class MeasureController : MonoBehaviour
{
    private int heightAboveSea;

    public Transform player;
    public TMP_Text heightText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        heightText.text = MeasureHeightAboveSea() + "m";
    }

    private int MeasureHeightAboveSea()
    {
        heightAboveSea = Mathf.RoundToInt(player.position.y - 2);

        return heightAboveSea;
    }
}
