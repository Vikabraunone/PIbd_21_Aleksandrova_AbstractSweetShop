﻿using AbstractSweetShopBusinessLogic.HelperModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;

namespace AbstractSweetShopBusinessLogic.BusinessLogics
{
    static class SaveToWord
    {
        /// <summary>
        /// Создание документа
        /// </summary>
        /// <param name="info"></param>
        public static void CreateDoc(WordInfo info)
        {
            using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<string> { info.Title },
                    TextProperties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                if (info.Products != null)
                {
                    foreach (var product in info.Products)
                        docBody.AppendChild(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<string> { product.ProductName, product.Price.ToString() },
                            TextProperties = new WordParagraphProperties
                            {
                                Size = "24",
                                JustificationValues = JustificationValues.Both
                            }
                        }));
                }
                else if (info.StoreHouses != null)
                {
                    Table table = new Table();
                    table.AppendChild(CreateTableProperties());
                    foreach (var storeHouse in info.StoreHouses)
                    {
                        var tr = new TableRow();
                        var tc = new TableCell();
                        tc.Append(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<string> { storeHouse.StoreHouseName },
                            ParagraphIsCell = true,
                            TextProperties = new WordParagraphProperties
                            {
                                Bold = false,
                                Size = "24",
                                JustificationValues = JustificationValues.Both
                            }
                        }));
                        tr.AppendChild(tc);
                        table.AppendChild(tr);
                    }
                    docBody.AppendChild(table);
                }
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        /// <summary>
        /// Настройки страницы
        /// </summary>
        /// <returns></returns>
        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize { Orient = PageOrientationValues.Portrait };
            properties.AppendChild(pageSize);
            return properties;
        }

        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph == null) return null;
            Paragraph docParagraph = new Paragraph();
            docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
            if (!paragraph.ParagraphIsCell)
            {
                docParagraph.AppendChild(CreateBoldText(paragraph, 0));
                for (int i = 1; i < paragraph.Texts.Count; i++)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = paragraph.TextProperties.Size });
                    if (paragraph.TextProperties.Bold)
                        properties.AppendChild(new Bold());
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text { Text = " " + paragraph.Texts[i], Space = SpaceProcessingModeValues.Preserve });
                    docParagraph.AppendChild(docRun);
                }
            }
            else
            {
                Run docRun = new Run();
                RunProperties properties = new RunProperties();
                properties.AppendChild(new FontSize { Val = paragraph.TextProperties.Size });
                docRun.AppendChild(properties);
                docRun.AppendChild(new Text { Text = paragraph.Texts[0], Space = SpaceProcessingModeValues.Preserve });
                docParagraph.AppendChild(docRun);
            }
            return docParagraph;
        }

        private static Run CreateBoldText(WordParagraph paragraph, int index)
        {
            Run docRun = new Run();
            RunProperties properties = new RunProperties();
            properties.AppendChild(new FontSize { Val = paragraph.TextProperties.Size });
            properties.AppendChild(new Bold());
            docRun.AppendChild(properties);
            docRun.AppendChild(new Text { Text = paragraph.Texts[index], Space = SpaceProcessingModeValues.Preserve });
            return docRun;
        }

        /// <summary>
        /// Задание форматирования для абзаца
        /// </summary>
        /// <param name="paragraphProperties"></param>
        /// <returns></returns>
        private static ParagraphProperties CreateParagraphProperties(WordParagraphProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification() { Val = paragraphProperties.JustificationValues });
                properties.AppendChild(new SpacingBetweenLines { LineRule = LineSpacingRuleValues.Auto });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                    paragraphMarkRunProperties.AppendChild(new FontSize { Val = paragraphProperties.Size });
                if (paragraphProperties.Bold)
                    paragraphMarkRunProperties.AppendChild(new Bold());
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }

        /// <summary>
        /// Задание форматирования для таблицы
        /// </summary>
        /// <param name="paragraphProperties"></param>
        /// <returns></returns>
        private static TableProperties CreateTableProperties()
        {
            TableProperties tp = new TableProperties(
                new TableBorders(
                    new TopBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new BottomBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new LeftBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new RightBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new InsideHorizontalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new InsideVerticalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    }));
            return tp;
        }
    }
}
