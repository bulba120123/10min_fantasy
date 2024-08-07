using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpObjective : IObjective {
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    private int currentLevelUps;
    private int requiredLevelUps;

    public LevelUpObjective(int requiredLevelUps) {
        this.requiredLevelUps = requiredLevelUps;
        Description = "Level up " + requiredLevelUps + " times";
    }

    public void UpdateProgress(int levelUps) {
        currentLevelUps += levelUps;
        CheckCompletion();
    }

    public void CheckCompletion() {
        if (currentLevelUps >= requiredLevelUps) {
            IsCompleted = true;
        }
    }
}