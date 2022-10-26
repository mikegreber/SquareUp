using CommunityToolkit.Maui.Markup;

namespace SquareUp.Resources.Themes;

public abstract class ThemeBase : ResourceDictionary
{
    protected ThemeBase()
    {
        Add(nameof(Primary), Primary);
        Add(nameof(Secondary), Secondary);
        Add(nameof(Tertiary), Tertiary);
        Add(nameof(White), White);
        Add(nameof(White90), White90);
        Add(nameof(White80), White80);
        Add(nameof(White70), White70);
        Add(nameof(Black ),  Black );
        Add(nameof(Gray10), Gray10);
        Add(nameof(Gray20), Gray20);
        Add(nameof(Gray50), Gray50);
        Add(nameof(Gray100), Gray100);
        Add(nameof(Gray200), Gray200);
        Add(nameof(Gray300), Gray300);
        Add(nameof(Gray400), Gray400);
        Add(nameof(Gray450), Gray450);
        Add(nameof(Gray500), Gray500);
        Add(nameof(Gray600), Gray600);
        Add(nameof(Gray900), Gray900);
        Add(nameof(Gray950), Gray950);
        Add(nameof(Yellow100Accent), Yellow100Accent);
        Add(nameof(Yellow200Accent), Yellow200Accent);
        Add(nameof(Yellow300Accent), Yellow300Accent);
        Add(nameof(Cyan100Accent), Cyan100Accent);
        Add(nameof(Cyan200Accent), Cyan200Accent);
        Add(nameof(Cyan300Accent), Cyan300Accent);
        Add(nameof(Blue100Accent), Blue100Accent);
        Add(nameof(Blue200Accent), Blue200Accent);
        Add(nameof(Blue300Accent), Blue300Accent);
        Add(nameof(PrimaryBrush), PrimaryBrush);
        Add(nameof(SecondaryBrush), SecondaryBrush);
        Add(nameof(TertiaryBrush), TertiaryBrush);
        Add(nameof(WhiteBrush), WhiteBrush);
        Add(nameof(BlackBrush), BlackBrush);
        Add(nameof(Gray100Brush), Gray100Brush);
        Add(nameof(Gray200Brush), Gray200Brush);
        Add(nameof(Gray300Brush), Gray300Brush);
        Add(nameof(Gray400Brush), Gray400Brush);
        Add(nameof(Gray500Brush), Gray500Brush);
        Add(nameof(Gray600Brush), Gray600Brush);
        Add(nameof(Gray900Brush), Gray900Brush);
        Add(nameof(Gray950Brush), Gray950Brush);

        Add(nameof(PageBackgroundColor), PageBackgroundColor);
        Add(nameof(TransactionCardBackgroundColor), TransactionCardBackgroundColor);
        Add(nameof(TransactionCardPrimaryTextColor), TransactionCardPrimaryTextColor);
        Add(nameof(TransactionCardSecondaryTextColor), TransactionCardSecondaryTextColor);
        Add(nameof(TransactionCardTertiaryTextColor), TransactionCardTertiaryTextColor);
        Add(nameof(PrimaryTextColor), PrimaryTextColor);
        Add(nameof(SecondaryTextColor), SecondaryTextColor);
        Add(nameof(PositiveTextColor), PositiveTextColor);
        Add(nameof(NegativeTextColor), NegativeTextColor);
        Add(nameof(PlaceholderTextColor), PlaceholderTextColor);
        Add(nameof(DividerColor), DividerColor);

        //Add(ShellStyle); 
        Add(EntryStyle);
        Add(nameof(ButtonCreateStyle), ButtonCreateStyle);
        Add(nameof(CategoryHeaderStyle), CategoryHeaderStyle);
        Add(nameof(ButtonCreateDisabledStyle), ButtonCreateDisabledStyle);
        Add(nameof(ButtonDeleteStyle), ButtonDeleteStyle);
        Add(nameof(ButtonDeleteDisabledStyle), ButtonDeleteDisabledStyle);
        Add(nameof(ButtonUpdateStyle), ButtonUpdateStyle);
        Add(nameof(ButtonUpdateDisabledStyle), ButtonUpdateDisabledStyle);
        Add(nameof(AltButtonStyle), AltButtonStyle);
        Add(NavStyle);

    }

    public static Style NavStyle { get; } = new Style<NavigationPage>((NavigationPage.BarBackgroundColorProperty, Colors.Pink )).ApplyToDerivedTypes(true);

