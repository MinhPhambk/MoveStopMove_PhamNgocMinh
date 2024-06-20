using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : Character
{
    void Start()
    {
        ChangeMaterial(PantType.Pokemon);

        SimplePool.Spawn<Bot>(PoolType.Bot, new Vector3(Random.Range(-10, 10), 0f, Random.Range(-10, 10)), Quaternion.identity);
    }

    private void Update()
    {
        MovePlayer();

        if (enemies.Count > 0)
        {
            double minDis = double.MaxValue;

            foreach (var e in enemies)
            {
                minDis = Math.Min(minDis, Vector3.Distance(this.TF.transform.position, e.TF.position));
            }
            foreach (var e in enemies)
            {
                if (minDis - Vector3.Distance(this.TF.transform.position, e.transform.position) < 0.1f)
                {
                    e.SetActiveChoosed(true);
                    return;
                }
            }
        }
    }

    private void MovePlayer()
    {
        if (!stopped)
        {
            if (Input.GetMouseButton(0))
            {
                if (JoystickController.GetDirection() != Vector3.zero)
                {
                    TF.forward = JoystickController.GetDirection();
                    Vector3 newPosition = speed * Time.deltaTime * JoystickController.GetDirection() + TF.position;
                    TF.position = NextMove(newPosition);
                }

                ChangeAnim(Constant.ANIM_RUN);
            }

            if (Input.GetMouseButtonUp(0))
            {
                ChangeAnim(Constant.ANIM_IDLE);
            }
        }
        else
        {
            ChangeAnim(Constant.ANIM_IDLE);
        }
    }
}
