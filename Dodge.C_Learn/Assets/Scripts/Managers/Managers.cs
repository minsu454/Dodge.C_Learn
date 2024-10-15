using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    private static Managers instance;

    #region MonoBehaviour
    public static PopupManager Popup { get { return instance.popupManager; } }
    //public static ObjectPoolManager ObjectPool { get { return instance.objectPoolManager; } }

    private PopupManager popupManager;                                                      //팝업매니저
    //private ObjectPoolManager objectPoolManager;                                            //오브젝트풀 매니저
    //private readonly ObjectContainer objectContainer;                                       //오브젝트풀 컨테이너
    #endregion

    #region No MonoBehaviour
    public static EventManager Event { get { return instance.eventManager; } }
    public static SceneManagerEx Scene { get { return instance.sceneManager; } }
    public static CharacterContainer PlayerClass { get { return instance.playerClassManager; } }

    private readonly SceneManagerEx sceneManager = new SceneManagerEx();                    //씬매니저확장
    private readonly EventManager eventManager = new EventManager();                        //이벤트매니저
    private readonly CharacterContainer playerClassManager = new CharacterContainer();
    #endregion

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        GameObject managers = new GameObject("Managers");
        instance = managers.AddComponent<Managers>();
        DontDestroyOnLoad(managers);

        instance.popupManager = instance.CreateManager<PopupManager>("PopupManager", managers.transform);

        Popup.Init();
        PlayerClass.Init();
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