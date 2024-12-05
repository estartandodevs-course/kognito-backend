using System.Text.Json.Serialization;

namespace Kognito.Turmas.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Icones
{
    primeiro,
    segundo,
    terceiro
}