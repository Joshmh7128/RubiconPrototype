﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // this script does the sound
    // there's a lot okay

    public AudioSource announcerMain;
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
            int choice = Random.Range(1, 7);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = armorPickup1;
                    break;

                case 2:
                    announcerMain.clip = armorPickup2;
                    break;

                case 3:
                    announcerMain.clip = armorPickup3;
                    break;

                case 4:
                    announcerMain.clip = armorPickup4;
                    break;

                case 5:
                    announcerMain.clip = armorPickup5;
                    break;

                case 6:
                    announcerMain.clip = armorPickup6;
                    break;
            }
            // play the sound
            announcerMain.Play();

        }

        if (soundGroup == "xrayPickup")
        {
            int choice = Random.Range(1, 9);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = xrayPickup1;
                    break;
                case 2:
                    announcerMain.clip = xrayPickup2;
                    break;
                case 3:
                    announcerMain.clip = xrayPickup3;
                    break;
                case 4:
                    announcerMain.clip = xrayPickup4;
                    break;
                case 5:
                    announcerMain.clip = xrayPickup5;
                    break;
                case 6:
                    announcerMain.clip = xrayPickup6;
                    break;
                case 7:
                    announcerMain.clip = xrayPickup7;
                    break;
                case 8:
                    announcerMain.clip = xrayPickup8;
                    break;

            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "hpPickup")
        {
            int choice = Random.Range(1, 5);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = HPPickup1;
                    break;
                case 2:
                    announcerMain.clip = HPPickup2;
                    break;
                case 3:
                    announcerMain.clip = HPPickup3;
                    break;
                case 4:
                    announcerMain.clip = HPPickup4;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        // rounds
        if (soundGroup == "blasterRound")
        {
            int choice = Random.Range(1, 9);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = blasterRound1;
                    break;
                case 2:
                    announcerMain.clip = blasterRound2;
                    break;
                case 3:
                    announcerMain.clip = blasterRound3;
                    break;
                case 4:
                    announcerMain.clip = blasterRound4;
                    break;
                case 5:
                    announcerMain.clip = blasterRound5;
                    break;
                case 6:
                    announcerMain.clip = blasterRound6;
                    break;
                case 7:
                    announcerMain.clip = blasterRound7;
                    break;
                case 8:
                    announcerMain.clip = blasterRound8;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "grenadeRound")
        {
            int choice = Random.Range(1, 7);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = grenadeRound1;
                    break;
                case 2:
                    announcerMain.clip = grenadeRound2;
                    break;
                case 3:
                    announcerMain.clip = grenadeRound3;
                    break;
                case 4:
                    announcerMain.clip = grenadeRound4;
                    break;
                case 5:
                    announcerMain.clip = grenadeRound5;
                    break;
                case 6:
                    announcerMain.clip = grenadeRound6;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "machineRound")
        {
            int choice = Random.Range(1, 9);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = machineRound1;
                    break;
                case 2:
                    announcerMain.clip = machineRound2;
                    break;
                case 3:
                    announcerMain.clip = machineRound3;
                    break;
                case 4:
                    announcerMain.clip = machineRound4;
                    break;
                case 5:
                    announcerMain.clip = machineRound5;
                    break;
                case 6:
                    announcerMain.clip = machineRound6;
                    break;
                case 7:
                    announcerMain.clip = machineRound7;
                    break;
                case 8:
                    announcerMain.clip = machineRound8;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "missileRound")
        {
            int choice = Random.Range(1, 9);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = missileRound1;
                    break;
                case 2:
                    announcerMain.clip = missileRound2;
                    break;
                case 3:
                    announcerMain.clip = missileRound3;
                    break;
                case 4:
                    announcerMain.clip = missileRound4;
                    break;
                case 5:
                    announcerMain.clip = missileRound5;
                    break;
                case 6:
                    announcerMain.clip = missileRound6;
                    break;
                case 7:
                    announcerMain.clip = missileRound7;
                    break;
                case 8:
                    announcerMain.clip = missileRound8;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "shotgunRound")
        {
            int choice = Random.Range(1, 9);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = shotgunRound1;
                    break;
                case 2:
                    announcerMain.clip = shotgunRound2;
                    break;
                case 3:
                    announcerMain.clip = shotgunRound3;
                    break;
                case 4:
                    announcerMain.clip = shotgunRound4;
                    break;
                case 5:
                    announcerMain.clip = shotgunRound5;
                    break;
                case 6:
                    announcerMain.clip = shotgunRound6;
                    break;
                case 7:
                    announcerMain.clip = shotgunRound7;
                    break;
                case 8:
                    announcerMain.clip = shotgunRound8;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "sniperRound")
        {
            int choice = Random.Range(1, 7);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = sniperRound1;
                    break;
                case 2:
                    announcerMain.clip = sniperRound2;
                    break;
                case 3:
                    announcerMain.clip = sniperRound3;
                    break;
                case 4:
                    announcerMain.clip = sniperRound4;
                    break;
                case 5:
                    announcerMain.clip = sniperRound5;
                    break;
                case 6:
                    announcerMain.clip = sniperRound6;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        // mods
        if (soundGroup == "cineMod")
        {
            int choice = Random.Range(1, 3);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = cineMod1;
                    break;
                case 2:
                    announcerMain.clip = cineMod2;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "glowingMod")
        {
            int choice = Random.Range(1, 4);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = glowMod1;
                    break;
                case 2:
                    announcerMain.clip = glowMod2;
                    break;
                case 3:
                    announcerMain.clip = glowMod3;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "hpRegenMod")
        {
            int choice = Random.Range(1, 4);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = HPRegenMod1;
                    break;
                case 2:
                    announcerMain.clip = HPRegenMod1;
                    break;
                case 3:
                    announcerMain.clip = HPRegenMod1;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "invisMod")
        {
            int choice = Random.Range(1, 6);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = invisMod1;
                    break;
                case 2:
                    announcerMain.clip = invisMod2;
                    break;
                case 3:
                    announcerMain.clip = invisMod3;
                    break;
                case 4:
                    announcerMain.clip = invisMod4;
                    break;
                case 5:
                    announcerMain.clip = invisMod5;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "largeMod")
        {
            int choice = Random.Range(1, 4);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = largeMod1;
                    break;
                case 2:
                    announcerMain.clip = largeMod2;
                    break;
                case 3:
                    announcerMain.clip = largeMod3;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "shieldMod")
        {
            int choice = Random.Range(1, 3);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = shieldMod1;
                    break;
                case 2:
                    announcerMain.clip = shieldMod2;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "speedMod")
        {
            int choice = Random.Range(1, 3);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = speedMod1;
                    break;
                case 2:
                    announcerMain.clip = speedMod1;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "trackingMod")
        {
            int choice = Random.Range(1, 3);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = trackingMod1;
                    break;
                case 2:
                    announcerMain.clip = trackingMod2;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "vampMod")
        {
            int choice = Random.Range(1, 3);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = vampMod1;
                    break;
                case 2:
                    announcerMain.clip = vampMod2;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        // kills
        if (soundGroup == "kill")
        {
            int choice = Random.Range(1, 17);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = kill1;
                    break;
                case 2:
                    announcerMain.clip = kill2;
                    break;
                case 3:
                    announcerMain.clip = kill3;
                    break;
                case 4:
                    announcerMain.clip = kill4;
                    break;
                case 5:
                    announcerMain.clip = kill5;
                    break;
                case 6:
                    announcerMain.clip = kill6;
                    break;
                case 7:
                    announcerMain.clip = kill7;
                    break;
                case 8:
                    announcerMain.clip = kill8;
                    break;
                case 9:
                    announcerMain.clip = kill9;
                    break;
                case 10:
                    announcerMain.clip = kill10;
                    break;
                case 11:
                    announcerMain.clip = kill11;
                    break;
                case 12:
                    announcerMain.clip = kill12;
                    break;
                case 13:
                    announcerMain.clip = kill13;
                    break;
                case 14:
                    announcerMain.clip = kill14;
                    break;
                case 15:
                    announcerMain.clip = kill15;
                    break;
                case 16:
                    announcerMain.clip = kill16;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        // congrats
        if (soundGroup == "congratsPlayerOne")
        {
            int choice = Random.Range(1, 4);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = playerOne1;
                    break;
                case 2:
                    announcerMain.clip = playerOne2;
                    break;
                case 3:
                    announcerMain.clip = playerOne3;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "congratsPlayerTwo")
        {
            int choice = Random.Range(1, 3);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = playerTwo1;
                    break;
                case 2:
                    announcerMain.clip = playerTwo2;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "congratsPlayerThree")
        {
            int choice = Random.Range(1, 4);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = playerThree1;
                    break;
                case 2:
                    announcerMain.clip = playerThree2;
                    break;
                case 3:
                    announcerMain.clip = playerThree3;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        if (soundGroup == "congratsPlayerFour")
        {
            int choice = Random.Range(1, 3);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = playerFour1;
                    break;
                case 2:
                    announcerMain.clip = playerFour2;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        // round start
        if (soundGroup == "roundStart")
        {
            int choice = Random.Range(1, 6);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = roundStart1;
                    break;
                case 2:
                    announcerMain.clip = roundStart2;
                    break;
                case 3:
                    announcerMain.clip = roundStart3;
                    break;
                case 4:
                    announcerMain.clip = roundStart4;
                    break;
                case 5:
                    announcerMain.clip = roundStart5;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        // round over
        if (soundGroup == "roundOver")
        {
            int choice = Random.Range(1, 10);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = roundOver1;
                    break;
                case 2:
                    announcerMain.clip = roundOver2;
                    break;
                case 3:
                    announcerMain.clip = roundOver3;
                    break;
                case 4:
                    announcerMain.clip = roundOver4;
                    break;
                case 5:
                    announcerMain.clip = roundOver5;
                    break;
                case 6:
                    announcerMain.clip = roundOver6;
                    break;
                case 7:
                    announcerMain.clip = roundOver7;
                    break;
                case 8:
                    announcerMain.clip = roundOver8;
                    break;
                case 9:
                    announcerMain.clip = roundOver9;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        // exclamation
        if (soundGroup == "exclamation")
        {
            int choice = Random.Range(1, 8);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = exclaim1;
                    break;
                case 2:
                    announcerMain.clip = exclaim2;
                    break;
                case 3:
                    announcerMain.clip = exclaim3;
                    break;
                case 4:
                    announcerMain.clip = exclaim4;
                    break;
                case 5:
                    announcerMain.clip = exclaim5;
                    break;
                case 6:
                    announcerMain.clip = exclaim6;
                    break;
                case 7:
                    announcerMain.clip = exclaim7;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

        // ENTER THE RUBICOCNN
        if (soundGroup == "enterTheRubicon")
        {
            int choice = Random.Range(1, 9);

            // choose a sound
            switch (choice)
            {
                case 1:
                    announcerMain.clip = enter1;
                    break;
                case 2:
                    announcerMain.clip = enter2;
                    break;
                case 3:
                    announcerMain.clip = enter3;
                    break;
                case 4:
                    announcerMain.clip = enter4;
                    break;
                case 5:
                    announcerMain.clip = enter5;
                    break;
                case 6:
                    announcerMain.clip = enter6;
                    break;
                case 7:
                    announcerMain.clip = enter7;
                    break;
                case 8:
                    announcerMain.clip = enter8;
                    break;
            }
            // play the sound
            announcerMain.Play();
        }

    }
}
