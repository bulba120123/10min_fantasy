using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntMonstersObjective : IObjective {
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    private int currentCount;
    private int requiredCount;

    public HuntMonstersObjective(int requiredCount) {
        this.requiredCount = requiredCount;
        Description = "Hunt " + requiredCount + " monsters";
    }

    public void UpdateProgress(int count) {
        currentCount += count;
        CheckCompletion();
    }

    public void CheckCompletion() {
        if (currentCount >= requiredCount) {
            IsCompleted = true;
        }
    }
}