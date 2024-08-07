using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using AltTester.AltTesterUnitySDK.Driver;

public class MyFirstTest
{
    private AltDriver altDriver;

    [OneTimeSetUp]
    public void SetUp()
    {
        altDriver = new AltDriver();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        altDriver.Stop();
    }

    [Test]
    public void TestStartGame()
    {
        var uiElement = altDriver.FindObject(By.NAME, "Character UI").Tap();

        // var panelElement = altDriver.WaitForObject(By.NAME, "Panel");
        Assert.IsTrue(uiElement.enabled);
    }
}
