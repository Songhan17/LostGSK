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
        if (Vector3.Distance(transform.position, Target.Value.position) <= Distance.Value)
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

    public override TaskStatus OnUpdate()
    {
        float tempAngle = Vector3.Angle(transform.forward, Target.Value.position - transform.position);
        if (tempAngle < (Angle.Value / 2f))//目标和自身正前方的夹角小于视野范围的一般就认为在视野范围内
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}

/// <summary>
/// 创建检测目标的条件节点
/// </summary>
public class CheckTag : Conditional
{

    public SharedString PlayerTag;
    public SharedTransform Target;
    public SharedBool RayHitPlayer;//是否检测到目标

    public override void OnStart()
    {
        transform.LookAt(Target.Value.position);//第一进入视野范围就看向目标
    }

    public override TaskStatus OnUpdate()
    {
        if (RayHitPlayer.Value)//如果检测到目标就看向目标
        {
            transform.LookAt(Target.Value.position);
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20))
        {
            if (hit.collider.tag == (string)PlayerTag.GetValue())//射线检测到目标
            {
                RayHitPlayer.SetValue(true);
                return TaskStatus.Success;
            }
            else//检测到的不是目标
            {
                RayHitPlayer.SetValue(false);
                return TaskStatus.Running;
            }

        }
        else//射线什么也没检测到那么也默认为检测到目标，因为在这个节点里面，此时目标肯定是在视野范围内的并且没有被障碍物遮挡视野
        {
            RayHitPlayer.SetValue(true);
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

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(transform.position, Target.Value.position) <= 1f)//表示已经移动到目标身边
        {
            return TaskStatus.Success;

        }
        else if (Vector3.Distance(transform.position, Target.Value.position) <= 10)//判断是目标的距离
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
