using UnityEngine;

namespace Manager
{
    // 单例模式管理基类，继承自 MonoBehaviour
    public class _Manager<T> : MonoBehaviour where T : MonoBehaviour
    {
        // 单例实例
        public static T Instance
        {
            get
            {
                // 如果实例为空，在场景中查找
                if (_instance == null)
                {
                    _instance = Object.FindAnyObjectByType<T>();
                    // 如果场景中未找到实例
            if (_instance == null)
            {
                        Debug.Log($"单例模式 {typeof(T).Name} 在场景中不存在，尝试创建...");

                        // 查找带有"Manager"标签的游戏对象
                        GameObject target = GameObject.FindGameObjectWithTag("Manager");
                        Debug.Log(target.name);
                        if (target == null)
                        {
                            // 如果未找到则创建一个新的游戏对象
                            target = new GameObject();
                            target.name = $"单例模式管理器 {typeof(T).Name}";
                            Debug.Log("创建");
            }
                        try
            {
                            // 在目标游戏对象上添加组件
                            _instance = target.AddComponent<T>();
                        }
                        catch (System.Exception e)
                {
                            // 处理组件添加失败的情况
                            Debug.LogError($"创建实例 {typeof(T).Name} 失败: {e.Message}");
                }
            }
        }
                return _instance;
    }
            set
            {
                // 如果实例为空，将值赋给实例
                if (_instance == null)
                {
                    _instance = value;
}
            }
        }
        // 静态实例字段
        private static T _instance;
        private void Awake()
        {
            // 如果实例为空，将当前对象赋值给实例
            if (_instance == null)
            {
                _instance = this as T;
                // 标记该对象在场景加载时不销毁
                DontDestroyOnLoad(_instance.gameObject);
            }
            else
            {
                // 如果实例已存在，销毁当前对象
                if (this != _instance)
                {
                    Destroy(gameObject);
                    Debug.Log("删除");
                }
            }
        }
    }
}
