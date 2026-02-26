using System.Text.Json.Serialization;

namespace Byte_Backend.Entidades;

[JsonConverter(typeof(JsonStringEnumConverter<PermissaoPrincipal>))]
public enum PermissaoPrincipal
{
    G, // Garçom
    C, // Cozinha
    P  // Gerência
}

public class UsuarioPermissao : EntidadeBase
{
    public int UsuarioId { get; set; }
    public virtual Usuario? Usuario { get; set; }

    public bool Garcom { get; set; }
    public bool Cozinha { get; set; }
    public bool Gerencia { get; set; }

    public PermissaoPrincipal Principal { get; set; }
}
