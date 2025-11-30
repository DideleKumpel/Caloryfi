using CommunityToolkit.Maui.Views;

namespace Caloryfi.Views.DialogPopups;

public partial class MessagePopup : Popup
{
	public MessagePopup(string Message)
    {
        InitializeComponent();
        MessageLabel.Text = Message;

    }

    public void BtnOkClicked(object sender, EventArgs e)
    {
        Close();
    }
}