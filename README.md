
# �n���Y�I���菇

## �v���W�F�N�g�����

WebAPI (ASP.NET Core)������Ă��炤

- VS�̏ꍇ: �v���W�F�N�g��ASP.NET Core Web�A�v���P�[�V���� > API (HTTPS��ݒ�ADocker�T�|�[�g�͔C�Ӂi����͎g��Ȃ��j)
- VS Code�̏ꍇ:  `dotnet new webapi -o WebAPIHandsOn`

���s���āA�v���O�������������Ƃ��m�F����B���L�R���\�[���Ŏ��s���Ă݂�B

```
> (curl http://localhost:5080/weatherforecast).Content
```

�܂��A������œ������Ă���T�[�o�[��port 5080, 5081�ŃA�N�Z�X�ł��邱�Ƃ��m�F����B

https://localhost:5081/weatherforecast

### �v���W�F�N�g�̍\�����m�F

#### Program.cs

�G���g���|�C���g�BASP.NET (.NET Framework)�ƈقȂ�Amain���\�b�h������B
�����ŁAWeb Host�Ƃ���ASP.NET Core�̊�ՓI�Ȃ��̂��N������B
����͕ҏW���Ȃ����A���M���O�̐ݒ����ϐ��������ꍇ�ɂ���邱�Ƃ�����B

#### Startup.cs

ASP.NET Core�̋N�����̐ݒ���s���N���X�B
ConfigureServies��DI�̐ݒ���s���B
ASP.NET Core��DI�O��ō\�z����Ă���̂ŁA���i���R���Ȃ���΂���DI�̎d�g�݂��g���̂��悢�B
Configure���\�b�h��HTTP�̃��N�G�X�g�p�C�v���C���̐ݒ���s���B

����͐G��Ȃ����A����Developerment,Production�̗p�ɐ؂蕪���邱�Ƃ��ł���B
�\�[�X�R�[�h�͓����܂܁A�N�����̊��ϐ��Ȃǂŕ�������B

#### Controllers

MVC�̃R���g���[���[�B
API�̃R���g���[���[��ControlelerBase���g�������āAApiController����������B
Controller�N���X���p�������View�̋@�\������̂ł������߂ł͂Ȃ��B
�����x�[�X���[�e�B���O���g����B
[controller] �ŃN���X���𗘗p�B

## Controller�N���X�̒ǉ�

Lab1Controller��ǉ��B

�ȉ���NuGet���C�u������ǉ�����B
> Install-Package Rick.Docs.Samples.RouteInfo -Version 1.0.0.4

����{�̂悤�ɃR�[�h��ǉ�����B

�N�����ă��[�e�B���O�ɂ��Ċm�F����B

curl https://localhost:5081/api/lab1	 => �f�t�H���g�ł�HTTPGET�̂Ƃ����
curl https://localhost:5081/api/lab1/xyz => id�������ł�HTTPGet�Ńp�����[�^�[���̃��\�b�h��
curl https://localhost:5081/api/lab1/int/3 => 
curl https://localhost:5081/api/lab1/int/a => int�łȂ��̂�404
curl https://localhost:5081/api/lab1/int2/1
curl https://localhost:5081/api/lab1/int2/a => int�ł͂Ȃ���Route�Ő�������Ă��Ȃ����߃��\�b�h�����s�����悤�Ƃ��邪�A�^���Ⴄ�̂�400�G���[



### DI�ł̐ݒ�̑}��

Controller�N���X��DB�A�N�Z�X��Z�b�V�����Ȃ�ASP.NET Core�Œ񋟂���Ă���@�\�𗘗p����ꍇ�ADI��ݒ肷��B
����͒P���̂��߂ɂ��łɗ��p�\��Configuration��DI���Ă݂�B

```
private readonly IConfiguration configuration;

public Lab1Controller(IConfiguration configuration)
{
    this.configuration = configuration;
    var env = configuration["ASPNETCORE_ENVIRONMENT"];
}
```

�Ȃ��A�^�p�A�v���ł́AConfiguration�𒼐�DI��������A�K�v�Ȑݒ���N���X���Ƃɕ������āA�N���X�P�ʂ�DI����̂��������߁B

## Startup�ɖ߂���URL�̏��������Ȃ�

URL�̃p�X��N�G��������̐擪���啶���Ȃ̂�C#���ۂ����A�������ɂ������ꍇ�͂����Őݒ肷��B

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    //URL�ƃN�G����������������ɂ���
    services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options => {
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    });
}
```

### Route����api���폜����

Controller�N���X��Route�A�m�e�[�V�����̃p�X��ύX���邱�Ƃ��ł���B

## �������Controller�N���X�̒ǉ�

Lab2Controller������Ă���{�̃R�[�h��ǉ��B

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

## ���܂�

//XML���g���ꍇ
//https://docs.microsoft.com/ja-jp/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1#input-formatters
//[Consumes("application/xml")]


