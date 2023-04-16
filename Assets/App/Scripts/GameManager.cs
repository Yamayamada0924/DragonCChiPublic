using System;
using App.Scripts.AppDebug;
using App.Scripts.Fungus;
using App.Scripts.GameCommon;
using App.Scripts.GameView;
using App.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using Input = UnityEngine.Windows.Input;

namespace App.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager gameManager;

        [SerializeField]
        public GameLifetimeScope gameLifetimeScope;

        [SerializeField] private TitleBehavior titleBehavior;
        [SerializeField] private MainMenuBehavior mainMenuBehavior;
        [SerializeField] private FlowchartController flowchartController;
        [SerializeField] private SpotLightChangeView spotLightChangeView;
        [SerializeField] private SkipViewBehavior skipViewBehavior;

        [SerializeField] public Camera mainCamera;

        [SerializeField] protected CanvasScaler canvasScaler;

        [SerializeField] protected Canvas masterCanvas;

        [SerializeField] private UnityEngine.Playables.PlayableDirector debug0;
        [SerializeField] private UnityEngine.Playables.PlayableDirector debug1;
        
        // Start is called before the first frame update
        void Awake()
        {
            if (gameManager == null)
            {
                gameManager = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

#if DEBUG
            var debugManager = new GameObject("DebugManager");
            debugManager.AddComponent<DebugDragonParamViewer>();
            debugManager.AddComponent<DebugCommandManager>();
            debugManager.AddComponent<DebugFlagManager>();
#endif       
            StartGameModelView();
        }

        private void Update()
        {
#if DEBUG
            if (UnityEngine.Input.anyKeyDown)
            {
                if (UnityEngine.Input.GetKeyDown(KeyCode.F1))
                {
                    debug0.Play();
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.F2))
                {
                    debug1.Play();
                }
            }
#endif
        }
        
        // RefactorするならViewのどこかから上位のMasterCanvasを取得してscaleFactorを返す？
        public float GetScaleFactor()
        {
            return masterCanvas.scaleFactor;
        }

        private void StartGameModelView()
        {
            gameLifetimeScope.SetFlowchartController(flowchartController);
            gameLifetimeScope.SetTitleBehavior(titleBehavior);
            gameLifetimeScope.SetMainMenuBehavior(mainMenuBehavior);
            gameLifetimeScope.SetSkipViewBehavior(skipViewBehavior);
            gameLifetimeScope.SetSpotLightChangeView(spotLightChangeView);
            gameLifetimeScope.Build();
        }

        private void EndGameModelView()
        {
            gameLifetimeScope.DisposeCore();
        }


    }
}
