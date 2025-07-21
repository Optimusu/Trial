namespace Trial.AppFront.Layout;

public partial class MainLayout
{
    private bool isMenuVisible = false;

    private void ShowMenu() => isMenuVisible = true;
    private void HideMenu() => isMenuVisible = false;

}