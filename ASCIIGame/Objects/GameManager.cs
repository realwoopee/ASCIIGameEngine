using System;
using ASCIIEngine.Core;
using ASCIIEngine.Core.BasicClasses;

namespace ASCIIGame.Objects
{
    public class GameManager : GameObject
    {
        public override bool HasCollider => false;
        public override string Tag => "gameManager";

        public Action ResetBonus;

        public int Score { get; private set; }

        public GameManager(Action resetBonus, Vector2D worldSize)
        {
            ResetBonus = resetBonus;
        }

        protected override void Update()
        {
            base.Update();
        }

        public void OnBonus()
        {
            Logger.PrintLine("Player ate a bonus.");
            Score++;
            ResetBonus();
        }

        public void OnEnemy()
        {
            Logger.PrintLine("GIMME UR POITNZ.");
            Score = 0;
        }
    }
}