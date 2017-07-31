using System;
using System.Net;
using System.IO;


public static async Task<Stream> Run(Stream outBlob,String input,TraceWriter log)
{
    WebClient webClient = new WebClient();
    var file=await webClient.DownloadDataTaskAsync(new Uri("http://files.data.gouv.fr/sirene/sirene_2017207_E_Q.zip"));
    await outBlob.WriteAsync(file,0,file.Length);
    return outBlob;
}