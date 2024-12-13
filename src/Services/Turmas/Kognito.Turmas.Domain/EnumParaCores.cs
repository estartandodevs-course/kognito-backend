using System.Text.Json.Serialization;

namespace Kognito.Turmas.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Cor
{
    portugues,
    matematica,
    fisica,
    quimica,
    biologia,
    historia,
    geografia
}