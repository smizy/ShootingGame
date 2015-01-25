// Unityのバージョンが4.3.4以前であれば
#if UNITY_VERSION <= 434

// Unity4.3.4ではUnityGetGLViewControllerを使用するためにiPhone_Viewのインポートが必要
#import "iPhone_View.h"

#endif

extern "C" {

    void Shooting_Share(const char *text, const char *url, const char *textureURL) {

        // NSStringに変換
        NSString *_text = [NSString stringWithUTF8String:text];
        NSString *_url = [NSString stringWithUTF8String:url];
        NSString *_textureURL = [NSString stringWithUTF8String:textureURL];

        UIImage *image = nil;

        // パスから画像を取得
        if ([_textureURL length] != 0) {
            image = [UIImage imageWithContentsOfFile:_textureURL];
        }

        // テキスト・URL・画像の順に配列を作成する
        NSArray *actItems = [NSArray arrayWithObjects:_text, _url, image, nil];

        // UIActivityViewを作成する
        UIActivityViewController *uiActivityViewController = [[[UIActivityViewController alloc] initWithActivityItems:actItems applicationActivities:nil] autorelease];


        // Unity画面の上にビューを表示させる
        [UnityGetGLViewController() presentViewController:uiActivityViewController animated:YES completion:nil];

    }

}