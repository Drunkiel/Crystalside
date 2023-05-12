using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public ItemData _itemData;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnPickedItem()
    {
        Instantiate(_itemData.itemOBJ, player.position, Quaternion.identity);
    }
}
