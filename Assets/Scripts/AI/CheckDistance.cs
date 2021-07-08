using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

/// <summary>
/// 创建检测距离的条件节点
/// </summary>
public class CheckDistance : Conditional
{
    public SharedFloat Distance;
    public SharedTransform Target;

    public override TaskStatus OnUpdate()
    {
        if (Vector2.Distance(transform.position, Target.Value.position) <= Distance.Value)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
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

    public override TaskStatus OnUpdate()
    {
        float tempAngle = Vector2.Angle(transform.position - Target.Value.position,
            new Vector2(transform.localScale.x * faceDir.Value, 0));
        if (tempAngle < (Angle.Value))//目标和自身正前方的夹角小于视野范围的一般就认为在视野范围内
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}

/// <summary>
/// 检测当前状态是否应该行动
/// </summary>
public class CheckStatus : Conditional
{

    public override TaskStatus OnUpdate()
    {
        if (transform.GetComponent<EnemyController>().status == Status.combat)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
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
                //return TaskStatus.Success;
                return TaskStatus.Running;
            }
        }
        else if (hitRight.collider != null)
        {
            if (hitRight.collider.tag == (string)PlayerTag.GetValue())//射线检测到目标
            {
                RayHitPlayer.SetValue(true);
                //return TaskStatus.Success;
                return TaskStatus.Running;
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
    public SharedInt faceDir;

    public override TaskStatus OnUpdate()
    {
            transform.localScale = new Vector2(transform.position.x > Target.Value.position.x ?
                faceDir.Value*1 : -1* faceDir.Value, 1);

        if (Vector3.Distance(transform.position, Target.Value.position) <= 1f)//表示已经移动到目标身边
        {
            return TaskStatus.Running;
        }
        else if (Vector3.Distance(transform.position, Target.Value.position) <= Distance.Value)//判断是目标的距离
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.Value.position, 1.5f * Time.deltaTime);
        }
        else//超过距离认为目标逃离，那么返回失败
        {
            return TaskStatus.Failure;
        }

        return TaskStatus.Running;
    }
}

/// <summary>
/// 看向目标
/// </summary>
public class LookAtTarget : Action
{
    public SharedTransform Target;

    public override TaskStatus OnUpdate()
    {
        if (transform.position.x > Target.Value.position.x)//表示已经移动到目标身边
        {
            transform.localScale = new Vector2(1, 1);
            return TaskStatus.Success;
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
            return TaskStatus.Success;
        }
    }
}

/// <summary>
/// 创建检测攻击距离的条件节点
/// </summary>
public class CheckAttackDistance : Conditional
{
    public SharedTransform Target;
    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(transform.position, Target.Value.position) <= 1)
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
/// 创建攻击目标的事件节点
/// </summary>
public class AttackTarget : Action
{
    public SharedTransform Target;

    public override TaskStatus OnUpdate()
    {
        if (Target.Value.localScale.x <= 0.1f)
        {
            return TaskStatus.Success;
        }
        else
        {
            Target.Value.localScale -= Vector3.one * 0.2f * Time.deltaTime;
        }
        return TaskStatus.Running;
    }
}
