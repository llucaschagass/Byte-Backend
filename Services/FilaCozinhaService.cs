using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class FilaCozinhaService
{
    private readonly IFilaCozinhaRepository filaCozinhaRepository;
    private readonly IItemComandaRepository itemComandaRepository;

    public FilaCozinhaService(
        IFilaCozinhaRepository filaCozinhaRepository,
        IItemComandaRepository itemComandaRepository)
    {
        this.filaCozinhaRepository = filaCozinhaRepository;
        this.itemComandaRepository = itemComandaRepository;
    }

    public async Task<IEnumerable<FilaCozinha>> GetAllFilasCozinha()
    {
        return await filaCozinhaRepository.GetAllFilasCozinha();
    }

    public async Task<FilaCozinha?> GetFilaCozinhaById(int id)
    {
        return await filaCozinhaRepository.GetFilaCozinhaById(id);
    }

    public async Task<IEnumerable<FilaCozinha>> GetFilasCozinhaByStatus(string status)
    {
        return await filaCozinhaRepository.GetFilasCozinhaByStatus(status);
    }

    public async Task<IEnumerable<FilaCozinha>> GetFilasCozinhaByItemComandaId(int itemComandaId)
    {
        return await filaCozinhaRepository.GetFilasCozinhaByItemComandaId(itemComandaId);
    }

    public async Task<(bool Success, string Message)> CreateFilaCozinha(FilaCozinha filaCozinha)
    {
        // Verificar se o item da comanda existe
        var itemComanda = await itemComandaRepository.GetItemComandaById(filaCozinha.ItemComandaId);
        if (itemComanda == null)
        {
            return (false, "Item da comanda não encontrado.");
        }

        // Validar status
        if (filaCozinha.StatusPreparo != "Pendente" && 
            filaCozinha.StatusPreparo != "Preparando" && 
            filaCozinha.StatusPreparo != "Pronto")
        {
            return (false, "Status inválido. Use 'Pendente', 'Preparando' ou 'Pronto'.");
        }

        await filaCozinhaRepository.CreateFilaCozinha(filaCozinha);
        return (true, "Item adicionado à fila da cozinha com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdateFilaCozinha(int id, FilaCozinha filaCozinha)
    {
        var filaCozinhaExistente = await filaCozinhaRepository.GetFilaCozinhaById(id);
        if (filaCozinhaExistente == null)
        {
            return (false, "Item da fila da cozinha não encontrado.");
        }

        // Verificar se o item da comanda existe
        var itemComanda = await itemComandaRepository.GetItemComandaById(filaCozinha.ItemComandaId);
        if (itemComanda == null)
        {
            return (false, "Item da comanda não encontrado.");
        }

        // Validar status
        if (filaCozinha.StatusPreparo != "Pendente" && 
            filaCozinha.StatusPreparo != "Preparando" && 
            filaCozinha.StatusPreparo != "Pronto")
        {
            return (false, "Status inválido. Use 'Pendente', 'Preparando' ou 'Pronto'.");
        }

        filaCozinhaExistente.ItemComandaId = filaCozinha.ItemComandaId;
        filaCozinhaExistente.StatusPreparo = filaCozinha.StatusPreparo;

        await filaCozinhaRepository.UpdateFilaCozinha(filaCozinhaExistente);
        return (true, "Item da fila da cozinha atualizado com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteFilaCozinha(int id)
    {
        var filaCozinha = await filaCozinhaRepository.GetFilaCozinhaById(id);
        if (filaCozinha == null)
        {
            return (false, "Item da fila da cozinha não encontrado.");
        }

        await filaCozinhaRepository.DeleteFilaCozinha(id);
        return (true, "Item da fila da cozinha excluído com sucesso.");
    }

    public async Task<(bool Success, string Message)> AtualizarStatusPreparo(int id, string novoStatus)
    {
        var filaCozinha = await filaCozinhaRepository.GetFilaCozinhaById(id);
        if (filaCozinha == null)
        {
            return (false, "Item da fila da cozinha não encontrado.");
        }

        // Validar status
        if (novoStatus != "Pendente" && 
            novoStatus != "Preparando" && 
            novoStatus != "Pronto")
        {
            return (false, "Status inválido. Use 'Pendente', 'Preparando' ou 'Pronto'.");
        }

        filaCozinha.StatusPreparo = novoStatus;
        await filaCozinhaRepository.UpdateFilaCozinha(filaCozinha);
        
        return (true, $"Status atualizado para '{novoStatus}' com sucesso.");
    }
}
