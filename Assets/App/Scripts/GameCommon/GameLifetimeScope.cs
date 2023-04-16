using App.Scripts.Fungus;
using App.Scripts.GameView;
using App.Scripts.Sound;
using App.Scripts.UI;
using VContainer;
using VContainer.Unity;

namespace App.Scripts.GameCommon
{
    public class GameLifetimeScope : LifetimeScope
    {
        private FlowchartController _flowchartController;
        private TitleBehavior _titleBehavior;
        private MainMenuBehavior _mainMenuBehavior;
        private SpotLightChangeView _spotLightChangeView;
        private SkipViewBehavior _skipViewBehavior;
        
        
        public void SetFlowchartController(FlowchartController flowchartController)
        {
            _flowchartController = flowchartController;
        }

        public void SetTitleBehavior(TitleBehavior titleBehavior)
        {
            _titleBehavior = titleBehavior;
        }
        public void SetMainMenuBehavior(MainMenuBehavior mainMenuBehavior)
        {
            _mainMenuBehavior = mainMenuBehavior;
        }
        public void SetSpotLightChangeView(SpotLightChangeView spotLightChangeView)
        {
            _spotLightChangeView = spotLightChangeView;
        }

        public void SetSkipViewBehavior(SkipViewBehavior skipViewBehavior)
        {
            _skipViewBehavior = skipViewBehavior;
        }
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameModel.GameModel>(Lifetime.Singleton);
            builder.Register<GameSoundView>(Lifetime.Singleton);
            builder.RegisterComponent(_flowchartController);
            builder.RegisterComponent(_titleBehavior);
            builder.RegisterComponent(_mainMenuBehavior);
            builder.RegisterComponent(_spotLightChangeView);
            builder.RegisterComponent(_skipViewBehavior);
            builder.RegisterEntryPoint<GamePresenter>(Lifetime.Singleton);

        }


    }
}
