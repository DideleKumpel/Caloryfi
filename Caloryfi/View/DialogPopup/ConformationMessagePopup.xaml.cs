using CommunityToolkit.Maui.Views;

namespace Caloryfi.Views.DialogPopups;

public partial class ConformationMessagePopup : Popup
{
	public ConformationMessagePopup(string Message)
    {
        InitializeComponent();
        MessageLabel.Text = Message;
    }

    public void BtnYesClicked(object sender, EventArgs e)
    {
        Close(true);
    }

    private void BtnNoClicked(object sender, EventArgs e)
    {
        Close(false);
    }
}