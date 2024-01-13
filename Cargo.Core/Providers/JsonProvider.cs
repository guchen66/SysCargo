using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.Providers
{
    public class JsonProvider
    {
        /// <summary>
        /// 获取根目录下的所有json文件
        /// </summary>
        /// <returns></returns>
        public static async Task<string[]> GetJsonFileNamesAsync()
        {
            string rootDirectory = DirectoryProvider.SelectRootDirectory();

            string[] jsonFilePaths = await Task.Run(() => Directory.GetFiles(rootDirectory, "*.json"));

            return jsonFilePaths.Select(Path.GetFileName).ToArray()!;           //只列出路径的文件名
        }

        /// <summary>
        /// 条件宽松
        /// 获取根目录下包含Name的所有json文件
        /// </summary>
        public static async Task<List<string>> GetJsonFilesContainNameAsync(string searchContent)
        {
            var allJsonFilePaths = await GetJsonFileNamesAsync();
            var allJsons = allJsonFilePaths.Where(s => s.Contains(searchContent)).ToList();
            return allJsons;
        }

        /// <summary>
        /// 条件不宽松
        /// 获取根目录下指定Name的唯一json文件
        /// </summary>
        public static async Task<string> GetJsonFileByNameAsync(string name)
        {
            if (name is null)
            {
                return await Task.FromResult<string>(null);
            }

            var allJsonFilePaths = await GetJsonFileNamesAsync();
            var matchingJson = allJsonFilePaths.FirstOrDefault(s => Path.GetFileNameWithoutExtension(s) == name);
            return matchingJson;
        }


        /// <summary>
        /// 获取根目录下的指定json文件并打开查看内容
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static async Task<string> GetDecisionJsonAsync(string resourceName, string key)
        {
            var path = DirectoryProvider.SelectDirectoryByName(resourceName);
            using (var stream = File.OpenText(path))
            {
                if (stream == null) return null;

                JsonTextReader reader = new JsonTextReader(stream);
                JObject jsonObject = (JObject)await JToken.ReadFromAsync(reader);

                string json = jsonObject[key]!.ToString();
                return json;
            }
        }


    }
}
