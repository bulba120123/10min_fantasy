using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjective
{
    string Description { get; }
    bool IsCompleted { get; }
    void UpdateProgress(int amount);
    void CheckCompletion();
}
