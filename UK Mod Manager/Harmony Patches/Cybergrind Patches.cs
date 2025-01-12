﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UMM.Loader;

namespace UMM.HarmonyPatches
{
    [HarmonyPatch(typeof(FinalCyberRank), "GameOver")]
    public static class Ensure_NoSubmitBadScore
    {
        public static bool Prefix()
        {
            bool flag = UKAPI.ShouldSubmitCyberGrindScore();
            if (!flag)
                StatsManager.Instance.majorUsed = true;
            Debug.Log("Flag is " + flag);
            return true;
        }
    }

    [HarmonyPatch(typeof(SteamController), "SubmitCyberGrindScore")]
    public static class Ensure_NoSubmitBadScoreRedundant // I am very paranoid
    {
        public static bool Prefix()
        {
            bool flag = UKAPI.ShouldSubmitCyberGrindScore();
            Debug.Log("Should submit cybergrind score is " + flag);
            return flag;
        }
    }
}