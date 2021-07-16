using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections;

/// <summary>
/// 创建检测距离的条件节点
/// </summary>
public class CheckDistance : Conditional
{
    public SharedFloat Distance;
    public SharedTransform Target;

    public override void OnStart()
    {
        Target.Value = PlayerController.Instance.transform;
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector2.Distance(transform.position, Target.Value.position) <= Distance.Value)
        {
            return TaskStatus.Success;
        }


        return TaskStatus.Failure;
    }
}

/// <summary>
/// 创建检测视野范围的条件节点
/// </summary>
public class CheckView : Conditional
{

    public SharedTransform Target;
    public SharedFloat Angle;
    public SharedInt faceDir;
    public SharedFloat UnTime;
    public SharedBool AtkStatus;

    public override void OnStart()
    {
        Target.Value = PlayerController.Instance.transform;
    }
    public override TaskStatus OnUpdate()
    {
        float tempAngle = Vector2.Angle(transform.position - Target.Value.position,
            new Vector2(transform.localScale.x * faceDir.Value, 0));
        if (tempAngle < (Angle.Value))//目标和自身正前方的夹角小于视野范围的一般就认为在视野范围内
        {
            return TaskStatus.Success;
        }
        else if (AtkStatus.Value)
        {
            if (UnTime.Value > 0 && AtkStatus.Value)
            {
                UnTime.Value -= Time.deltaTime;
            }
            if (UnTime.Value <= 0)
            {
                AtkStatus.Value = false;
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}

/// <summary>
/// 检测当前状态是否应该行动
/// </summary>
public class CheckStatus : Conditional
{
    public Status CurrentStatus;
    public override TaskStatus OnUpdate()
    {
        if (transform.GetComponent<EnemyController>().status.Equals(CurrentStatus))
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}

/// <summary>
/// 创建检测目标的条件节点
/// </summary>
public class CheckOnLine : Conditional
{
    public SharedString PlayerTag;
    public SharedTransform Target;
    public SharedBool RayHitPlayer;//是否检测到目标

    public override void OnStart()
    {
        Target.Value = PlayerController.Instance.transform;
    }
    public override TaskStatus OnUpdate()
    {
        if (RayHitPlayer.Value)//如果检测到目标就看向目标
        {
            transform.localScale = new Vector2(transform.position.x > Target.Value.position.x ? 1 : -1, 1);
        }

        Ray2D ray = new Ray2D(transform.position, new Vector2(-transform.localScale.x, 0));
        RaycastHit2D hitLeft = Physics2D.Raycast(ray.origin, ray.direction, 5);
        RaycastHit2D hitRight = Physics2D.Raycast(ray.origin, -ray.direction, 5);
        if (hitLeft.collider != null)
        {
            if (hitLeft.collider.tag == (string)PlayerTag.GetValue())//射线检测到目标
            {
                RayHitPlayer.SetValue(true);
                return TaskStatus.Success;
            }
        }
        else if (hitRight.collider != null)
        {
            if (hitRight.collider.tag == (string)PlayerTag.GetValue())//射线检测到目标
            {
                RayHitPlayer.SetValue(true);
                return TaskStatus.Success;
            }
        }
        else
        {
            RayHitPlayer.SetValue(false);
            return TaskStatus.Running;
        }
        return TaskStatus.Running;
    }

}

/// <summary>
/// 创建目标移动的事件节点
/// </summary>
public class MoveToTarget : Action
{
    public SharedTransform Target;
    public SharedFloat Distance;
    public SharedFloat TargetDistance;
    public SharedInt faceDir;
    public SharedInt jumpForce;
    public SharedVector2 bottomOffset;
    public SharedFloat collisionRadius;
    public LayerMask groundLayer;
    public bool isPlayer;

    private Rigidbody2D rd;
    private bool onGround;
    private bool isJump;
    public override void OnStart()
    {
        base.OnStart();
        rd = transform.GetComponent<Rigidbody2D>();
        if (!isPlayer)
        {
            Target.Value = PlayerController.Instance.transform;
        }
    }

    public override TaskStatus OnUpdate()
    {
        transform.localScale = new Vector2(transform.position.x > Target.Value.position.x + 0.1f ?
            faceDir.Value * 1 : -1 * faceDir.Value, 1);
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset.Value,
                    collisionRadius.Value, groundLayer);

        if (Vector2.Distance(transform.position, Target.Value.position) <= TargetDistance.Value)//表示已经移动到目标身边
        {
            return TaskStatus.Success;
        }
        else if (Vector2.Distance(transform.position, Target.Value.position) <= Distance.Value)//判断是目标的距离
        {
            if (onGround)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(Target.Value.position.x, transform.position.y), 2f * Time.deltaTime);
                rd.velocity = new Vector2();
            }

            Ray2D ray = new Ray2D(transform.position, new Vector2(-transform.localScale.x * faceDir.Value, 0));
            RaycastHit2D hitLeft = Physics2D.Raycast(ray.origin, ray.direction, 0.5f);
            if (hitLeft.collider != null)
            {
                if (hitLeft.collider.CompareTag("Ground") && onGround && !isJump)//射线检测到目标
                {
                    StartCoroutine(Jump());
                }
            }
        }
        else//超过距离认为目标逃离，那么返回失败
        {
            return TaskStatus.Failure;
        }

        return TaskStatus.Running;
    }

    IEnumerator Jump()
    {
        if (!isJump)
        {
            Debug.Log("jump");
            rd.AddForce(new Vector2(-transform.localScale.x, 10) * jumpForce.Value);
            isJump = true;
            yield return new WaitForSeconds(0.2f);
            isJump = false;
        }
    }

}

/// <summary>
/// 看向目标
/// </summary>
public class LookAtTarget : Action
{
    public SharedTransform Target;
    public override void OnStart()
    {
        Target.Value = PlayerController.Instance.transform;
    }
    public override TaskStatus OnUpdate()
    {
        transform.localScale = new Vector2(transform.position.x > Target.Value.position.x ? 1 : -1, 1);
        return TaskStatus.Success;
    }
}

/// <summary>
/// 创建检测攻击距离的条件节点
/// </summary>
public class SetAtkStatus : Action
{
    public SharedBool AtkStatus;
    public override TaskStatus OnUpdate()
    {
        AtkStatus.Value = true;
        return TaskStatus.Success;
    }
}

/// <summary>
/// 两点巡逻
/// </summary>
public class Patrol : Action
{
    public SharedTransform TargetA;
    public SharedTransform TargetB;
    public SharedFloat WaitTime;
    public SharedInt faceDir;

    private bool move;
    private bool back;

    public override TaskStatus OnUpdate()
    {
        if (WaitTime.Value > 0)
        {
            WaitTime.Value -= Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, TargetA.Value.position) <= 0.2f && !move)
        {
            move = true;
            back = false;
            WaitTime.Value = 3f;
        }

        if (Vector2.Distance(transform.position, TargetB.Value.position) <= 0.2f && !back)
        {
            move = false;
            back = true;
            WaitTime.Value = 3f;
        }
        if (move && WaitTime.Value <= 0)
        {
            MoveToTarget(TargetB, -faceDir.Value);
        }

        if (back && WaitTime.Value <= 0)
        {
            MoveToTarget(TargetA, faceDir.Value);
        }
        return TaskStatus.Running;
    }

    private void MoveToTarget(SharedTransform target, int face)
    {
        transform.localScale = new Vector2(face, 1);
        transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(target.Value.position.x, transform.position.y), 1.5f * Time.deltaTime);
    }

}
