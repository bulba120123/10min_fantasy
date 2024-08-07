using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager {
    private Dictionary<IObjective, Action> unlocks = new Dictionary<IObjective, Action>();
    private HashSet<IObjective> unlockedObjectives = new HashSet<IObjective>(); // 해금된 목표 추적
    
    // 해금 조건 추가
    public void AddUnlockCondition(IObjective objective, Action unlockAction) {
        unlocks[objective] = unlockAction;
    }

    // 적 처치 목표 업데이트
    public void UpdateMonsterKillObjectives(int count) {
        foreach (var pair in unlocks) {
            if (!unlockedObjectives.Contains(pair.Key) && pair.Key is HuntMonstersObjective huntObjective) {
                huntObjective.UpdateProgress(count);
                CheckObjectivesCompletion();
            }
        }
    }

    // 금 획득 목표 업데이트
    public void UpdateCollectGoldObjectives(int amount) {
        foreach (var pair in unlocks) {
            if (!unlockedObjectives.Contains(pair.Key) && pair.Key is CollectGoldObjective goldObjective) {
                goldObjective.UpdateProgress(amount);
                CheckObjectivesCompletion();
            }
        }
    }

    // 레벨업 목표 업데이트
    public void UpdateLevelUpObjectives(int levelUps) {
        foreach (var pair in unlocks) {
            if (!unlockedObjectives.Contains(pair.Key) && pair.Key is LevelUpObjective levelUpObjective) {
                levelUpObjective.UpdateProgress(levelUps);
                CheckObjectivesCompletion();
            }
        }
    }

    // 보스 처치 목표 업데이트
    public void UpdateDefeatBossObjectives(int count) {
        foreach (var pair in unlocks) {
            if (!unlockedObjectives.Contains(pair.Key) && pair.Key is DefeatBossObjective bossObjective) {
                bossObjective.UpdateProgress(count);
                CheckObjectivesCompletion();
            }
        }
    }

    // 완료된 목표 처리
    private void CheckObjectivesCompletion() {
        foreach (var pair in unlocks) {
            if (pair.Key.IsCompleted && !unlockedObjectives.Contains(pair.Key)) {
                pair.Value.Invoke(); // 해금 조건 충족 시, 해당 액션 실행
                unlockedObjectives.Add(pair.Key); // 해금된 목표로 기록
            }
        }
    }
}