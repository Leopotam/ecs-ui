# Интеграция uGui в LeoECS
Интеграция событий uGui в ECS-мир.

> Проверено на Unity 2020.3 (зависит от Unity) и содержит asmdef-описания для компиляции в виде отдельных сборок и уменьшения времени рекомпиляции основного проекта.

# Содержание
* [Социальные ресурсы](#Социальные-ресурсы)
* [Установка](#Установка)
    * [В виде unity модуля](#В-виде-unity-модуля)
    * [В виде исходников](#В-виде-исходников)
* [Основные типы](#Основные-типы)
    * [EcsUiEmitter](#EcsUiEmitter)
    * [Действия](#Действия)
    * [Компоненты](#Компоненты)
* [Инициализация](#Инициализация)
* [Лицензия](#Лицензия)

# Социальные ресурсы
[![discord](https://img.shields.io/discord/404358247621853185.svg?label=enter%20to%20discord%20server&style=for-the-badge&logo=discord)](https://discord.gg/5GZVde6)

# Установка

## В виде unity модуля
Поддерживается установка в виде unity-модуля через git-ссылку в PackageManager или прямое редактирование `Packages/manifest.json`:
```
"com.leopotam.ecs": "https://github.com/Leopotam/ecs-ui.git",
```
По умолчанию используется последняя релизная версия. Если требуется версия "в разработке" с актуальными изменениями - следует переключиться на ветку `develop`:
```
"com.leopotam.ecs": "https://github.com/Leopotam/ecs-ui.git#develop",
```

## В виде исходников
Код так же может быть склонирован или получен в виде архива со страницы релизов.

# Основные типы

## EcsUiEmitter
`EcsUiEmitter` является `MonoBehaviour`-классом, отвечающим за генерацию ECS-событий на основе uGui-событий (нажатие, отпускание, перетаскивание и т.п):
```c#
public class Startup : MonoBehaviour {
    // Поле должно быть проинициализировано в инспекторе средствами редактора Unity.
    [SerializeField] EcsUiEmitter _uiEmitter;

    EcsSystems _systems;

    void Start () {
        var world = new EcsWorld ();
        _systems = new EcsSystems (world)
            // Дополнительная инициализация...
            .Add (new TestSystem ())
            .InjectUi (_uiEmitter);
        _systems.Init ();
    }
}

public class TestSystem : IEcsInitSystem {
    // Поля с автоматической инъекцией.
    EcsUiEmitter _ui;
    [EcsUiNamed("MyButton")] GameObject _btnGo;
    [EcsUiNamed("MyButton")] Transform _btnTransform;
    [EcsUiNamed("MyButton")] Button _btn;

    public void Init () {
        // Все поля системы к этому моменту проинициализированы и могут быть использованы:
        // _ui - содержит ссылку на EcsUiEmitter.
        // _btnGo - содержит ссылку, аналог вызова _ui.GetNamedObject ("MyButton");
        // _btnTransform = содержит ссылку, аналог вызова _ui.GetNamedObject ("MyButton").GetComponent<Transform> ();
        // _btn = содержит ссылку, аналог вызова _ui.GetNamedObject ("MyButton").GetComponent<Button> ();
    }
}
```

> **ВАЖНО!** `EcsUiEmitter` должен быть добавлен к корневому `GameObject`, содержащему виджеты uGui.
> **ВАЖНО!** Не нужно принудительно выполнять инъекцию для `EcsUiEmitter` - вызов метода `EcsSystems.Inject()` делает это автоматически.

## Действия
Действия (классы `xxxAction`) - это `MonoBehaviour`-компоненты, которые слушают события uGui виджетов, ищут `EcsUiEmitter` по иерархии вверх и вызывают генерацию соответствующих событий для ECS-мира.

## Компоненты
ECS-компоненты, описывающие события: `EcsUiClickEvent`, `EcsUiBeginDragEvent`, `EcsUiEndDragEvent` and others - they can be used as ecs-components with standard filtering through `EcsFilter`:
```c#
public class TestUiClickEventSystem : IEcsRunSystem {
    EcsWorld _world;
    EcsFilter<EcsUiClickEvent> _clickEvents;

    public void Run () {
        foreach (var idx in _clickEvents) {
            ref EcsUiClickEvent data = ref _clickEvents.Get1 (idx);
            Debug.Log ("Im clicked!", data.Sender);
        }
    }
}
```

# Лицензия
Фреймворк выпускается под двумя лицензиями, [подробности тут](./LICENSE.md).

В случаях лицензирования по условиям MIT-Red не стоит расчитывать на
персональные консультации или какие-либо гарантии.