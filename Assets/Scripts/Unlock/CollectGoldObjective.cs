using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGoldObjective : IObjective
{
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    private int currentGold;
    private int requiredGold;

    public CollectGoldObjective(int requiredGold) {
        this.requiredGold = requiredGold;
        Description = "Collect " + requiredGold + " gold";
    }

    public void UpdateProgress(int gold) {
        currentGold += gold;
        CheckCompletion();
    }

    public void CheckCompletion() {
        if (currentGold >= requiredGold) {
            IsCompleted = true;
        }
    }
}
