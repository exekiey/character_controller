using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{

    public virtual void FixedUpdateState () { }
    public virtual void UpdateState () { }
    public virtual void LateUpdateState () { }
    public virtual void EnterState(params System.Object[] data) { }
    public virtual void ExitState() { }

}
