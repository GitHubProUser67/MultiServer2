﻿using BackendProject;
using HttpServerLite;
using System.Diagnostics;
using System.Text;

namespace HTTPSecureServerLite.Extensions
{
    public class PHP
    {
        public static (byte[]?, string[][]) ProcessPHPPage(string FilePath, string phppath, string phpver, string ip, string port, HttpRequest request)
        {
            if (request.Url != null)
            {
                string[][] HeadersLocal = Array.Empty<string[]>();
                byte[]? returndata = null;
                int index = request.Url.Full.IndexOf("?");
                string? queryString = index == -1 ? string.Empty : request.Url.Full[(index + 1)..];
                // Get paths for PHP
                string? documentRootPath = Path.GetDirectoryName(FilePath);
                string? scriptFilePath = Path.GetFullPath(FilePath);
                string? scriptFileName = Path.GetFileName(FilePath);
                string? tempPath = Path.GetTempPath();

                // Extract POST data (if available)
                string? postData = null;
                if (request.Method.ToString() == "POST" && request.DataAsBytes != null)
                    postData = Encoding.UTF8.GetString(request.DataAsBytes);

                Process proc = new();

                proc.StartInfo.FileName = $"{phppath}/{phpver}/php-cgi";

                if (MiscUtils.IsWindows())
                {
                    if (postData == null)
                    {
                        proc.StartInfo.Arguments = $"-q -d \"error_reporting=E_ALL\" -d \"display_errors={HTTPSServerConfiguration.PHPDebugErrors}\" -d \"expose_php=Off\" -d \"include_path='{documentRootPath}'\" " +
                        $"-d \"extension_dir='{$@"{phppath}/{phpver}/ext/"}'\" -d \"extension=php_bz2.dll\" " +
                        $"-d \"extension=php_com_dotnet.dll\" -d \"extension=php_curl.dll\" -d \"extension=php_dba.dll\" -d \"extension=php_dl_test.dll\" -d \"extension=php_enchant.dll\" " +
                        $"-d \"extension=php_exif.dll\" -d \"extension=php_ffi.dll\" -d \"extension=php_fileinfo.dll\" -d \"extension=php_ftp.dll\" -d \"extension=php_gd.dll\" -d \"extension=php_gettext.dll\" " +
                        $"-d \"extension=php_gmp.dll\" -d \"extension=php_imap.dll\" -d \"extension=php_intl.dll\" -d \"extension=php_ldap.dll\" -d \"extension=php_mbstring.dll\" " +
                        $"-d \"extension=php_mysqli.dll\" -d \"extension=php_odbc.dll\" -d \"extension=php_openssl.dll\" " +
                        $"-d \"extension=php_pdo_mysql.dll\" -d \"extension=php_pdo_odbc.dll\" -d \"extension=php_pdo_pgsql.dll\" " +
                        $"-d \"extension=php_pdo_sqlite.dll\" -d \"extension=php_pgsql.dll\" -d \"extension=php_shmop.dll\" -d \"extension=php_snmp.dll\" -d \"extension=php_soap.dll\" " +
                        $"-d \"extension=php_sockets.dll\" -d \"extension=php_sodium.dll\" -d \"extension=php_sqlite3.dll\" -d \"extension=php_sysvshm.dll\" -d \"extension=php_tidy.dll\" " +
                        $"-d \"extension=php_xsl.dll\" -d \"extension=php_zend_test.dll\" -d \"extension=php_zip.dll\" \"{FilePath}\"";
                    }
                    else
                    {
                        proc.StartInfo.Arguments = $"-q -d \"error_reporting=E_ALL\" -d \"display_errors={HTTPSServerConfiguration.PHPDebugErrors}\" -d \"expose_php=Off\" -d \"include_path='{documentRootPath}'\" " +
                        $"-d \"extension_dir='{$@"{phppath}/{phpver}/ext/"}'\" -d \"extension=php_bz2.dll\" " +
                        $"-d \"extension=php_com_dotnet.dll\" -d \"extension=php_curl.dll\" -d \"extension=php_dba.dll\" -d \"extension=php_dl_test.dll\" -d \"extension=php_enchant.dll\" " +
                        $"-d \"extension=php_exif.dll\" -d \"extension=php_ffi.dll\" -d \"extension=php_fileinfo.dll\" -d \"extension=php_ftp.dll\" -d \"extension=php_gd.dll\" -d \"extension=php_gettext.dll\" " +
                        $"-d \"extension=php_gmp.dll\" -d \"extension=php_imap.dll\" -d \"extension=php_intl.dll\" -d \"extension=php_ldap.dll\" -d \"extension=php_mbstring.dll\" " +
                        $"-d \"extension=php_mysqli.dll\" -d \"extension=php_odbc.dll\" -d \"extension=php_openssl.dll\" " +
                        $"-d \"extension=php_pdo_mysql.dll\" -d \"extension=php_pdo_odbc.dll\" -d \"extension=php_pdo_pgsql.dll\" " +
                        $"-d \"extension=php_pdo_sqlite.dll\" -d \"extension=php_pgsql.dll\" -d \"extension=php_shmop.dll\" -d \"extension=php_snmp.dll\" -d \"extension=php_soap.dll\" " +
                        $"-d \"extension=php_sockets.dll\" -d \"extension=php_sodium.dll\" -d \"extension=php_sqlite3.dll\" -d \"extension=php_sysvshm.dll\" -d \"extension=php_tidy.dll\" " +
                        $"-d \"extension=php_xsl.dll\" -d \"extension=php_zend_test.dll\" -d \"extension=php_zip.dll\" -d \"post_data={postData}\" \"{FilePath}\"";
                    }
                }
                else
                {
                    if (postData == null)
                    {
                        proc.StartInfo.Arguments = $"-q -d \"error_reporting=E_ALL\" -d \"display_errors={HTTPSServerConfiguration.PHPDebugErrors}\" -d \"expose_php=Off\" -d \"include_path='{documentRootPath}'\" " +
                        $"-d \"extension_dir='{$@"{phppath}/{phpver}/ext/"}'\" -d \"extension=php_bz2.so\" " +
                        $"-d \"extension=php_com_dotnet.so\" -d \"extension=php_curl.so\" -d \"extension=php_dba.so\" -d \"extension=php_dl_test.so\" -d \"extension=php_enchant.so\" " +
                        $"-d \"extension=php_exif.so\" -d \"extension=php_ffi.so\" -d \"extension=php_fileinfo.so\" -d \"extension=php_ftp.so\" -d \"extension=php_gd.so\" -d \"extension=php_gettext.so\" " +
                        $"-d \"extension=php_gmp.so\" -d \"extension=php_imap.so\" -d \"extension=php_intl.so\" -d \"extension=php_ldap.so\" -d \"extension=php_mbstring.so\" " +
                        $"-d \"extension=php_mysqli.so\" -d \"extension=php_odbc.so\" -d \"extension=php_openssl.so\" " +
                        $"-d \"extension=php_pdo_mysql.so\" -d \"extension=php_pdo_odbc.so\" -d \"extension=php_pdo_pgsql.so\" " +
                        $"-d \"extension=php_pdo_sqlite.so\" -d \"extension=php_pgsql.so\" -d \"extension=php_shmop.so\" -d \"extension=php_snmp.so\" -d \"extension=php_soap.so\" " +
                        $"-d \"extension=php_sockets.so\" -d \"extension=php_sodium.so\" -d \"extension=php_sqlite3.so\" -d \"extension=php_sysvshm.so\" -d \"extension=php_tidy.so\" " +
                        $"-d \"extension=php_xsl.so\" -d \"extension=php_zend_test.so\" -d \"extension=php_zip.so\" \"{FilePath}\"";
                    }
                    else
                    {
                        proc.StartInfo.Arguments = $"-q -d \"error_reporting=E_ALL\" -d \"display_errors={HTTPSServerConfiguration.PHPDebugErrors}\" -d \"expose_php=Off\" -d \"include_path='{documentRootPath}'\" " +
                        $"-d \"extension_dir='{$@"{phppath}/{phpver}/ext/"}'\" -d \"extension=php_bz2.so\" " +
                        $"-d \"extension=php_com_dotnet.so\" -d \"extension=php_curl.so\" -d \"extension=php_dba.so\" -d \"extension=php_dl_test.so\" -d \"extension=php_enchant.so\" " +
                        $"-d \"extension=php_exif.so\" -d \"extension=php_ffi.so\" -d \"extension=php_fileinfo.so\" -d \"extension=php_ftp.so\" -d \"extension=php_gd.so\" -d \"extension=php_gettext.so\" " +
                        $"-d \"extension=php_gmp.so\" -d \"extension=php_imap.so\" -d \"extension=php_intl.so\" -d \"extension=php_ldap.so\" -d \"extension=php_mbstring.so\" " +
                        $"-d \"extension=php_mysqli.so\" -d \"extension=php_odbc.so\" -d \"extension=php_openssl.so\" " +
                        $"-d \"extension=php_pdo_mysql.so\" -d \"extension=php_pdo_odbc.so\" -d \"extension=php_pdo_pgsql.so\" " +
                        $"-d \"extension=php_pdo_sqlite.so\" -d \"extension=php_pgsql.so\" -d \"extension=php_shmop.so\" -d \"extension=php_snmp.so\" -d \"extension=php_soap.so\" " +
                        $"-d \"extension=php_sockets.so\" -d \"extension=php_sodium.so\" -d \"extension=php_sqlite3.so\" -d \"extension=php_sysvshm.so\" -d \"extension=php_tidy.so\" " +
                        $"-d \"extension=php_xsl.so\" -d \"extension=php_zend_test.so\" -d \"extension=php_zip.so\" -d \"post_data={postData}\" \"{FilePath}\"";
                    }
                }

                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = true;

                proc.StartInfo.EnvironmentVariables.Clear();

                // Set environment variables for PHP

                // Set content length for POST data
                if (postData != null)
                    proc.StartInfo.EnvironmentVariables.Add("CONTENT_LENGTH", postData.Length.ToString());

                proc.StartInfo.EnvironmentVariables.Add("GATEWAY_INTERFACE", "CGI/1.1");
                proc.StartInfo.EnvironmentVariables.Add("SERVER_PROTOCOL", "HTTP/1.1");
                proc.StartInfo.EnvironmentVariables.Add("REDIRECT_STATUS", "200");
                proc.StartInfo.EnvironmentVariables.Add("DOCUMENT_ROOT", documentRootPath);
                proc.StartInfo.EnvironmentVariables.Add("SCRIPT_NAME", scriptFileName);
                proc.StartInfo.EnvironmentVariables.Add("SCRIPT_FILENAME", scriptFilePath);
                proc.StartInfo.EnvironmentVariables.Add("QUERY_STRING", queryString);
                proc.StartInfo.EnvironmentVariables.Add("CONTENT_TYPE", request.ContentType);
                proc.StartInfo.EnvironmentVariables.Add("REQUEST_METHOD", request.Method.ToString());
                proc.StartInfo.EnvironmentVariables.Add("USER_AGENT", request.Useragent);
                proc.StartInfo.EnvironmentVariables.Add("SERVER_ADDR", MiscUtils.GetPublicIPAddress());
                proc.StartInfo.EnvironmentVariables.Add("REMOTE_ADDR", ip);
                proc.StartInfo.EnvironmentVariables.Add("REMOTE_PORT", port);
                proc.StartInfo.EnvironmentVariables.Add("REFERER", HTTPUtils.RemoveQueryString(request.Url.Full));
                proc.StartInfo.EnvironmentVariables.Add("REQUEST_URI", $"http://{ip}:{port}{request.Url}");
                proc.StartInfo.EnvironmentVariables.Add("HTTP_COOKIE", request.RetrieveHeaderValue("Cookie"));
                proc.StartInfo.EnvironmentVariables.Add("HTTP_ACCEPT", request.RetrieveHeaderValue("Accept"));
                proc.StartInfo.EnvironmentVariables.Add("HTTP_ACCEPT_CHARSET", request.RetrieveHeaderValue("Accept-Charset"));
                proc.StartInfo.EnvironmentVariables.Add("HTTP_ACCEPT_ENCODING", request.RetrieveHeaderValue("Accept-Encoding"));
                proc.StartInfo.EnvironmentVariables.Add("HTTP_ACCEPT_LANGUAGE", request.RetrieveHeaderValue("Accept-Language"));
                proc.StartInfo.EnvironmentVariables.Add("TMPDIR", tempPath);
                proc.StartInfo.EnvironmentVariables.Add("TEMP", tempPath);

                proc.Start();

                if (postData != null)
                    proc.StandardInput.WriteLine(postData);

                using (MemoryStream memoryStream = new())
                {
                    using (StreamReader reader = proc.StandardOutput)
                    {
                        // Read PHP output to memory stream
                        byte[] buffer = new byte[4096];
                        int bytesRead;
                        while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            memoryStream.Write(buffer, 0, bytesRead);
                        }

                        // If no output, read error stream
                        if (memoryStream.Length == 0)
                        {
                            using (StreamReader errorReader = proc.StandardError)
                            {
                                string errorOutput = errorReader.ReadToEnd();
                                byte[] errorOutputBytes = HTTPUtils.RemoveUnwantedHeaders(Encoding.UTF8.GetBytes(errorOutput));
                                memoryStream.Write(errorOutputBytes, 0, errorOutputBytes.Length);
                            }
                        }

                        // Process Set-Cookie headers
                        List<string> setCookieHeaders = new();
                        foreach (var header in proc.StandardOutput.ReadToEnd().Split('\n'))
                        {
                            if (header.Trim().StartsWith("Set-Cookie:", StringComparison.OrdinalIgnoreCase))
                                setCookieHeaders.Add(header.Trim());
                        }

                        int i = 0;

                        // Add cookies to the HttpListenerResponse
                        foreach (var setCookieHeader in setCookieHeaders)
                        {
                            int colonIndex = setCookieHeader.IndexOf(':');
                            if (colonIndex != -1)
                            {
                                string cookieHeaderValue = setCookieHeader[(colonIndex + 1)..].Trim();
                                string[] cookieParts = cookieHeaderValue.Split(';');

                                foreach (var cookiePart in cookieParts)
                                {
                                    int equalIndex = cookiePart.IndexOf('=');
                                    if (equalIndex != -1)
                                    {
                                        HeadersLocal[i] = new string[] { "Set-Cookie", $"{cookiePart[..equalIndex].Trim()}={cookiePart.Substring(equalIndex + 1).Trim()}; Path=/" };
                                        i++;
                                    }
                                }
                            }
                        }

                        // Get the final byte array
                        returndata = HTTPUtils.RemoveUnwantedHeaders(memoryStream.ToArray());

                        reader.Close();
                    }

                    memoryStream.Flush();
                }

                proc.WaitForExit(); // Wait for the PHP process to complete
                proc.Close();
                proc.Dispose();

                return (returndata, HeadersLocal);
            }

            return (null, Array.Empty<string[]>());
        }
    }
}
