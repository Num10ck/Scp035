using Exiled.API.Features;
using System;

namespace Scp035
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "Scp035";
        public override string Author => "Num1ock";
        public override Version Version => new Version(1, 0, 0);

        public override void OnEnabled()
        {

            base.OnEnabled();
        }

        public override void OnDisabled()
        {

            base.OnDisabled();
        }

        public void Spawn(Player ply, bool Spawned)
        {

        }
    }
}
