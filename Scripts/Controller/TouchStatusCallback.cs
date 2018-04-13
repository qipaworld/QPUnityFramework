using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TouchStatusCallback
{
    void TouchBegin(Vector3 v);
    void TouchMove(Vector3 v);
    void TouchEnd(Vector3 v);
    Transform GetMoveTransform();
}