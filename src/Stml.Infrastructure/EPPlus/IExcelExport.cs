using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Stml.Infrastructure.EPPlus
{
    public interface IExcelExport
    {
        MemoryStream ExportStream<T>(IEnumerable<T> datas) where T : IExportSheet;
    }
}
