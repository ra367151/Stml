using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Infrastructure.EPPlus
{
    public class ExcelManager : IExcelExport
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ExcelManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public MemoryStream ExportStream<T>(IEnumerable<T> datas) where T : IExportSheet
        {
            MemoryStream ms = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage())
            {
                var sheetAttribute = (SheetAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(SheetAttribute));
                ExcelWorksheet sheet = sheetAttribute == null ? package.Workbook.Worksheets.Add("sheet1")
                    : package.Workbook.Worksheets.Add(sheetAttribute.Name);
                var properties = typeof(T).GetProperties();

                var rows = datas.Count();
                var columns = properties.Count();

                if (columns <= 0)
                    throw new InvalidOperationException($"{typeof(T)} has no properties");

                // wirte headers
                for (var c = 1; c <= columns; c++)
                {
                    var prop = properties[c - 1];
                    var displayAttribute = (DisplayAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayAttribute));
                    sheet.Cells[1, c].Value = displayAttribute?.Name ?? prop.Name;
                }

                // write contents
                for (int c = 1; c <= columns; c++)
                {
                    var prop = properties[c - 1];

                    Parallel.For(1, rows + 1, r =>
                    {
                        var imageAttribute = (ImageAttribute)Attribute.GetCustomAttribute(prop, typeof(ImageAttribute));
                        if (imageAttribute == null)
                            sheet.Cells[r + 1, c].Value = typeof(T).GetProperty(prop.Name).GetValue(datas.ElementAt(r - 1), null);
                        else
                            AddImage(sheet, r + 1, c, (string)typeof(T).GetProperty(prop.Name).GetValue(datas.ElementAt(r - 1), null));
                    });
                }
                package.SaveAs(ms);
                ms.Seek(0, SeekOrigin.Begin);
            }
            return ms;
        }

        private void AddImage(ExcelWorksheet ws, int rowIndex, int columnIndex, string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var client = _httpClientFactory.CreateClient();
                try
                {
                    var stream = client.GetStreamAsync(imageUrl).Result;
                    var image = Image.FromStream(stream);
                    ExcelPicture pic = ws.Drawings.AddPicture("Sample1", image);
                    pic.SetPosition(rowIndex - 1, 0, columnIndex - 1, 0);
                    pic.SetSize(40, 40);
                    ws.Row(rowIndex).Height = 30; // = 40px
                    ws.Row(rowIndex).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                }
                catch { }
            }
        }
    }
}
