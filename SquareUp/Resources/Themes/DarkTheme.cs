using CommunityToolkit.Maui.Markup;

namespace SquareUp.Resources.Themes;

public class DarkTheme : ThemeBase
{
    // public override Style ShellStyle { get; } = new Style<Shell>(
    //     (Shell.NavBarHasShadowProperty, false),
    //     (Shell.TitleColorProperty, Colors.White),
    //     (Shell.DisabledColorProperty, Colors.White),
    //     (Shell.UnselectedColorProperty, Colors.White),
    //     (Shell.ForegroundColorProperty, Colors.White),
    //     (Shell.BackgroundColorProperty, Colors.Blue))
    //     .ApplyToDerivedTypes(true);

    public override Style EntryStyle { get; } = new Style<Entry>(
        (Entry.TextColorProperty, Gray50),
        (Entry.PlaceholderColorProperty, Gray600),
        (Entry.BackgroundColorProperty, Black)
    ).ApplyToDerivedTypes(true);

    public override Style ButtonCreateStyle { get; } = new Style<Button>(
        (Button.TextColorProperty, White),
        (VisualElement.BackgroundColorProperty, Yellow100Accent)
    );

    public override Style ButtonCreateDisabledStyle { get; } = new Style<Button>(
        (Button.TextColorProperty, Gray100),
        (VisualElement.BackgroundColorProperty, Yellow300Accent),
        (VisualElement.IsEnabledProperty, false)
    );

    public override Style ButtonUpdateStyle { get; } = new Style<Button>(
        (Button.TextColorProperty, White),
        (VisualElement.BackgroundColorProperty, Yellow100Accent)
    );

    public override Style ButtonUpdateDisabledStyle { get; } = new Style<Button>(
        (Button.TextColorProperty, Gray100),
        (VisualElement.BackgroundColorProperty, Yellow300Accent),
        (VisualElement.IsEnabledProperty, false)
    );

    public override Style ButtonDeleteStyle { get; } = new Style<Button>(
        (Button.TextColorProperty, White),
        (VisualElement.BackgroundColorProperty, Cyan100Accent)
    );

    public override Style ButtonDeleteDisabledStyle { get; } = new Style<Button>(
        (Button.TextColorProperty, Gray100),
        (VisualElement.BackgroundColorProperty, Cyan300Accent),
        (VisualElement.IsEnabledProperty, false)
    );

    public override Style AltButtonStyle { get; } = new Style<Button>(
        (Button.TextColorProperty, White),
        (Button.CornerRadiusProperty, 13),
        (Button.FontSizeProperty, 12),
        (Button.PaddingProperty, 0),
        (VisualElement.BackgroundColorProperty, Blue100Accent),
        (VisualElement.HeightRequestProperty, 26)
    );

    public override Style CategoryHeaderStyle { get; } = new Style<Label>(
        (Label.TextColorProperty, Gray50),
        (Label.BackgroundColorProperty, Gray600),
        (Label.HorizontalOptionsProperty, LayoutOptions.Fill),
        (Label.PaddingProperty, new Thickness(12,6))
    );


    public override Color PrimaryTextColor => White;
    public override Color SecondaryTextColor => Gray50;
    public override Color PositiveTextColor => Colors.Green;
    public override Color NegativeTextColor => Colors.Red;
    public override Color PlaceholderTextColor => White70;
    public override Color PageBackgroundColor => Black;
    public override Color TransactionCardBackgroundColor => Gray600;
    public override Color TransactionCardPrimaryTextColor => Gray10;
    public override Color TransactionCardSecondaryTextColor => Gray20;
    public override Color TransactionCardTertiaryTextColor => Gray50;
    public override Color DividerColor => Gray900;
}
