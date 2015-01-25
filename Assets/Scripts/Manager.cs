using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Manager : MonoBehaviour
{
        // Playerプレハブ
        public GameObject player;

        // タイトル
        private GameObject title;

        // シェアのテクスチャ
        public Texture2D shareButtonImage;

        // iOS側のコードを呼び出すための処理
        [DllImport("__Internal")]
        private static extern void Shooting_Share (string text, string url, string textureUrl);

        void Start ()
        {
                // Titleゲームオブジェクトを検索し取得する
                title = GameObject.Find ("Title");
        }
/**
        void Update ()
        {
                // ゲーム中ではなく、Xキーが押されたらtrueを返す。
                if (IsPlaying () == false && Input.GetKeyDown (KeyCode.X)) {
                        GameStart ();
                }
        }

        void Update ()
        {

                for (int i = 0; i < Input.touchCount; i++) {

                        // タッチ情報を取得する
                        Touch touch = Input.GetTouch (i);

                        // ゲーム中ではなく、タッチ直後であればtrueを返す。
                        if (IsPlaying () == false && touch.phase == TouchPhase.Began) {
                                GameStart ();
                        }
                }

                // ゲーム中ではなく、マウスクリックされたらtrueを返す。
                if (IsPlaying () == false && Input.GetMouseButtonDown (0)) {
                        GameStart ();
                }
        } 

**/
        void OnGUI ()
        {
        	// シェアボタンを設置
                if (GUILayout.Button (shareButtonImage, GUIStyle.none, GUILayout.Width (128), GUILayout.Height (128))) {

                        // シェアする処理をコルーチンで実行
                        StartCoroutine (Share ());
                }

                // ゲーム中ではなく、タッチまたはマウスクリック直後であればtrueを返す。
                if (IsPlaying () == false && Event.current.type == EventType.MouseDown) {
                        GameStart ();
                }
        }               

        IEnumerator Share ()
        {
                // 現在の画面をキャプチャして名前をscreenShotとして保存する
                Application.CaptureScreenshot ("screenShot.png");

                // キャプチャを保存する処理として１フレーム待つ
                yield return new WaitForEndOfFrame ();

                string text = "2Dシューティング チュートリアル #unity";
                string url = "http://japan.unity3d.com/developer/document/tutorial/2d-shooting-game/ios/01.html";

                // Application.CaptureScreenshotの保存先はApplication.persistentDataPath
                string textureUrl = Application.persistentDataPath + "/screenShot.png";

                // iOS側の処理を呼び出す
                Shooting_Share (text, url, textureUrl);
        }

        void GameStart ()
        {
                // ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
                title.SetActive (false);
                Instantiate (player, player.transform.position, player.transform.rotation);
        }

        public void GameOver ()
        {
        		// ハイスコアの保存
                FindObjectOfType<Score>().Save();

                // ゲームオーバー時に、タイトルを表示する
                title.SetActive (true);
        }

        public bool IsPlaying ()
        {
                // ゲーム中かどうかはタイトルの表示/非表示で判断する
                return title && title.activeSelf == false;
        }
}
