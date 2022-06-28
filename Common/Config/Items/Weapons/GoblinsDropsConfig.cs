using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;
using VanillaPlus.Common.Config.Global;
using VanillaPlus.Common.Config.Items.Weapons;
using VanillaPlus.Common.Config.Items.Weapons.EOCDrops;
using VanillaPlus.Common.Config.Items.Weapons.EvilMaces;
using VanillaPlus.Common.Models.Config;

using static VanillaPlus.Common.Config.VanillaPlusServerConfig;

namespace VanillaPlus.Common.Config.Items.Weapons
{
    [Label("$Mods.VanillaPlus.Config.Items.Weapons.GoblinsDrops")]
    public class GoblinsDropsConfig
    {
        public GoblinsBladeConfig? GoblinsBlade
        {
            get => _goblinsBlade;
            set => ElementConfigSetter(ref _goblinsBlade, value);
        }
        GoblinsBladeConfig? _goblinsBlade;

        public ThiefsDaggerConfig? ThiefsDagger
        {
            get => _thiefsDagger;
            set => ElementConfigSetter(ref _thiefsDagger, value);
        }
        ThiefsDaggerConfig? _thiefsDagger;

        public WarriorsMalletConfig? WarriorsMallet
        {
            get => _warriorsMallet;
            set => ElementConfigSetter(ref _warriorsMallet, value);
        }
        WarriorsMalletConfig? _warriorsMallet;

        public override bool Equals(object? obj)
        {
            if (obj is GoblinsDropsConfig other)
            {
                if (!Equals(GoblinsBlade, other.GoblinsBlade))
                    return false;

                if (!Equals(ThiefsDagger, other.ThiefsDagger))
                    return false;

                if (!Equals(WarriorsMallet, other.WarriorsMallet))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
