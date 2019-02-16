using System;
using System.Collections.Generic;
using System.Text;
using ASCIIEngine.BasicClasses;

namespace ASCIIGame.Objects
{
    public class GameManager : GameObject
    {
        public override bool HasCollider => false;
        public override string ID => "gameManager";

        public Action ResetBonus;

        public int Score { get; private set; }

        public GameManager(Action resetBonus)
        {
            ResetBonus = resetBonus;
        }

        public override void Step()
        {
            base.Step();
        }

        public void OnBonus()
        {
            Score++;
            ResetBonus();
        }

        public void OnEnemy()
        {
            Score = 0;
        }
    }
}
