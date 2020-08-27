using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace MaxedOutCompanions
{
    public class Main : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            if (isGameCampaign(game))
                AddGameWorkerToBehavior(gameStarter);
            StartTheGame(game);
            
        }
        /// <summary>
        /// Displays debug message on game load.
        /// </summary>
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            InformationManager.DisplayMessage(new InformationMessage("Maxed out companions " + ConfigReader.GetInstance().Get("ModVersion") + " by m1g4c"));
        }
        /// <summary>
        /// Starts game.
        /// </summary>
        /// <param name="game">Game</param>
        private void StartTheGame(Game game)
        {
            this.BeginGameStart(game);
        }
        /// <summary>
        /// Adds new behavior to game.
        /// </summary>
        /// <param name="gameStarter"></param>
        private void AddGameWorkerToBehavior(IGameStarter gameStarter)
        {
            ((CampaignGameStarter)gameStarter).AddBehavior((CampaignBehaviorBase)new HeroAttributeChanger());
        }
        /// <summary>
        /// Checks if game is campaign
        /// </summary>
        /// <param name="game">Game</param>
        /// <returns>True if it is, False if isnt</returns>
        private bool isGameCampaign(Game game)
        {
            return game.GameType is Campaign ? true : false;
        }
    }
}
