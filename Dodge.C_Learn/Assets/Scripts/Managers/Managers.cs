using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    private static Managers instance;

    #region MonoBehaviour
    public static PopupManager Popup { get { return instance.popupManager; } }

    private PopupManager popupManager;                                                      //팝업매니저
    #endregion

    #region No MonoBehaviour
    public static EventManager Event { get { return instance.eventManager; } }
    public static SceneManagerEx Scene { get { return instance.sceneManager; } }

    private readonly SceneManagerEx sceneManager = new SceneManagerEx();                    //씬매니저확장
    private readonly EventManager eventManager = new EventManager();                        //이벤트매니저
    #endregion

    #region Container
    public static CharacterContainer Character { get { return instance.characterContainer; } }
    public static StageContainer Stage { get { return instance.stageContainer; } }

    private readonly CharacterContainer characterContainer = new CharacterContainer();
    private readonly StageContainer stageContainer = new StageContainer();
    #endregion

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        GameObject managers = new GameObject("Managers");
        instance = managers.AddComponent<Managers>();
        DontDestroyOnLoad(managers);

        instance.popupManager = instance.CreateManager<PopupManager>("PopupManager", managers.transform);

        Popup.Init();
        Character.Init();
        Stage.Init();
    }

    /// <summary>
    /// MonoBehaviour 상속받고있는 매니저 만들 때 객체만들어주는 함수
    /// </summary>
    private T CreateManager<T>(string name, Transform parent = null) where T : MonoBehaviour
    {
        GameObject manager = new GameObject(name);
        if(parent != null)
            manager.transform.parent = parent;

        return manager.AddComponent<T>();
    }
}