using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;
using static КП_Кафедра.ToastForm;
using OpenXmlWord = DocumentFormat.OpenXml.Wordprocessing;

namespace КП_Кафедра
{
    internal class RepExcel
    {
        public static readonly Dictionary<string, string> columnNames = new Dictionary<string, string>
        {
            //{ "emp_id", "№" },
            { "emp_id", "Номер викладача" }, // можливо прибрати
            { "emp_full_name", "ПІБ" },
            { "emp_position", "Посада" },
            { "emp_hire_date", "Дата прийняття на роботу" },
            { "phone_number", "Телефон" },
            { "email", "Електронна пошта" },
            { "status", "Статус" },
            { "subject_id", "Номер дисципліни" },
            { "subject_name", "Назва дисципліни" },
            { "semester", "Семестр" },
            { "total_hours", "Кількість годин" },
            { "lesson_type_id", "Номер типу заняття" },
            { "hours_taught", "Відпрацьовано годин" },
            { "specialty_id", "Номер спеціальності" },
            //{ "assignment_id", "№" },
            { "assignment_id", "Номер призначення" }, // можливо прибрати
            { "plan_hours", "Кількість годин за планом" },
            { "lesson_type", "Тип заняття" },
            { "research_id", "Номер наукової роботи" },
            { "research_name", "Назва наукової роботи" },
            { "start_date", "Дата початку" },
            { "end_date", "Дата завершення" }
        };

        public List<string> GetSheetNames(string filePath) // можливо прибрати
        {
            var sheetNames = new List<string>();
            if (!File.Exists(filePath)) throw new FileNotFoundException("Файл не знайдено.", filePath);
            using (var workbook = new XLWorkbook(filePath))
            {
                foreach (var sheet in workbook.Worksheets) sheetNames.Add(sheet.Name);
            }

            return sheetNames;
        }

        public void ExportToExcel(DataTable table, string sheetName = "Дані")
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel файли (*.xlsx)|*.xlsx";
                sfd.Title = "Зберегти";
                sfd.FileName = $"{sheetName}.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            var ws = workbook.Worksheets.Add(sheetName);

                            for (int i = 0; i < table.Columns.Count; i++)
                            {
                                string colName = table.Columns[i].ColumnName;
                                ws.Cell(1, i + 1).Value = columnNames.ContainsKey(colName) ? columnNames[colName] : colName;
                            }

                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                for (int j = 0; j < table.Columns.Count; j++)
                                {
                                    object cellValue = table.Rows[i][j];
                                    string cellText = cellValue?.ToString()?.Trim();
                                    string columnName = table.Columns[j].ColumnName.ToLower();

                                    var cell = ws.Cell(i + 2, j + 1);

                                    if (columnName == "phone_number")
                                    {
                                        cell.Style.NumberFormat.Format = "@";
                                        cell.Value = cellText ?? string.Empty;
                                    }
                                    else if (!string.IsNullOrEmpty(cellText) && decimal.TryParse(cellText, out decimal num)) { cell.Value = Convert.ToDouble(num); }
                                    else { cell.Value = cellText ?? string.Empty; }
                                }
                            }


                            var fullRange = ws.Range(1, 1, table.Rows.Count + 1, table.Columns.Count);
                            fullRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            fullRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            fullRange.Style.Font.FontName = "Calibri";
                            fullRange.Style.Font.FontSize = 11;

                            var headerRange = ws.Range(1, 1, 1, table.Columns.Count);
                            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            headerRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            headerRange.Style.Font.Bold = true;
                            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

                            var dataRange = ws.Range(2, 1, table.Rows.Count + 1, table.Columns.Count);
                            dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            dataRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            dataRange.Style.Fill.BackgroundColor = XLColor.White;

                            ws.Columns().AdjustToContents();

