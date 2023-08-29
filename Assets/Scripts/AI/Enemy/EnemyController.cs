using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isPatrolling;
    public bool isPlayerInRadius;

    public EnemyInfo _info;
    [SerializeField] private EnemyMovement _movement;

    private void Start()
    {
        _info.npcCanvas.UpdateTitle(_info.name);
    }

    // Update is called once per frame
    void Update()
    {
        _movement.RotateTo(_info.npcCanvas.transform, GameObject.Find("Player").transform.position);
    }
}
