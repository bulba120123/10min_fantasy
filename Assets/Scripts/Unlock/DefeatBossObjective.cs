using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatBossObjective : IObjective {
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    private int bossesDefeated;

    public DefeatBossObjective() {
        Description = "Defeat at least one boss";
    }

    public void UpdateProgress(int count) {
        bossesDefeated += count;
        CheckCompletion();
    }

    public void CheckCompletion() {
        if (bossesDefeated >= 1) {
            IsCompleted = true;
        }
    }
}