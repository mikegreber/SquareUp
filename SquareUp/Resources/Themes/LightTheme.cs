using CommunityToolkit.Maui.Markup;

namespace SquareUp.Resources.Themes;

public class LightTheme : ThemeBase
{
    // public override Style ShellStyle { get; } = new Style<Shell>(
    //     (Shell.NavBarHasShadowProperty, true),
    //     (Shell.TitleColorProperty, Colors.Black),
    //     (Shell.DisabledColorProperty, Colors.Black),
    //     (Shell.UnselectedColorProperty, Colors.Black),
    //     (Shell.ForegroundColorProperty, Colors.Black),
    //     (Shell.BackgroundColorProperty, Blue100Accent))
    //     .ApplyToDerivedTypes(true);

    public override Style EntryStyle { get; } = new Style<Entry>(
            (Entry.TextProperty, Gray950)
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
        (VisualElement.HeightProperty, 26)
    );

    public override Style CategoryHeaderStyle { get; } = new Style<Label>(
        (Label.TextColorProperty, Gray50),
        (Label.BackgroundColorProperty, Gray600),
        (Label.HorizontalOptionsProperty, LayoutOptions.Fill),
        (Label.PaddingProperty, new Thickness(12, 6))
    );

    public override Color PrimaryTextColor => Gray950;
    public override Color SecondaryTextColor => Gray900;
    public override Color PositiveTextColor => Colors.Green;
    public override Color NegativeTextColor => Colors.Red;
    public override Color PageBackgroundColor => Blue100Accent;
    public override Color PlaceholderTextColor => Gray100;
    public override Color TransactionCardBackgroundColor => Gray600;
    public override Color TransactionCardPrimaryTextColor => Gray10;
    public override Color TransactionCardSecondaryTextColor => Gray20;
    public override Color TransactionCardTertiaryTextColor => Gray50;
    public override Color DividerColor => Gray100;
}