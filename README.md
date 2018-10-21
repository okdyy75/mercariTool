# mercariTool
メルカリ出品ツール

## 概要
メルカリで出品した商品の、再出品と削除ができるツール

### 環境
- Visual Studio 2017 Community

#### ソース管理初期セットアップメモ
1. GitHub上でリポジトリ作成
1. Visual Studio上でGitHubに接続し、作成したリポジトリをクローン（C:\Users\xxxxx\source\repos\okdyy75\mercariTool）
1. クローンしたフォルダに作成したプロジェクトのソースをそのままコピー
1. Visual Studio上で.gitignoreファイルなどを設定して、プッシュ

### 実行手順
1. .slhファイルをクリックしてVisual Studio起動
1. ソリューションをリビルドしてexeを作成
1. exeが作成されたフォルダに設定ファイル「init.txt」を設置
```
[global]
PHPSESSID = XXXXXXXXXX
```

### 使い方
1. exeファイルを実行するとWindowsアプリケーションが起動する。
1. [PHPSESSIDログイン]ボタンをクリック。左のラベルが青になったらログイン成功。
1. [出品一覧取得]ボタンをクリックして、商品データが一覧で表示される。
1. [全部再出品]ボタンをクリックすると、ステータスが[出品中]の商品をすべて再出品する。
1. [選択削除]ボタンをクリックすると、商品データ一覧でチェックを付けたものが削除される。
1. [選択削除]のヘッダーをクリックすると、一括チェックができる。
1. 商品データ一覧で[出品]ボタンをクリックすると、同じ商品データで再出品される。

![mercariTool画面キャプチャ.bmp](https://raw.github.com/wiki/okdyy75/mercariTool/images/mercariTool画面キャプチャ.bmp)
