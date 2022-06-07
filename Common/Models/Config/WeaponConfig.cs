using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace VanillaPlus.Common.Models.Config
{
    public class WeaponConfig : ItemConfig
    {
        public const int DEFAULT_DAMAGE = 1;

        [ReloadRequired]
        public int Damage { get; set; }
    }
}
