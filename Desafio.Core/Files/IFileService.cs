namespace Desafio.Api.Services.Files
{
    public interface IFileService
    {
        Task<string>  UploadAsync(string formFile);
        bool Remove(string nomeArquivo); 
    }
}
