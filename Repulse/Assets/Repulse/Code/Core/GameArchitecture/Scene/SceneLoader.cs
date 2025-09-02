using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadMainMenu() => LoadScene(Scenes.MainMenu);
    public static void LoadGameScene() => LoadScene(Scenes.Game);
}
