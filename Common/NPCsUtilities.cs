using Terraria;
using Terraria.ID;

namespace VanillaPlus.Common
{
    class NPCsUtilities
    {
        static public void Kill(int npcID)
        {
            NPC npc = Main.npc[npcID];
            npc.active = false;
            npc.life = -1;
            if (Main.netMode != NetmodeID.MultiplayerClient)
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
    }
}
