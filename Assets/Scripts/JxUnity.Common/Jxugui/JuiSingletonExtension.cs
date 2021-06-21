using UnityEngine;

public abstract class JuiSingletonExtension<UIType> : JuiSingleton<UIType>
{
    /// <summary>
    /// 导航返回，Esc
    /// </summary>
    /// <param name="parent">所在父物体</param>
    public void CancelIfInParent(GameObject parent)
    {
        if (EventSystemManager.Instance.GetCurrent()?.transform.parent == parent.transform)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Hide();
            }
        }
    }

}