                            workbook.SaveAs(sfd.FileName);
                        }

                        Toast.Show("SUCCESS", "Успішно збережено!");
                        LoggerService.LogInfo($"Збереження Excel: {sheetName}");
                    }
                    catch (Exception ex)
                    {
                        Toast.Show("ERROR", "Помилка збереження");
                        LoggerService.LogError($"Помилка збереження Excel ({sheetName}): {ex.Message}");
                    }
                }
            }
        }

        public void ExportDataGridViewToExcel(DataGridView dgv, string fileName) // можливо прибрати
        {
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Лист1");

                    for (int col = 0; col < dgv.Columns.Count; col++)
                    {
                        var headerCell = dgv.Columns[col].HeaderCell;
                        var excelHeaderCell = ws.Cell(1, col + 1);

                        excelHeaderCell.Value = dgv.Columns[col].HeaderText;
                        excelHeaderCell.Style.Font.Bold = true;

                        var backColor = headerCell.Style.BackColor;
                        if (backColor == Color.Empty) backColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
                        if (backColor != Color.Empty) excelHeaderCell.Style.Fill.BackgroundColor = XLColor.FromArgb(backColor.R, backColor.G, backColor.B);

                        var foreColor = headerCell.Style.ForeColor;
                        if (foreColor == Color.Empty) foreColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
                        if (foreColor != Color.Empty) excelHeaderCell.Style.Font.FontColor = XLColor.FromArgb(foreColor.R, foreColor.G, foreColor.B);

                        excelHeaderCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        excelHeaderCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    }

                    for (int row = 0; row < dgv.Rows.Count; row++)
                    {
                        var rowStyleBackColor = dgv.Rows[row].DefaultCellStyle.BackColor;
                        var rowStyleForeColor = dgv.Rows[row].DefaultCellStyle.ForeColor;

                        for (int col = 0; col < dgv.Columns.Count; col++)
                        {
                            var cell = dgv.Rows[row].Cells[col];
                            var excelCell = ws.Cell(row + 2, col + 1);

                            excelCell.Value = cell.Value?.ToString();

                            Color backColor = cell.Style.BackColor != Color.Empty ? cell.Style.BackColor : rowStyleBackColor != Color.Empty ? rowStyleBackColor : dgv.DefaultCellStyle.BackColor;
                            if (backColor != Color.Empty) excelCell.Style.Fill.BackgroundColor = XLColor.FromArgb(backColor.R, backColor.G, backColor.B);

                            Color foreColor = cell.Style.ForeColor != Color.Empty ? cell.Style.ForeColor : rowStyleForeColor != Color.Empty ? rowStyleForeColor : dgv.DefaultCellStyle.ForeColor;
                            if (foreColor != Color.Empty) excelCell.Style.Font.FontColor = XLColor.FromArgb(foreColor.R, foreColor.G, foreColor.B);
                        }
                    }

                    ws.Columns().AdjustToContents();
                    wb.SaveAs(fileName);
                }

                Toast.Show("SUCCESS", "Успішно збережено!");
                LoggerService.LogInfo($"Збереження Excel: {fileName}");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка збереження");
                LoggerService.LogError($"Помилка збереження Excel: {ex.Message}");
            }
        }

        public DataTable ImportFromExcel(string filePath, string sheetName)
        {
            //using (OpenFileDialog ofd = new OpenFileDialog())
            //{
            //    ofd.Filter = "Excel файли (*.xlsx)|*.xlsx";
            //    ofd.Title = "Виберіть Excel файл";

            //    if (ofd.ShowDialog() == DialogResult.OK)
            //    {
            //        try
            //        {
            //            using (XLWorkbook workbook = new XLWorkbook(ofd.FileName))
            //            {
            //                IXLWorksheet ws = workbook.Worksheet(1);
            //                DataTable dt = new DataTable();

            //                foreach (IXLCell cell in ws.Row(1).CellsUsed()) dt.Columns.Add(cell.Value.ToString());

            //                foreach (IXLRow row in ws.RowsUsed().Skip(1))
            //                {
            //                    DataRow dr = dt.NewRow();
            //                    int i = 0;
            //                    foreach (IXLCell cell in row.Cells(1, dt.Columns.Count)) dr[i++] = cell.Value.ToString();
            //                    dt.Rows.Add(dr);
            //                }

            //                Toast.Show("SUCCESS", "Успішно завантажено!");
            //                LoggerService.LogInfo($"Завантаження Excel: {dt.Rows.Count} рядків");
            //                return dt;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Toast.Show("ERROR", "Помилка завантаження");
            //            LoggerService.LogError($"Помилка завантаження Excel: {ex.Message}");
            //        }
            //    }

            //    return null;
            //}

            if (!File.Exists(filePath)) throw new FileNotFoundException("Файл не знайдено.", filePath); // можливо прибрати
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    IXLWorksheet ws = workbook.Worksheet(sheetName);
                    DataTable dt = new DataTable();
                    foreach (IXLCell cell in ws.Row(1).CellsUsed()) dt.Columns.Add(cell.Value.ToString());
                    foreach (IXLRow row in ws.RowsUsed().Skip(1))
                    {
                        DataRow dr = dt.NewRow();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells(1, dt.Columns.Count)) dr[i++] = cell.Value.ToString() ?? string.Empty;
                        dt.Rows.Add(dr);
                    }
                    LoggerService.LogInfo($"Завантаження Excel: {dt.Rows.Count} рядків");
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка завантаження");
                LoggerService.LogError($"Помилка завантаження Excel ({sheetName}): {ex.Message}");
                return null;
            }
        }

        public void OpenExcelFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel файли (*.xlsx)|*.xlsx";
                ofd.Title = "Виберіть файл для відкриття";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(ofd.FileName) { UseShellExecute = true });
                        LoggerService.LogInfo($"Відкрито Excel-файл {ofd.FileName}");
                    }
                    catch (Exception ex)
                    {
                        Toast.Show("ERROR", "Не вдалося відкрити");
                        LoggerService.LogError($"Помилка відкриття Excel: {ex.Message}");
                    }
                }
            }
        }
    }

    internal class RepWord
    {
        public void ExportToWord(DataTable table, string fileName)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Word файли (*.docx)|*.docx";
                sfd.Title = "Зберегти звіт у Word";
                sfd.FileName = $"{fileName}.docx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(
                            sfd.FileName, WordprocessingDocumentType.Document))
                        {
                            MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                            mainPart.Document = new OpenXmlWord.Document(new OpenXmlWord.Body());
                            OpenXmlWord.Body body = mainPart.Document.Body;

                            OpenXmlWord.Table wordTable = new OpenXmlWord.Table();
                            OpenXmlWord.TableProperties tblProps = new OpenXmlWord.TableProperties(
                                new OpenXmlWord.TableBorders(
                                    new OpenXmlWord.TopBorder { Val = new EnumValue<OpenXmlWord.BorderValues>(OpenXmlWord.BorderValues.Single), Size = 6 },
                                    new OpenXmlWord.BottomBorder { Val = new EnumValue<OpenXmlWord.BorderValues>(OpenXmlWord.BorderValues.Single), Size = 6 },
                                    new OpenXmlWord.LeftBorder { Val = new EnumValue<OpenXmlWord.BorderValues>(OpenXmlWord.BorderValues.Single), Size = 6 },
                                    new OpenXmlWord.RightBorder { Val = new EnumValue<OpenXmlWord.BorderValues>(OpenXmlWord.BorderValues.Single), Size = 6 },
                                    new OpenXmlWord.InsideHorizontalBorder { Val = new EnumValue<OpenXmlWord.BorderValues>(OpenXmlWord.BorderValues.Single), Size = 6 },
                                    new OpenXmlWord.InsideVerticalBorder { Val = new EnumValue<OpenXmlWord.BorderValues>(OpenXmlWord.BorderValues.Single), Size = 6 }
                                )
                            );
                            wordTable.AppendChild(tblProps);

                            OpenXmlWord.TableRow headerRow = new OpenXmlWord.TableRow();
                            foreach (DataColumn column in table.Columns)
                            {
                                string headerText = RepExcel.columnNames.ContainsKey(column.ColumnName) ? RepExcel.columnNames[column.ColumnName] : column.ColumnName;

                                var run = new OpenXmlWord.Run(
                                    new OpenXmlWord.RunProperties(
                                        new OpenXmlWord.RunFonts
                                        {
                                            Ascii = "Times New Roman",
                                            HighAnsi = "Times New Roman",
                                            EastAsia = "Times New Roman",
                                            ComplexScript = "Times New Roman"
                                        },
                                        new OpenXmlWord.Bold()
                                    ),
                                    new OpenXmlWord.Text(headerText)
                                );

                                var paragraph = new OpenXmlWord.Paragraph(run)
                                {
                                    ParagraphProperties = new OpenXmlWord.ParagraphProperties(
                                        new OpenXmlWord.SpacingBetweenLines { After = "0", Before = "0" },
                                        new OpenXmlWord.Justification { Val = OpenXmlWord.JustificationValues.Center }
                                    )
                                };

                                var cellProps = new OpenXmlWord.TableCellProperties(
                                    new OpenXmlWord.TableCellWidth { Type = OpenXmlWord.TableWidthUnitValues.Auto },
                                    new OpenXmlWord.Shading { Fill = "DDDDDD" },
                                    new OpenXmlWord.TopMargin { Width = "50", Type = OpenXmlWord.TableWidthUnitValues.Dxa },
                                    new OpenXmlWord.BottomMargin { Width = "50", Type = OpenXmlWord.TableWidthUnitValues.Dxa },
                                    new OpenXmlWord.LeftMargin { Width = "50", Type = OpenXmlWord.TableWidthUnitValues.Dxa },
                                    new OpenXmlWord.RightMargin { Width = "50", Type = OpenXmlWord.TableWidthUnitValues.Dxa }
                                );

                                var headerCell = new OpenXmlWord.TableCell(paragraph);
                                headerCell.Append(cellProps);
                                headerRow.Append(headerCell);
                            }
                            wordTable.Append(headerRow);

                            foreach (DataRow row in table.Rows)
                            {
                                OpenXmlWord.TableRow dataRow = new OpenXmlWord.TableRow();
                                foreach (var cellValue in row.ItemArray)
                                {
                                    string text = cellValue?.ToString()?.Trim() ?? "";

                                    var run = new OpenXmlWord.Run(
                                        new OpenXmlWord.RunProperties(
                                            new OpenXmlWord.RunFonts
                                            {
                                                Ascii = "Times New Roman",
                                                HighAnsi = "Times New Roman",
                                                EastAsia = "Times New Roman",
                                                ComplexScript = "Times New Roman"
                                            }
                                        ),
                                        new OpenXmlWord.Text(text)
                                    );

                                    var paragraph = new OpenXmlWord.Paragraph(run)
                                    {
                                        ParagraphProperties = new OpenXmlWord.ParagraphProperties(
                                            new OpenXmlWord.SpacingBetweenLines { After = "0", Before = "0" },
                                            new OpenXmlWord.Justification { Val = OpenXmlWord.JustificationValues.Left }
                                        )
                                    };

                                    var cell = new OpenXmlWord.TableCell(paragraph);
                                    cell.Append(new OpenXmlWord.TableCellProperties(
                                        new OpenXmlWord.TableCellWidth { Type = OpenXmlWord.TableWidthUnitValues.Auto },
                                        new OpenXmlWord.TopMargin { Width = "50", Type = OpenXmlWord.TableWidthUnitValues.Dxa },
                                        new OpenXmlWord.BottomMargin { Width = "50", Type = OpenXmlWord.TableWidthUnitValues.Dxa },
                                        new OpenXmlWord.LeftMargin { Width = "50", Type = OpenXmlWord.TableWidthUnitValues.Dxa },
                                        new OpenXmlWord.RightMargin { Width = "50", Type = OpenXmlWord.TableWidthUnitValues.Dxa }
                                    ));
                                    dataRow.Append(cell);
                                }
                                wordTable.Append(dataRow);
                            }

                            body.Append(wordTable);
                            mainPart.Document.Save();
                        }

                        Toast.Show("SUCCESS", "Звіт успішно збережено!");
                        LoggerService.LogInfo($"Збереження у Word завершено ({table.Rows.Count} рядків, файл: {fileName}.docx)");
                    }
                    catch (Exception ex)
                    {
                        Toast.Show("ERROR", "Помилка збереження Word-файлу.");
                        LoggerService.LogError($"Помилка збереження у Word: {ex.Message}");
                    }
                }
            }
        }

        public DataTable ImportFromWord()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Word файли (*.docx)|*.docx";
                ofd.Title = "Виберіть Word файл для завантаження";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DataTable dt = new DataTable();

                        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(ofd.FileName, false))
                        {
                            var body = wordDoc.MainDocumentPart.Document.Body;
                            var table = body.Elements<OpenXmlWord.Table>().FirstOrDefault();

                            if (table == null)
                            {
                                Toast.Show("WARNING", "У вибраному документі немає таблиць.");
                                LoggerService.LogError($"Завантаження Word скасовано — таблиця відсутня ({ofd.FileName}).");
                                return null;
                            }

                            var rows = table.Elements<OpenXmlWord.TableRow>().ToList();
                            if (rows.Count == 0)
                            {
                                Toast.Show("WARNING", "Таблиця порожня.");
                                LoggerService.LogError($"Завантаження Word скасовано — таблиця порожня ({ofd.FileName}).");
                                return null;
                            }

                            var headerCells = rows[0].Elements<OpenXmlWord.TableCell>().ToList();
                            foreach (var cell in headerCells)
                            {
                                string headerText = cell.InnerText.Trim();
                                dt.Columns.Add(headerText);
                            }

                            for (int i = 1; i < rows.Count; i++)
                            {
                                var dataCells = rows[i].Elements<OpenXmlWord.TableCell>().ToList();
                                DataRow dr = dt.NewRow();
                                for (int j = 0; j < dt.Columns.Count && j < dataCells.Count; j++)
                                {
                                    dr[j] = dataCells[j].InnerText.Trim();
                                }
                                dt.Rows.Add(dr);
                            }
                        }

                        Toast.Show("SUCCESS", "Успішно завантажено!");
                        LoggerService.LogInfo($"Завантаження Word завершено ({dt.Rows.Count} рядків, файл: {ofd.FileName}).");

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        Toast.Show("ERROR", "Помилка завантаження Word-файлу.");
                        LoggerService.LogError($"Помилка завантаження з Word: {ex.Message}");
                        return null;
                    }
                }

                return null;
            }
        }

        public void OpenWordFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Word файли (*.docx)|*.docx";
                ofd.Title = "Виберіть файл для відкриття";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(ofd.FileName) { UseShellExecute = true });
                        LoggerService.LogInfo($"Відкрито Word-файл: {ofd.FileName}");
                    }
                    catch (Exception ex)
                    {
                        Toast.Show("ERROR", "Не вдалося відкрити Word.");
                        LoggerService.LogError($"Помилка відкриття Word: {ex.Message}");
                    }
                }
            }
        }
    }
}
