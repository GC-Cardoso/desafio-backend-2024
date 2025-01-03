
using Desafio.Core;
using System.Buffers.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Desafio.Api.Services.Files
{
    public class FileService : IFileService
    {
        public async Task<string>  UploadAsync(string base64)
        {
            var EBase64 = Base64.IsValid(base64);

            if (!EBase64)
                return "Imagem Invalida";
            
            var nomeArquivo = $"{Guid.NewGuid()}.jpg";
            var bytes = Convert.FromBase64String(base64);

            var caminhoArquivo = $"{Configuration.DEFAULT_IMAGE_PATH}/{nomeArquivo}";

            var diretorio = Directory.Exists(Configuration.DEFAULT_IMAGE_PATH);
            if (!diretorio) 
                Directory.CreateDirectory(Configuration.DEFAULT_IMAGE_PATH);

            await System.IO.File.WriteAllBytesAsync(caminhoArquivo, bytes);

            return nomeArquivo;
        }

        public bool Remove(string nomeArquivo)
        {
            try
            {
                if (Directory.Exists(Configuration.DEFAULT_IMAGE_PATH))
                    File.Delete($"{Configuration.DEFAULT_IMAGE_PATH}/{nomeArquivo}");
                else
                    return false;

                return true;

            }
            catch (Exception)
            {

                return false;
            }


        }
    }
}
