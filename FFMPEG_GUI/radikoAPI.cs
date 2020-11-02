using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Drawing.Printing;
using System.Diagnostics;

namespace FFMPEG_GUI
{
  class radikoAPI
  {
    string authtoken;
    public radikoAPI()
    {}

    /// <summary>
    /// 
    /// </summary>
    public async Task<string> Tuning(string station)
    {
      string m3u8filename=null;

      try
      {
        string authkey = "bcd151073c03b352e1ef2fd66c32209da9ca0afa";
        //Auth1 適当なヘッダーを用意してauth1に投げる処理
        var httpClient = new HttpClient();

        var request = new HttpRequestMessage(new HttpMethod("GET"), "https://radiko.jp/v2/api/auth1");

        request.Headers.TryAddWithoutValidation("User-Agent", "curl/7.52.1");
        request.Headers.TryAddWithoutValidation("Accept", "*/*");
        request.Headers.TryAddWithoutValidation("X-Radiko-User", "dummy_user");
        request.Headers.TryAddWithoutValidation("X-Radiko-App", "pc_html5");
        request.Headers.TryAddWithoutValidation("X-Radiko-App-Version", "0.0.1");
        request.Headers.TryAddWithoutValidation("X-Radiko-Device", "pc");

        var response = await httpClient.SendAsync(request);
        var h = response.Headers;
        
        //RADIKOから送られてきた情報から必要なものを取得
        authtoken = string.Join("", h.GetValues("X-Radiko-AuthToken"));
        var len = int.Parse(string.Join("", h.GetValues("X-Radiko-KeyLength")));
        var offset = int.Parse(string.Join("", h.GetValues("X-Radiko-KeyOffset")));

        //m3u8ファイルの取得に必要なpatialkeyの解読
        var partialkey = Convert.ToBase64String(Encoding.UTF8.GetBytes(authkey.Substring(offset, len)));

        //Auth2 auth1から得たauthtokenと生成したpartialkeyをauth2に投げる。
        request = new HttpRequestMessage(new HttpMethod("GET"), "https://radiko.jp/v2/api/auth2");
        request.Headers.TryAddWithoutValidation("User-Agent", "curl/7.52.1");
        request.Headers.TryAddWithoutValidation("Accept", "*/*");
        request.Headers.TryAddWithoutValidation("X-Radiko-User", "dummy_user");
        request.Headers.TryAddWithoutValidation("X-RADIKO-AUTHTOKEN", authtoken);
        request.Headers.TryAddWithoutValidation("X-radiko-partialkey", partialkey);
        request.Headers.TryAddWithoutValidation("X-Radiko-Device", "pc");

        response = await httpClient.SendAsync(request);
        //await Task.Delay(1000);

        //m3u8ファイルのURL取得
        request = new HttpRequestMessage(new HttpMethod("GET"), 
          string.Format("http://c-radiko.smartstream.ne.jp/{0}/_definst_/simul-stream.stream/playlist.m3u8",station));

        request.Headers.TryAddWithoutValidation("X-RADIKO-AUTHTOKEN", authtoken);

        response = await httpClient.SendAsync(request);

        //失敗したときはhttp://f-radikoで再度試す
        if (response.StatusCode != HttpStatusCode.OK)
        {
          request = new HttpRequestMessage(new HttpMethod("GET"),
          string.Format("http://f-radiko.smartstream.ne.jp/{0}/_definst_/simul-stream.stream/playlist.m3u8", station));

          request.Headers.TryAddWithoutValidation("X-RADIKO-AUTHTOKEN", authtoken);

          response = await httpClient.SendAsync(request);
        }
        //帰ってきた情報の取得
        var content = await response.Content.ReadAsStringAsync(); //right!

        //最後尾の要素を取り出す
        var wordlist = content.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
        foreach(var word in wordlist.Reverse())
        {
          if (word != "")
          {
            m3u8filename = word;
            break;
          }
        }

        Debug.WriteLine("Content:" + content);
        Debug.WriteLine("authtoken:" + authtoken);
        Debug.WriteLine("partialkey:" + partialkey);
        Debug.WriteLine("m3u8filename:" + m3u8filename);

      }
      catch (HttpRequestException e)
      {
        // 404エラーや、名前解決失敗など
        // InnerExceptionも含めて、再帰的に例外メッセージを表示する
        Exception ex = e;
        while (ex != null)
        {
          Console.WriteLine("例外メッセージ: {0} ", ex.Message);
          ex = ex.InnerException;
        }
      }
      catch (TaskCanceledException e)
      {
        // タスクがキャンセルされたとき（一般的にタイムアウト）
        Console.WriteLine("\nタイムアウト!");
        Console.WriteLine("例外メッセージ: {0} ", e.Message);
      }
      return m3u8filename;
    }
  }
}
