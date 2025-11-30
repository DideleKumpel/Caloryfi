using CommunityToolkit.Maui.Views;

namespace Caloryfi.Views.DialogPopups;

public partial class InputPopup : Popup
{
	public InputPopup(string Message, Keyboard KeyboardType)
    {
        InitializeComponent();
        MessageLabel.Text = Message;
        EntryField.Keyboard = KeyboardType;
    }

    public void BtnYesClicked(object sender, EventArgs e)
    {
        Close(EntryField.Text);
    }

    private void BtnNoClicked(object sender, EventArgs e)
    {
        Close(false);
    }
}