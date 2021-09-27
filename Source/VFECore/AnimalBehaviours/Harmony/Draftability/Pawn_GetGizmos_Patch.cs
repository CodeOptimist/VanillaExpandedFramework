﻿using HarmonyLib;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI.Group;


namespace AnimalBehaviours
{



    /*This second Harmony postfix adds or removes gizmos from the pawn's gizmo list (which is actually IEnumerable)
     * 
     */
   /* [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("GetGizmos")]

    static class VanillaExpandedFramework_Pawn_GetGizmos_Patch
    {
        [HarmonyPostfix]

        public static void AddGizmo(Pawn __instance, ref IEnumerable<Gizmo> __result)
        {
         
            var pawn = __instance;
            var gizmos = __result.ToList();
           

            bool flagIsCreatureMine = pawn.Faction != null && pawn.Faction.IsPlayer;
            bool flagIsCreatureDraftable = AnimalCollectionClass.draftable_animals.ContainsKey(pawn);


            if (flagIsCreatureDraftable && flagIsCreatureMine && pawn.drafter != null)
            {


                Command_Toggle drafting_command = new Command_Toggle();
                drafting_command.toggleAction = delegate
                {
                    pawn.drafter.Drafted = !pawn.drafter.Drafted;
                };
                drafting_command.isActive = (() => pawn.drafter.Drafted);
                drafting_command.defaultLabel = (pawn.drafter.Drafted ? "CommandUndraftLabel" : "CommandDraftLabel").Translate();
                drafting_command.hotKey = KeyBindingDefOf.Command_ColonistDraft;
                drafting_command.defaultDesc = "CommandToggleDraftDesc".Translate();
                drafting_command.icon = ContentFinder<Texture2D>.Get("ui/commands/Draft", true);
                drafting_command.turnOnSound = SoundDefOf.DraftOn;
                drafting_command.groupKey = 81729172;
                drafting_command.turnOffSound = SoundDefOf.DraftOff;
                gizmos.Insert(0, drafting_command);


            }





            __result = gizmos;
        }






    }

    */
}
