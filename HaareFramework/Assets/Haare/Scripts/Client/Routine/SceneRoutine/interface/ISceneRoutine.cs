using R3;
using UnityEngine.SceneManagement;

namespace Haare.Client.Routine.SceneRoutine
{
    public interface ISceneRoutine
    {
        ReactiveProperty<SceneLoadPhase> CurrentPhase
        {  get; }

        public void LoadSceneWithLoad(SceneName scene, LoadSceneMode mode = LoadSceneMode.Single);
    }
}