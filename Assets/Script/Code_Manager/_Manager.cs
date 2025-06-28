using UnityEngine;

namespace Manager

{// ���͵����������࣬�̳��� MonoBehaviour
    public class _Manager<T> : MonoBehaviour where T : MonoBehaviour
    {
        // ����ʵ��
        public static T Instance
        {
            get
            {
                // ��ʵ��Ϊ�գ��ڳ����в���
                if (_instance == null)
                {
                    _instance = Object.FindAnyObjectByType<T>();
                    // ��������δ�ҵ�ʵ��
                    if (_instance == null)
                    {
                        Debug.Log($"����ģʽ {typeof(T).Name} �����в����ڣ����Դ���...");

                        // ���Ҵ��� "Manager" ��ǩ����Ϸ����
                        GameObject target = GameObject.FindGameObjectWithTag("Manager");
                        Debug.Log(target.name);
                        if (target == null)
                        {
                            // ��δ�ҵ�������һ���µ���Ϸ����
                            target = new GameObject();
                            target.name = $"����ģʽ������ {typeof(T).Name}";
                            Debug.Log("����");
                        }
                        try
                        {
                            // ������Ŀ����Ϸ������������
                            _instance = target.AddComponent<T>();
                        }
                        catch (System.Exception e)
                        {
                            // ����������ʧ�ܵ����
                            Debug.LogError($"�������� {typeof(T).Name} ʧ��: {e.Message}");
                        }
                    }
                }
                return _instance;
            }
            set
            {
                // ��ʵ��Ϊ�գ��������ֵ����ʵ��
                if (_instance == null)
                {
                    _instance = value;
                }
            }
        }
        // ��̬ʵ������
        private static T _instance;
        private void Awake()
        {
            // ��ʵ��Ϊ�գ�����ǰ����ֵ��ʵ��
            if (_instance == null)
            {
                _instance = this as T;
                // ��Ǹö����ڳ����л�ʱ��������
                DontDestroyOnLoad(_instance.gameObject);
            }
            else
            {
                // ��ʵ���Ѵ��ڣ����ٵ�ǰ����
                if (this != _instance)
                {
                    Destroy(gameObject);
                    Debug.Log("ɾ��");
                }
            }
        }
    }
}
