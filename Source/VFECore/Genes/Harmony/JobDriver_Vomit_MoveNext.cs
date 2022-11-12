﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld.QuestGen;

namespace VanillaGenesExpanded
{




    [HarmonyPatch]

    public static class VanillaGenesExpanded_JobDriver_Vomit_MoveNext_Patch
    {

        static MethodBase TargetMethod()
        {
            MethodBase method = typeof(JobDriver_Vomit).GetNestedType("<MakeNewToils>d__4", BindingFlags.Instance | BindingFlags.NonPublic).GetMethod("MoveNext", BindingFlags.Instance | BindingFlags.NonPublic);
            return method;
        }

       

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var codes = codeInstructions.ToList();
            var field = AccessTools.Field(typeof(EffecterDefOf), "Vomit");
            var pawnField = AccessTools.Field(typeof(JobDriver_Vomit), "pawn");
            foreach (var code in codes)
            {

                if (code.opcode == OpCodes.Ldsfld && code.LoadsField(field))
                {

                    yield return new CodeInstruction(OpCodes.Ldfld, pawnField);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(VanillaGenesExpanded_JobDriver_Vomit_MoveNext_Patch), nameof(GetVomitEffecter)));
                }
                else yield return code;
            }
        }

        public static EffecterDef GetVomitEffecter(Pawn pawn)
        {
            
            if (pawn!=null&&StaticCollectionsClass.vomitEffect_gene_pawns.ContainsKey(pawn))
            {
                return StaticCollectionsClass.vomitEffect_gene_pawns[pawn];
            }
            return EffecterDefOf.Vomit;

        }


    }





}
