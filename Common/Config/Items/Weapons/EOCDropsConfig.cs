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
    [Label("$Mods.VanillaPlus.Config.Items.Weapons.EOCDrops")]
    public class EOCDropsConfig
    {
        public EyeballOnAStickConfig? EyeballOnAStick
        {
            get => _eyeballOnAStick;
            set => ElementConfigSetter(ref _eyeballOnAStick, value);
        }
        EyeballOnAStickConfig? _eyeballOnAStick;

        public FangOfCthulhuConfig? FangOfCthulhu
        {
            get => _fangOfCthulhu;
            set => ElementConfigSetter(ref _fangOfCthulhu, value);
        }
        FangOfCthulhuConfig? _fangOfCthulhu;

        public TearConfig? Tear
        {
            get => _tear;
            set => ElementConfigSetter(ref _tear, value);
        }
        TearConfig? _tear;

        public override bool Equals(object? obj)
        {
            if (obj is EOCDropsConfig other)
            {
                if (!Equals(EyeballOnAStick, other.EyeballOnAStick))
                    return false;

                if (!Equals(FangOfCthulhu, other.FangOfCthulhu))
                    return false;

                if (!Equals(Tear, other.Tear))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
