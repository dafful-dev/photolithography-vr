using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUtil : MonoBehaviour
{
    public void MoveUp(float amount)
    {
        transform.position += Vector3.up * amount;
    }

    public void MoveDown(float amount)
    {
        transform.position += Vector3.down * amount;
    }
}
