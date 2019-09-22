using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTester : MonoBehaviour
{
    public SoundManager soundManager;

    private void Start()
    {
        StartCoroutine(AudioTest());
    }

    IEnumerator AudioTest()
    {
        soundManager.PlaySound("kill");
        yield return new WaitForSeconds(4f);
        soundManager.PlaySound("kill");
        yield return new WaitForSeconds(4f);
        soundManager.PlaySound("kill");
        yield return new WaitForSeconds(4f);
        soundManager.PlaySound("kill");
        yield return new WaitForSeconds(4f); 
        soundManager.PlaySound("kill");
        yield return new WaitForSeconds(4f);
        soundManager.PlaySound("kill");
        yield return new WaitForSeconds(4f);
        soundManager.PlaySound("kill");
        yield return new WaitForSeconds(4f); 
        soundManager.PlaySound("kill");
        yield return new WaitForSeconds(4f);


        /*
        Debug.Log("Starting Coroutine...");
        soundManager.PlaySound("armorPickup");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("xrayPickup");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("hpPickup");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("blasterRound");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("grenadeRound");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("machineRound");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("missileRound");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("shotgunRound");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("sniperRound");
        yield return new WaitForSeconds(4f);
        // this might be the choice shit, but these might be the same?
        soundManager.PlaySound("cineMod");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("glowingMod");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("hpRegenMod");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("invisMod");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("largeMod");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("shieldMod");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("speedMod");
        yield return new WaitForSeconds(4f);
        // yeah they're the same brb will fix :D
        soundManager.PlaySound("trackingMod");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("vampMod");
        yield return new WaitForSeconds(4f);
        // g a r l i c
        soundManager.PlaySound("kill");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("congratsPlayerOne");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("congratsPlayerTwo");
        yield return new WaitForSeconds(4f);
        // // fixed
        soundManager.PlaySound("congratsPlayerThree");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("congratsPlayerFour");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("roundStart");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("roundOver");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("exclamation");
        yield return new WaitForSeconds(4f);

        soundManager.PlaySound("enterTheRubicon");
        yield return new WaitForSeconds(4f);
    */
    }
}