    public static Color Primary => Color.FromArgb("#512BD4");
    public static Color Secondary => Color.FromArgb("#DFD8F7");
    public static Color Tertiary => Color.FromArgb("#2B0B98");
    public static Color White => Colors.White;
    public static Color White90 => Color.FromRgba("E6FFFFFF");
    public static Color White80 => Color.FromRgba("CCFFFFFF");
    public static Color White70 => Color.FromRgba("B3FFFFFF");
    public static Color Black => Colors.Black;
    public static Color Gray10 => Color.FromArgb("#EEEEEE");
    public static Color Gray20 => Color.FromArgb("#DDDDDD");
    public static Color Gray50 => Color.FromArgb("#CCCCCC");
    public static Color Gray100 => Color.FromArgb("#E1E1E1");
    public static Color Gray200 => Color.FromArgb("#C8C8C8");
    public static Color Gray300 => Color.FromArgb("#ACACAC");
    public static Color Gray400 => Color.FromArgb("#919191");
    public static Color Gray450 => Color.FromArgb("#797979");
    public static Color Gray500 => Color.FromArgb("#6E6E6E");
    public static Color Gray600 => Color.FromArgb("#404040");
    public static Color Gray900 => Color.FromArgb("#212121");
    public static Color Gray950 => Color.FromArgb("#141414");
    public static Color Yellow100Accent => Color.FromArgb("#F7B548");
    public static Color Yellow200Accent => Color.FromArgb("#FFD590");
    public static Color Yellow300Accent => Color.FromArgb("#FFE5B9");
    public static Color Cyan100Accent => Color.FromArgb("#28C2D1");
    public static Color Cyan200Accent => Color.FromArgb("#7BDDEF");
    public static Color Cyan300Accent => Color.FromArgb("#C3F2F4");
    public static Color Blue100Accent => Color.FromArgb("#3E8EED");
    public static Color Blue200Accent => Color.FromArgb("#72ACF1");
    public static Color Blue300Accent => Color.FromArgb("#A7CBF6");
    public static SolidColorBrush PrimaryBrush => new(Primary);
    public static SolidColorBrush SecondaryBrush => new(Secondary);
    public static SolidColorBrush TertiaryBrush => new(Tertiary);
    public static SolidColorBrush WhiteBrush => new(White);
    public static SolidColorBrush BlackBrush => new(Black);
    public static SolidColorBrush Gray100Brush => new(Gray100);
    public static SolidColorBrush Gray200Brush => new(Gray200);
    public static SolidColorBrush Gray300Brush => new(Gray300);
    public static SolidColorBrush Gray400Brush => new(Gray400);
    public static SolidColorBrush Gray500Brush => new(Gray500);
    public static SolidColorBrush Gray600Brush => new(Gray600);
    public static SolidColorBrush Gray900Brush => new(Gray900);
    public static SolidColorBrush Gray950Brush => new(Gray950);
    //public abstract Style ShellStyle { get; }
    public abstract Style EntryStyle { get; }
    public abstract Style ButtonCreateStyle { get; }
    public abstract Style ButtonCreateDisabledStyle { get; }
    public abstract Style ButtonUpdateStyle { get; }
    public abstract Style ButtonUpdateDisabledStyle { get; }
    public abstract Style ButtonDeleteStyle { get; }
    public abstract Style ButtonDeleteDisabledStyle { get; }
    public abstract Style AltButtonStyle { get; }
    public abstract Style CategoryHeaderStyle { get; }
    public abstract Color PrimaryTextColor { get; }
    public abstract Color SecondaryTextColor { get; }
    public abstract Color PositiveTextColor { get; }
    public abstract Color NegativeTextColor { get; }
    public abstract Color PlaceholderTextColor { get; }
    public abstract Color PageBackgroundColor { get; }
    public abstract Color TransactionCardBackgroundColor { get; }
    public abstract Color TransactionCardPrimaryTextColor { get; }
    public abstract Color TransactionCardSecondaryTextColor { get; }
    public abstract Color TransactionCardTertiaryTextColor { get; }
    public abstract Color DividerColor { get; }

    public static List<string> GroupColors { get; } = new(){ "#9F2846:#9E287D", "#A43D96:#813CA3", "#354BAB:#3582AB", "#40A9C4:#41C4A1", "#FF8811:#FF1A12" };
}