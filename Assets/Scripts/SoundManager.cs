using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // this script does the sound
    // there's a lot okay

    public AudioSource anouncerMain;
    public AudioSource musicMain;

    #region Music Tracks
    public AudioClip BattleTheme;
    #endregion

    #region all the sounds (if you're gonna open this take a deep breath)

    #region pickups
    public AudioClip armor1;
    public AudioClip armor2;
    public AudioClip armorPickup1;
    public AudioClip armorPickup2;
    public AudioClip armorPickup3;
    public AudioClip armorPickup4;
    public AudioClip armorPickup5;
    public AudioClip armorPickup6;
    public AudioClip xrayPickup1;
    public AudioClip xrayPickup2;
    public AudioClip xrayPickup3;
    public AudioClip xrayPickup4;
    public AudioClip xrayPickup5;
    public AudioClip xrayPickup6;
    public AudioClip xrayPickup7;
    public AudioClip xrayPickup8;
    public AudioClip HPPickup1;
    public AudioClip HPPickup2;
    public AudioClip HPPickup3;
    public AudioClip HPPickup4;
    #endregion

    #region round declarations

    public AudioClip blasterRound1;
    public AudioClip blasterRound2;
    public AudioClip blasterRound3;
    public AudioClip blasterRound4;
    public AudioClip blasterRound5;
    public AudioClip blasterRound6;
    public AudioClip blasterRound7;
    public AudioClip blasterRound8;

    public AudioClip grenadeRound1;
    public AudioClip grenadeRound2;
    public AudioClip grenadeRound3;
    public AudioClip grenadeRound4;
    public AudioClip grenadeRound5;
    public AudioClip grenadeRound6;

    public AudioClip machineRound1;
    public AudioClip machineRound2;
    public AudioClip machineRound3;
    public AudioClip machineRound4;
    public AudioClip machineRound5;
    public AudioClip machineRound6;
    public AudioClip machineRound7;
    public AudioClip machineRound8;

    public AudioClip missileRound1;
    public AudioClip missileRound2;
    public AudioClip missileRound3;
    public AudioClip missileRound4;
    public AudioClip missileRound5;
    public AudioClip missileRound6;
    public AudioClip missileRound7;
    public AudioClip missileRound8;

    public AudioClip shotgunRound1;
    public AudioClip shotgunRound2;
    public AudioClip shotgunRound3;
    public AudioClip shotgunRound4;
    public AudioClip shotgunRound5;
    public AudioClip shotgunRound6;
    public AudioClip shotgunRound7;
    public AudioClip shotgunRound8;

    public AudioClip sniperRound1;
    public AudioClip sniperRound2;
    public AudioClip sniperRound3;
    public AudioClip sniperRound4;
    public AudioClip sniperRound5;
    public AudioClip sniperRound6;
    #endregion

    #region mod declarations
    public AudioClip cineMod1;
    public AudioClip cineMod2;
    public AudioClip glowMod1;
    public AudioClip glowMod2;
    public AudioClip glowMod3;
    public AudioClip HPRegenMod1;
    public AudioClip HPRegenMod2;
    public AudioClip HPRegenMod3;
    public AudioClip invisMod1;
    public AudioClip invisMod2;
    public AudioClip invisMod3;
    public AudioClip invisMod4;
    public AudioClip invisMod5;
    public AudioClip largeMod1;
    public AudioClip largeMod2;
    public AudioClip largeMod3;
    public AudioClip shieldMod1;
    public AudioClip shieldMod2;
    public AudioClip speedMod1;
    public AudioClip speedMod2;
    public AudioClip trackingMod1;
    public AudioClip trackingMod2;
    public AudioClip vampMod1;
    public AudioClip vampMod2;
    #endregion

    #region kills
    public AudioClip kill1;
    public AudioClip kill2;
    public AudioClip kill3;
    public AudioClip kill4;
    public AudioClip kill5;
    public AudioClip kill6;
    public AudioClip kill7;
    public AudioClip kill8;
    public AudioClip kill9;
    public AudioClip kill10;
    public AudioClip kill11;
    public AudioClip kill12;
    public AudioClip kill13;
    public AudioClip kill14;
    public AudioClip kill15;
    public AudioClip kill16;
    #endregion

    #region player congrats
    public AudioClip playerOne1;
    public AudioClip playerOne2;
    public AudioClip playerOne3;
    public AudioClip playerTwo1;
    public AudioClip playerTwo2;
    public AudioClip playerThree1;
    public AudioClip playerThree2;
    public AudioClip playerThree3;
    public AudioClip playerFour1;
    public AudioClip playerFour2;
    #endregion

    #region round end
    public AudioClip roundOver1;
    public AudioClip roundOver2;
    public AudioClip roundOver3;
    public AudioClip roundOver4;
    public AudioClip roundOver5;
    public AudioClip roundOver6;
    public AudioClip roundOver7;
    public AudioClip roundOver8;
    public AudioClip roundOver9;
    #endregion

    #region round start
    public AudioClip roundStart1;
    public AudioClip roundStart2;
    public AudioClip roundStart3;
    public AudioClip roundStart4;
    public AudioClip roundStart5;
    #endregion

    #region exclamations
    public AudioClip exclaim1;
    public AudioClip exclaim2;
    public AudioClip exclaim3;
    public AudioClip exclaim4;
    public AudioClip exclaim5;
    public AudioClip exclaim6;
    public AudioClip exclaim7;
    #endregion

    #region ENTER THE RUBICON
    public AudioClip enter1;
    public AudioClip enter2;
    public AudioClip enter3;
    public AudioClip enter4;
    public AudioClip enter5;
    public AudioClip enter6;
    public AudioClip enter7;
    public AudioClip enter8;
    #endregion

    #endregion 

    // use this to play a random sound from each group
    public void PlaySound(string soundGroup) // sound groups are for example, the group of HP Regen vocal sounds. You would ask this to play an HP Regen mod sound.
    {
        // pickups
        if (soundGroup == "armorPickup")
        {

        }

        if (soundGroup == "xrayPickup")
        {

        }

        if (soundGroup == "hpPickup")
        {


        }

        // rounds
        if (soundGroup == "blasterRound")
        {

        }

        if (soundGroup == "grenadeRound")
        {

        }

        if (soundGroup == "machineRound")
        {

        }

        if (soundGroup == "missileRound")
        {

        }

        if (soundGroup == "shotgunRound")
        {

        }

        if (soundGroup == "sniperRound")
        {

        }

        // mods
        if (soundGroup == "cineMod")
        {

        }

        if (soundGroup == "glowingMod")
        {

        }

        if (soundGroup == "hpRegenMod")
        {

        }

        if (soundGroup == "invisMod")
        {

        }

        if (soundGroup == "largeMod")
        {

        }

        if (soundGroup == "shieldMod")
        {

        }

        if (soundGroup == "speedMod")
        {

        }

        if (soundGroup == "trackingMod")
        {

        }

        if (soundGroup == "vampMod")
        {

        }

        // kills
        if (soundGroup == "kill")
        {

        }

        // congrats
        if (soundGroup == "congratsPlayerOne")
        {

        }

        if (soundGroup == "congratsPlayerTwo")
        {

        }

        if (soundGroup == "congratsPlayerThree")
        {

        }

        if (soundGroup == "congratsPlayerFour")
        {

        }

        // round start
        if (soundGroup == "roundStart")

        // round over
        if (soundGroup == "roundOver")
        {

        }

        // exclamation
        if (soundGroup == "exclamation")
        {

        }

        // ENTER THE RUBICOCNN
        if (soundGroup == "enterTheRubicon")
        {

        }

    }
}
