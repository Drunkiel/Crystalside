using UnityEngine;

public class Mouse : EnemyMovement
{
    public float distance;

    public override void Update()
    {
        base.Update();

        if (_enemyController.isPlayerInRadius) distanceToAttack = distance;
    }

    public override void Attack()
    {
        base.Attack();

        print("test");
    }

    public override void GetDistanceToPlayer()
    {
        base.GetDistanceToPlayer();

        if (onTheGround)
        {
            MoveNPC(distanceToDestination);
            JumpNPC(2);
        }
    }
}
