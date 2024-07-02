namespace MauiApp1;

public partial class VideoWindo : ContentPage
{
	public VideoWindo()
	{
		InitializeComponent();
        LoadVideo();
	}
    private void LoadVideo()
    {
        var htmlSource = new HtmlWebViewSource
        {
            Html = @"
                <!DOCTYPE html>
                <html>
                <body>
                    <video width='100%' height='100%' controls autoplay muted>
                        <source src='https://shattereddisk.github.io/rickroll/rickroll.mp4' type='video/mp4'>
                        Your browser does not support the video tag.
                    </video>
                </body>
                </html>"
        };

        videoWebView.Source = htmlSource;
    }
}