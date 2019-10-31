namespace RealEstate.Infrastructure
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class FileExtension
    {
        /// <summary>
        /// use local system to check is file existed in storage
        /// </summary>
        public static bool IsExisted(this string storagePath, string fileName)
            => File.Exists($"{storagePath}/{fileName}");

        /// <summary>
        /// use for local storage
        /// <system.web>
        ///     <httpRuntime targetFramework = "4.7.2" maxRequestLength="2097151"/>
        /// </system.web>
        /// </summary>
        public static string WriteBase64ToFile(this string storagePath, string fileName, string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            string finalFileName = UniqueFileName(storagePath, fileName, (storage, file) => storage.IsExisted(file));
            using (FileStream imageFiles = new FileStream($"{storagePath}{finalFileName}", FileMode.Create))
            {
                imageFiles.Write(bytes, 0, bytes.Length);
                imageFiles.Flush();
            }

            return finalFileName;
        }

        /// <summary>
        /// use for local storage
        /// </summary>
        public static string UniqueFileName(this string storagePath, string fileName, Func<string, string, bool> isExisted)
        {
            int counter = 0;
            int fileExtPos = fileName.LastIndexOf(".", StringComparison.Ordinal);

            string finalName = null;
            string fileNameWithoutExtension = fileName.Substring(0, fileExtPos);
            string ext = fileName.Substring(fileExtPos, fileName.Length - fileExtPos);
            do
            {
                finalName = fileNameWithoutExtension;

                finalName = counter++ == 0 ?
                    $"{finalName.RewriteUrl(1000, true, true, '-')}{ext}" :
                    $"{finalName.RewriteUrl(1000, true, true, '-')}-{counter - 1}{ext}";
            } while (isExisted.Invoke(storagePath, finalName));
            return $"{finalName}";
        }

        /// <summary>
        /// use another system(api) to check is file existed in storage
        /// </summary>
        public static async Task<string> UniqueFileName(this string fileName, Func<string, Task<bool>> isExisted)
        {
            int counter = 0;
            int fileExtPos = fileName.LastIndexOf(".", StringComparison.Ordinal);

            string finalName = null;
            string fileNameWithoutExtension = fileName.Substring(0, fileExtPos);
            string ext = fileName.Substring(fileExtPos, fileName.Length - fileExtPos);
            do
            {
                finalName = fileNameWithoutExtension;

                finalName = counter++ == 0 ?
                    $"{finalName.RewriteUrl(1000, true, true, '-')}{ext}" :
                    $"{finalName.RewriteUrl(1000, true, true, '-')}-{counter - 1}{ext}";
            } while (await isExisted.Invoke(finalName));
            return $"{finalName}";
        }
    }
}