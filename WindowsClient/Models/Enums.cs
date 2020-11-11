using IBrewery.Client.Converter;
using System.ComponentModel;

namespace IBrewery.Client.Models
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum IngredientTyp
    {
        [Description("Kilogramm")]
        Kilogram,
        [Description("Liter")]
        Liter
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum PhaseTyp
    {
        [Description("aufheizen")]
        HeatUp,
        [Description("abkühlen")]
        HeatDown,     
        [Description("halten")]
        KeepHeat
    }
}
