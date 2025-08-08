using UnityEngine.EventSystems;

namespace Haare.Client.Routine.SceneRoutine
{
    public interface ISceneWasLoaded : IEventSystemHandler
    {
        void OnSceneWasLoaded(object argument);
    }

}