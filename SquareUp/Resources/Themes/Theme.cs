namespace SquareUp.Resources.Themes;
public class Theme
{
    public static ResourceDictionary Resources => Application.Current.Resources;

    //public static Style ShellStyle => Resources[nameof(ThemeBase.ShellStyle)] as Style;
    public static Style EntryStyle => Resources[nameof(ThemeBase.EntryStyle)] as Style;
    public static Style ButtonCreateStyle => Resources[nameof(ThemeBase.ButtonCreateStyle)] as Style;
    public static Style ButtonCreateDisabledStyle => Resources[nameof(ThemeBase.ButtonCreateDisabledStyle)] as Style;
    public static Style ButtonUpdateStyle => Resources[nameof(ThemeBase.ButtonUpdateStyle)] as Style;
    public static Style ButtonUpdateDisabledStyle => Resources[nameof(ThemeBase.ButtonUpdateDisabledStyle)] as Style;
    public static Style ButtonDeleteStyle => Resources[nameof(ThemeBase.ButtonDeleteStyle)] as Style;
    public static Style ButtonDeleteDisabledStyle => Resources[nameof(ThemeBase.ButtonDeleteDisabledStyle)] as Style;
    public static Color PrimaryTextColor => Resources[nameof(ThemeBase.PrimaryTextColor)] as Color;
    public static Color SecondaryTextColor => Resources[nameof(ThemeBase.SecondaryTextColor)] as Color;
    public static Color PageBackgroundColor => Resources[nameof(ThemeBase.PageBackgroundColor)] as Color;
    public static Color TransactionCardBackgroundColor => Resources[nameof(ThemeBase.TransactionCardBackgroundColor)] as Color;
    public static Color TransactionCardPrimaryTextColor => Resources[nameof(ThemeBase.TransactionCardPrimaryTextColor)] as Color;
    public static Color TransactionCardSecondaryTextColor => Resources[nameof(ThemeBase.TransactionCardSecondaryTextColor)] as Color;
    public static Color TransactionCardTertiaryTextColor => Resources[nameof(ThemeBase.TransactionCardTertiaryTextColor)] as Color;
}