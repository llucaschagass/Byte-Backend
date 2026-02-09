using System.Text.Json.Serialization;

namespace Byte_Backend.Entidades;

public abstract class EntidadeBase
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime InseridoEm { get; set; } = DateTime.Now;
}