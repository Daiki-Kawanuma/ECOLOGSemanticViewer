using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECOLOGSemanticViewer.Utils
{
    static class LogWriter
    {
        private static ReaderWriterLock readWriteLock = new ReaderWriterLock();

        /// <summary>
        /// 引数の文字列をログファイルへ出力する。
        /// </summary>
        /// <param name="me">書き込む内容を含む文字列</param>
        public static void Write(string log)
        {
            try
            {
                readWriteLock.AcquireWriterLock(Timeout.Infinite);

                if (!Directory.Exists("LogFile"))
                {
                    Directory.CreateDirectory("LogFile");
                }

                if (!File.Exists(@"LogFile\ErrorLog.txt"))
                {
                    File.Create(@"LogFile\ErrorLog.txt");
                }

                using (StreamWriter writer = new StreamWriter(@"LogFile\ErrorLog.txt", true))
                {
                    writer.Write("---------------------------------------------------------------\n");
                    writer.Write(DateTime.Now + "\n");
                    writer.Write(log + "\n");
                }
            }
            finally
            {
                readWriteLock.ReleaseWriterLock();
            }

            return;
        }
    }
}
