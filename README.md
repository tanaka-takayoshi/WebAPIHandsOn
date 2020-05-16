
# ハンズオン手順

## プロジェクトを作る

WebAPI (ASP.NET Core)を作ってもらう

- VSの場合: プロジェクトでASP.NET Core Webアプリケーション > API (HTTPSを設定、Dockerサポートは任意（今回は使わない）)
- VS Codeの場合:  `dotnet new webapi -o WebAPIHandsOn`

実行して、プログラムが動くことを確認する。共有コンソールで実行してみる。

```
> (curl http://localhost:5080/weatherforecast).Content
```

また、こちらで動かしているサーバーにport 5080, 5081でアクセスできることを確認する。

https://localhost:5081/weatherforecast

### プロジェクトの構造を確認

#### Program.cs

エントリポイント。ASP.NET (.NET Framework)と異なり、mainメソッドがある。
ここで、Web HostというASP.NET Coreの基盤的なものを起動する。
今回は編集しないが、ロギングの設定や環境変数を扱う場合にさわることがある。

#### Startup.cs

ASP.NET Coreの起動時の設定を行うクラス。
ConfigureServiesでDIの設定を行う。
ASP.NET CoreはDI前提で構築されているので、特段理由がなければそのDIの仕組みを使うのがよい。
ConfigureメソッドはHTTPのリクエストパイプラインの設定を行う。

今回は触らないが、環境をDeveloperment,Productionの用に切り分けることができる。
ソースコードは同じまま、起動時の環境変数などで分けられる。

#### Controllers

MVCのコントローラー。
APIのコントローラーはControlelerBaseを拡張させて、ApiController属性をつける。
Controllerクラスを継承するはViewの機能があるのでおすすめではない。
属性ベースルーティングが使える。
[controller] でクラス名を利用。

## Controllerクラスの追加

Lab1Controllerを追加。

以下のNuGetライブラリを追加する。
> Install-Package Rick.Docs.Samples.RouteInfo -Version 1.0.0.4

お手本のようにコードを追加する。

起動してルーティングについて確認する。

curl https://localhost:5081/api/lab1	 => デフォルトではHTTPGETのところへ
curl https://localhost:5081/api/lab1/xyz => idがついたでのHTTPGetでパラメーターつきのメソッドへ
curl https://localhost:5081/api/lab1/int/3 => 
curl https://localhost:5081/api/lab1/int/a => intでないので404
curl https://localhost:5081/api/lab1/int2/1
curl https://localhost:5081/api/lab1/int2/a => intではないがRouteで制限されていないためメソッドが実行されるようとするが、型が違うので400エラー



### DIでの設定の挿入

ControllerクラスでDBアクセスやセッションなどASP.NET Coreで提供されている機能を利用する場合、DIを設定する。
今回は単純のためにすでに利用可能なConfigurationをDIしてみる。

```
private readonly IConfiguration configuration;

public Lab1Controller(IConfiguration configuration)
{
    this.configuration = configuration;
    var env = configuration["ASPNETCORE_ENVIRONMENT"];
}
```

なお、運用アプリでは、Configurationを直接DIするよりも、必要な設定をクラスごとに分離して、クラス単位でDIするのがおすすめ。

## Startupに戻ってURLの小文字化など

URLのパスやクエリ文字列の先頭が大文字なのはC#っぽいが、小文字にしたい場合はここで設定する。

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    //URLとクエリ文字列を小文字にする
    services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options => {
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    });
}
```

### Routeからapiを削除する

ControllerクラスのRouteアノテーションのパスを変更することができる。

## もう一つControllerクラスの追加

Lab2Controllerを作ってお手本のコードを追加。

```
> Invoke-RestMethod -Uri https://localhost:5081/api/lab2 -Method POST -Body (ConvertTo-Json @{"Name"="Hoge"}) -ContentType "application/json"
> Invoke-RestMethod -Uri https://localhost:5081/api/lab2 -Method POST -Body (ConvertTo-Json @{"Name"="Fuga"}) -ContentType "application/json"
```


```
> Invoke-RestMethod -Uri https://localhost:5081/api/lab2/2
> Invoke-RestMethod -Uri https://localhost:5081/api/lab2/getbyname?name=Hoge
> Invoke-RestMethod -Uri https://localhost:5081/api/lab2/getbyname?name=Ho
  Invoke-RestMethod -Uri https://localhost:5081/api/lab2/getbyname
```

## おまけ

//XMLを使う場合
//https://docs.microsoft.com/ja-jp/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1#input-formatters
//[Consumes("application/xml")]


