using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class QuestTesting
{
    enum States { REQUIREMENTS_NOT_MET,
    CAN_START,
    IN_PROGRESS,
    CAN_FINISH,
    FINISHED};
    // A Test behaves as an ordinary method
    [Test]
    public void SimpleQuestCreation()
    {
        // enum Direction {};
        Assert.AreEqual(States.CAN_START, QuestState.CAN_START);
        // var Quest = new Quests("", "",)
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestBasicQuestCompletion()
    {
        // Assert.AreEqual(new bool(false), QuestStep.isFinished);
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
