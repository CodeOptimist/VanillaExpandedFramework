﻿using System;
using System.Collections.Generic;
using RimWorld.BaseGen;
using Verse;

namespace KCSG
{
    internal class GenStep_CustomStructureGen : GenStep
    {
        public bool fullClear = false;

        public List<StructureLayoutDef> structureLayoutDefs = new List<StructureLayoutDef>();

        public List<string> symbolResolvers = new List<string>();

        public List<ThingDef> scatterThings = new List<ThingDef>();
        public List<ThingDef> filthTypes = new List<ThingDef>();
        public float scatterChance = 0.4f;

        // TODO Obsolete - To remove in next rimworld version
        [Obsolete]
        public bool shouldRuin = false;

        [Obsolete]
        public List<string> ruinSymbolResolvers = new List<string>();

        public override int SeedPart => 916595355;

        public override void Generate(Map map, GenStepParams parms)
        {
            // TODO Compat - To remove in next rimworld version
            if (!ruinSymbolResolvers.NullOrEmpty())
                symbolResolvers = ruinSymbolResolvers.ListFullCopy();

            GenOption.ext = new CustomGenOption
            {
                UsingSingleLayout = true,
                AdditionalResolvers = symbolResolvers.Count > 0,
                symbolResolvers = symbolResolvers,
                filthTypes = filthTypes,
                scatterThings = scatterThings,
                scatterChance = scatterChance,
            };

            StructureLayoutDef layoutDef = structureLayoutDefs.RandomElement();

            CellRect cellRect = CellRect.CenteredOn(map.Center, layoutDef.width, layoutDef.height);

            GenUtils.PreClean(map, cellRect, fullClear, layoutDef.roofGridResolved);
            GenUtils.GenerateLayout(layoutDef, cellRect, map);

            if (GenOption.ext.AdditionalResolvers)
            {
                Debug.Message("GenStep_CustomStructureGen - Additional symbol resolvers");
                BaseGen.symbolStack.Push("kcsg_runresolvers", new ResolveParams
                {
                    faction = map.ParentFaction,
                    rect = cellRect
                }, null);
            }

            // Flood refog
            if (map.mapPawns.FreeColonistsSpawned.Count > 0)
            {
                FloodFillerFog.DebugRefogMap(map);
            }
        }
    }
}