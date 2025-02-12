using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: fenn
//Quest for defeating enemies, parameters layed out but not implemented fully in game
public class DefeatEnemiesQuestStep : QuestStep
{
    
    private int enemiesDefeated = 0;
    //Num of enemies we want to defeat in the tutorial
    private int enemyGoal = 10;

    private void OnEnable()
    {
        //When we have a listener for enemy defeats put it here
    }

    private void OnDisable()
    {
        //When we have a listener for enemy defeats put it here
    }

    private void EnemiesDefeated()
    {
        if (enemiesDefeated < enemyGoal)
        {
            enemiesDefeated++;
            UpdateState();
        }

        if (enemiesDefeated >= enemyGoal)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = enemiesDefeated.ToString();
        string status = "Defeated " + enemiesDefeated + " / " + enemyGoal + " enemies.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.enemiesDefeated = System.Int32.Parse(state);
        UpdateState();
    }
}
