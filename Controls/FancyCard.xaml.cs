using Microsoft.Maui.Controls;

namespace SmartGroceryList.Controls;

public partial class FancyCard : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(FancyCard), default(string));

    public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(
        nameof(Subtitle), typeof(string), typeof(FancyCard), default(string));

    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
        nameof(ImageSource), typeof(ImageSource), typeof(FancyCard), default(ImageSource));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Subtitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public FancyCard()
    {
        InitializeComponent();
        TitleLabel.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
        SubtitleLabel.SetBinding(Label.TextProperty, new Binding(nameof(Subtitle), source: this));
    }
}
