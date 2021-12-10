# ENV Tween

README WIP



You might want to use the EaseFunctions package as well: https://github.com/Zeejfps/EaseFunctions-Unity.git



Simple Usage:

```c#
m_Transform.TweenLocalAnglesYTo(180f, 0.5f, EaseFunctions.CubicOut).Play();
```



Full Example Script:

```c#
using EnvDev;
using UnityEngine;

public class EnvTweenExample : MonoBehaviour
{
    [SerializeField] Transform m_Transform;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PlayTween();
    }

    void PlayTween()
    {
        var tweenHandle = m_Transform.TweenLocalAnglesYTo(180f, 0.5f, EaseFunctions.CubicOut).Play();
    }
}
```

