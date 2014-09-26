using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Drawing;

using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using TableRow = DocumentFormat.OpenXml.Wordprocessing.TableRow;
using TableCell = DocumentFormat.OpenXml.Wordprocessing.TableCell;
using FontSize = DocumentFormat.OpenXml.Wordprocessing.FontSize;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

public partial class Report : System.Web.UI.Page
{
    Dictionary<string, string> dict;
    int maxBaby = 6;  //months for a baby

    Body body;
    Paragraph paragraph;
    bool firstLine = false;
    TableCell tcLetter;

    protected void Page_Load(object sender, EventArgs e)
    {

        string examID = Request.QueryString["ExamID"];

        string cmdText;

        //checking whether the examid exists
        if (examID == null)
            examID = "";
        if (examID != "")
        {
            cmdText = "SELECT ExamID FROM Exam WHERE ExamID = " + examID;
            object retValue = DBUtil.ExecuteScalar(cmdText);
            if (retValue == null)
                examID = "";
        }

        if (examID == "")
            throw new ApplicationException("Invalid Exam Notes");

        //getting the Exam details from database 
        cmdText = "SELECT p.FirstName + ' ' + p.LastName as PatientName, ExamDate, ExamText, DateOfBirth, u.FirstName + ' ' + u.LastName as DoctorName FROM Patient p JOIN EXAM e on e.PatientID = p.PatientID JOIN [User] u ON u.UserName = e.UserName WHERE ExamID = " + examID;
        SqlDataReader drExam = DBUtil.ExecuteReader(cmdText);

        drExam.Read();
        DateTime dtExam = Convert.ToDateTime(drExam["ExamDate"]);
        DateTime dob = Convert.ToDateTime(drExam["DateOfBirth"]);
        string filename = drExam["PatientName"].ToString() + " - " + dtExam.ToString("yyyyMMdd");
        string xml = drExam["ExamText"].ToString();
        string examDate = dtExam.ToShortDateString();
        string doctorName = drExam["DoctorName"].ToString();

        drExam.Close();
        drExam.Dispose();

        using (MemoryStream ms = new MemoryStream())
        {

            using (WordprocessingDocument package = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
            {

                MainDocumentPart mainPart = package.AddMainDocumentPart();
                mainPart.Document = new Document();
                body = new Body();
                SectionProperties secprop = new SectionProperties();
                PageMargin pm = new PageMargin() { Top = 395, Right = 395, Bottom = 395, Left = 395 };
                secprop.Append(pm);
                body.Append(secprop);

                mainPart.Document.Append(body);
                //mainPart.Document.Descendants<PageMargin>().First();

                LetterHeader(package);
                GetLetter(xml, dtExam, dob, doctorName);

                package.Close();

            }


            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-word";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".docx");
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            Response.End();

        }
    }

    private void LetterHeader(WordprocessingDocument package)
    {
        Run run;
        Table tableHeader = new Table();
        body.Append(tableHeader);
        TableRow trHeader = new TableRow();
        tableHeader.Append(trHeader);


        //1.43 + 6.56 = 7.99  ==> 2000 + 9200 = 11200

        GetTableCellRun(trHeader, 2000, JustificationValues.Left, 0, true);
        paragraph.Append(GetTextRun("", "Cambria", 18, false, false, 6));
        paragraph.Append(GetTextRun("", "Cambria", 18, false, false, 6));
        //InsertAPicture(package, Server.MapPath(@"/ExamPatient/Images/WordImage.jpg"));
        //InsertPicture(package, Server.MapPath(@"~/Images/WordImage.jpg"));

        run = GetTextRun("30 East 40th Street", "Cambria", 14, false, false, 1);
        run.Append(new Text("Suite 405"));
        run.Append(new Break());
        run.Append(new Text("New York, NY 10016"));
        run.Append(new Break());
        run.Append(new Text("(212) 684-3980"));
        paragraph.Append(run);
        paragraph.Append(GetTextRun("", "Cambria", 16, false, false, 3));

        run = GetTextRun("77 Worth Street", "Cambria", 14, false, false, 1);
        run.Append(new Text("Ground Floor"));
        run.Append(new Break());
        run.Append(new Text("New York, NY 10013"));
        run.Append(new Break());
        run.Append(new Text("(212) 684-3980"));
        paragraph.Append(run);
        paragraph.Append(GetTextRun("", "Cambria", 16, false, false, 3));

        run = GetTextRun("1075 Central Park Avenue", "Cambria", 14, false, false, 1);
        run.Append(new Text("Suite 403"));
        run.Append(new Break());
        run.Append(new Text("Scarsdale, NY 10583"));
        run.Append(new Break());
        run.Append(new Text("(914) 713-3390 "));
        paragraph.Append(run);
        paragraph.Append(GetTextRun("", "Cambria", 16, false, false, 7));

        //Pediatric Ophthalmology
        paragraph.Append(GetTextRun("Pediatric Ophthalmology", "Cambria", 18, true, false, 2));
        paragraph.Append(GetTextRun("Frederick M. Wang, MD", "Cambria", 14, false, false, 0));
        paragraph.Append(GetTextRun("", "Cambria", 10, false, false, 2));
        paragraph.Append(GetTextRun("Brian Campolattaro, MD", "Cambria", 14, false, false, 0));
        paragraph.Append(GetTextRun("", "Cambria", 10, false, false, 2));
        paragraph.Append(GetTextRun("Anthony Panarelli, MD", "Cambria", 14, false, false, 0));
        paragraph.Append(GetTextRun("", "Cambria", 10, false, false, 2));
        paragraph.Append(GetTextRun("Idil Bilgin, MD", "Cambria", 14, false, false, 0));
        paragraph.Append(GetTextRun("", "Cambria", 16, true, false, 4));

        //Optometry
        //paragraph.Append(GetTextRun("Optometry", "Cambria", 18, true, false, 2));
        //paragraph.Append(GetTextRun("Kenneth H. Sorkin, OD", "Cambria", 14, false, false, 0));
        //paragraph.Append(GetTextRun("", "Cambria", 10, false, false, 2));
        //paragraph.Append(GetTextRun("Jill A. Marcus, OD", "Cambria", 14, false, false, 0));
        //paragraph.Append(GetTextRun("", "Cambria", 16, true, false, 4));

        //Orthoptics
        //paragraph.Append(GetTextRun("Orthoptics", "Cambria", 18, true, false, 2));
        //paragraph.Append(GetTextRun("Ricki Cohen-Marro, CO", "Cambria", 14, false, false, 0));
        //paragraph.Append(GetTextRun("", "Cambria", 10, false, false, 2));
        //paragraph.Append(GetTextRun("Leslie Blatt-Englander, CO", "Cambria", 14, false, false, 0));
        //paragraph.Append(GetTextRun("", "Cambria", 10, false, false, 2));
        //paragraph.Append(GetTextRun("Rachel Krawiec, CO", "Cambria", 14, false, false, 0));
        //paragraph.Append(GetTextRun("", "Cambria", 16, true, false, 4));

        //Administration
        //paragraph.Append(GetTextRun("Administration", "Cambria", 18, true, false, 2));
        //paragraph.Append(GetTextRun("Jaime Metzger", "Cambria", 14, false, false, 0));
        //paragraph.Append(GetTextRun("", "Cambria", 16, true, false, 4));

        paragraph.Append(GetTextRun("", "Cambria", 22, false, false, 2));
        paragraph.Append(GetTextRun("childrenseyeny.com", "Cambria", 18, true, true, 0));
        paragraph.Append(GetTextRun("", "Cambria", 18, false, false, 6));

        GetTableCellRun(trHeader, 9200, JustificationValues.Left, 0, true);
        //run.Append(new Text("Second line"));
    }

    private Run GetTextRun(string text, string fontName, int size, bool bold, bool italic, int breaks)
    {
        Run run = new Run();
        RunProperties runProperties = new RunProperties();

        runProperties.Append(new RunFonts() { Ascii = fontName });
        runProperties.Append(new FontSize() { Val = size.ToString() });

        RunFonts fnts = new RunFonts();

        if (bold)
            runProperties.Append(new Bold());
        if (italic)
            runProperties.Append(new Italic());

        run.Append(runProperties);
        run.Append(new Text(text));

        for (int ibreak = 0; ibreak < breaks; ibreak++)
            run.Append(new Break());

        return run;
    }

    private Run GetTableCellRun(TableRow tr, int width, JustificationValues justification, int fontSize, bool globalTC)
    {
        TableCell tcRun = new TableCell();
        TableCellProperties properties = new TableCellProperties();
        properties.Append(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = StringValue.FromString(width.ToString()) });
        tcRun.Append(properties);

        paragraph = new Paragraph();
        ParagraphProperties paraProperties = new ParagraphProperties();
        paraProperties.Append(new Justification() { Val = justification });
        paraProperties.Append(new SpacingBetweenLines() { After = "10", Line = "240", LineRule = LineSpacingRuleValues.Auto });
        paragraph.Append(paraProperties);
        Run run = new Run();

        if (fontSize != 0)
        {
            RunProperties runProperties = new RunProperties();
            FontSize size = new FontSize();
            size.Val = StringValue.FromString(fontSize.ToString());
            runProperties.Append(size);
            run.Append(runProperties);
        }
        paragraph.Append(run);
        tcRun.Append(paragraph);
        tr.Append(tcRun);

        if (globalTC)
            tcLetter = tcRun;

        return run;
    }

    protected void GetLetter(string xml, DateTime dtExam, DateTime dob, string doctorName)
    {
        Run run;
        Table tableLetter;
        TableRow trLetter;

        dict = WebUtil.GetDictionary(xml, true);


        bool premature = false;

        if (dict["BirthHist1"] == "Premature")
            premature = true;

        if (dict.ContainsKey("Premature"))
            premature = Convert.ToBoolean(dict["Premature"]);


        int patientMonths = monthDifference(dob, DateTime.Now);

        if (patientMonths >= 6)
            premature = false;

        //premature = true;

        paragraph.Append(GetTextRun("", "Cambria", 24, false, false, 1));
        paragraph.Append(GetTextRun(doctorName, "Cambria", 24, true, false, 0));
        paragraph.Append(GetTextRun("", "Cambria", 10, false, false, 3));
        paragraph.Append(GetTextRun("Pediatric Ophthalmology of New York, P.C", "Cambria", 30, true, false, 0));
        paragraph.Append(GetTextRun("", "Century Gothic", 21, false, false, 2));

        //EXAMDATE
        run = GetParaRun();
        run.Append(new Text(dtExam.ToString("MMMM dd, yyyy")));
        //FIRSTNAME & LASTNAME
        run = GetParaRun();
        run.Append(new Text("Your patient " + dict["FirstName"] + " " + dict["LastName"] + " was seen in the office today. Following are details of that examination."));
        run.Append(new Break());


        tableLetter = new Table();
        tcLetter.Append(tableLetter);
        trLetter = new TableRow();
        tableLetter.Append(trLetter);

        run = GetTableCellRun(trLetter, 5000, JustificationValues.Left, 0, false);
        Append("Patient Name", "FirstName", "LastName");
        run = GetTableCellRun(trLetter, 5000, JustificationValues.Left, 0, false);
        Append("Date of Birth", "DOB");

        trLetter = new TableRow();
        tableLetter.Append(trLetter);

        run = GetTableCellRun(trLetter, 5000, JustificationValues.Left, 0, false);
        AppendText("Exam Date: ", dtExam.ToShortDateString());

        //GestAge, BirthWt, PCA
        if (premature)
        {
            trLetter = new TableRow();
            tableLetter.Append(trLetter);

            run = GetTableCellRun(trLetter, 5000, JustificationValues.Left, 0, false);
            Append("Gestational Age", "GA");
            run = GetRun();

            run = GetTableCellRun(trLetter, 5000, JustificationValues.Left, 0, false);
            Append("Birth Weight", "BirthWt");

            trLetter = new TableRow();
            tableLetter.Append(trLetter);
            run = GetTableCellRun(trLetter, 5000, JustificationValues.Left, 0, false);
            Append("Post-conceptional Age", "PCA");
        }


        run = GetParaRun();
        //CHIEFCO & subjhx
        AppendLine("History/Chief Complaint", "Compliant", "SubjectiveHistory");

        //FAMHXLINE
        AppendLine("Family History", "FH1", "FH2", "FH3", "FH4", "FH5", "FH5");

        run = GetParaRun();
        //run.Append(new Break());

        //DistanceVALine
        AppendVALine("VA (uncorrected)", "VAscOD1", "VAscOD2", "DistOS1", "DistOS2", "NoPref");

        //NearVALine
        AppendVALine("Near VA (uncorrected)", "VAOD1", "VAOD2", "NearOS1", "NearOS2", "");

        //VAPresSpecsLine
        AppendVALine("VA in present spectacles", "VAccOD1", "VAccOD2", "DistOS3", "DistOS4", "");

        //BestVALine
        AppendVALine("Best-Corrected VA", "ManVAOD1", "ManVAOD2", "ManVSOS1", "ManVSOS2", "");

        //VATESTWITH
        //This is removed in the web application

        run = GetParaRun();
        //Pupils
        AppendLine("Pupils", "PupilOD1", "PupilOD2", "PupilOS1", "PupilOS2", "Pupil");

        //ConfVFLine
        AppendLine("Visual Fields", "Confront1", "Confront2", "Confront3");

        //OCMO
        AppendLine("Ocular motility testing", "OcularMotility6", "OcularMotility1", "OcularMotility2", "OcularMotility5", "OcularMotility4");

        //OCVE
        AppendLine("Ocular versions", "OcularVersions1", "OcularVersions11", "OcularVersions2", "OcularVersions3", "OcularVersions31", "OcularVersions4", "OcularVersions5");
        AppendLine("", "Binocularity1", "Binocularity2", "Binocularity3", "Binocularity4");
        AppendLine("Stereoacuity (Titmus)", "Stereo1");

        //ANTSEG4
        AppendLine("Slit lamp/Anterior segment examination", "AnteriorSegment");

        //TONOOD & TONOOS
        //AppendLine("Tonometry", "Tono1", "Tono2", "Tono3");
        //TONOOD & TONOOS
        if (dict["Tono1"] != "")
        {
            string tono = "Tonometry" + dict["Tono1"] + ": ";
            string tono2 = dict["Tono2"];
            string tonoodos;

            dict["Tono3"] = dict["Tono3"] == "STP" ? "soft to palpation" : dict["Tono3"];
            dict["Tono4"] = dict["Tono4"] == "STP" ? "soft to palpation" : dict["Tono4"];
            if (dict["Tono3"] == dict["Tono4"])
                tonoodos = dict["Tono3"] + " OU";
            else
                tonoodos = dict["Tono3"] + " OD " + dict["Tono4"] + " OS";
            if (firstLine)
            {
                run = GetRun();
                run.Append(new Break());
            }
            AppendText(tono, tono2 + " " + tonoodos);
        }


        if (dict["Dilate3"] != "Not dilated" && dict["Dilate3"] != "")
        {
            //DILATEDWITH
            run = GetParaRun();
            run.Append(new Text("The patient was dilated using " + dict["Dilate3"]));

            if (dict["CycRfxOD"] != "" || dict["CycVAOD3"] != "" || dict["CycVAOD4"] != "" || dict["CycRfxOS"] != "" || dict["CycVSOS1"] != "" || dict["CycVSOS2"] != "")
            {
                run.Append(new Break());
                tableLetter = new Table();
                tcLetter.Append(tableLetter);
                trLetter = new TableRow();
                tableLetter.Append(trLetter);

                run = GetTableCellRun(trLetter, 5000, JustificationValues.Left, 0, false);
                Append("Cycloplegic Retinoscopy OD", "CycRfxOD");
                run = GetTableCellRun(trLetter, 5000, JustificationValues.Left, 0, false);
                Append("VA", "CycVAOD3", "CycVAOD4");

                trLetter = new TableRow();
                tableLetter.Append(trLetter);

                run = GetTableCellRun(trLetter, 7000, JustificationValues.Left, 0, false);
                Append("Cycloplegic Retinoscopy OS", "CycRfxOS");
                run = GetTableCellRun(trLetter, 3000, JustificationValues.Left, 0, false);
                Append("VA", "CycVSOS1", "CycVSOS2");
            }
        }

        if (dict["Fundus1"] != "" && dict["Fundus2"] != "")
        {
            run = GetParaRun();
            //DF, ONH, CUPDISCLINE
            if (dict["Dilate3"] != "Not dilated")
                run.Append(new Text("Dilated fundus"));
            else
                run.Append(new Text("Fundus"));
            run.Append(new Text(" examination reveals optic nerve heads which are " + dict["Fundus1"] + " with " + dict["Fundus2"] + ". " + dict["RetinaOU"]) { Space = SpaceProcessingModeValues.Preserve });
        }

        run = GetParaRun();

        //AGE, SEX1, SUMMARY
        //if (premature)
        //{
        //    AppendText("Assessment: ", dict["GA"]+" "+dict["BirthWt"] + " premature newborn, now at  " + dict["PCA"] + " PCA, " + dict["Summary"]);
        //}
        //else
        //{
            AppendText("Assessment: ", dict["Summary"]);
        //}

        //BROUGHTBY
        if (dict.ContainsKey("Discussed"))
        {
            AppendLine("", "Discussed");
        }
        //PLAN
        AppendLine("Plan", "Advised");

        //FOLLOWUP
        AppendLine("Follow-Up", "FollowUp1", "FollowUp2", "FollowUp4");

        //CC
        run = GetParaRun();
        AppendLine("Letter To", "CopyTo");

    }

    private Run GetParaRun(bool linebreak = true)
    {
        paragraph = new Paragraph();
        ParagraphProperties paraProperties = new ParagraphProperties();
        paraProperties.Append(new SpacingBetweenLines() { After = "10", Line = "240", LineRule = LineSpacingRuleValues.Auto });
        paragraph.Append(paraProperties);
        tcLetter.Append(paragraph);
        Run run = GetRun();
        if (linebreak)
            run.Append(new Break());
        firstLine = false;
        return run;
    }

    private Run GetRun()
    {
        Run run = new Run();
        RunProperties runProperties = new RunProperties();
        runProperties.Append(new RunFonts() { Ascii = "Century Gothic" });
        runProperties.Append(new FontSize() { Val = "21" });
        run.Append(runProperties);
        paragraph.Append(run);
        return run;
    }

    private void AppendText(string title, string text)
    {
        Run run = GetRun();
        RunProperties runProperties = new RunProperties();
        runProperties.Append(new Bold());
        run.Append(runProperties);
        run.Append(new Text(title) { Space = SpaceProcessingModeValues.Preserve });

        run = GetRun();
        run.Append(new Text(text));
        firstLine = true; //once we write a line then it is not a first line
    }

    private void Append(string title, params string[] keys)
    {
        AppendLine(false, title, keys);
    }

    private void AppendLine(string title, params string[] keys)
    {
        AppendLine(firstLine, title, keys);
    }

    private void AppendLine(bool newLine, string title, params string[] keys)
    {
        string str = "";
        foreach (string key in keys)
        {
            if (dict[key].Trim() != "")
                str += dict[key].Trim() + " ";
        }

        if (str != "")
        {
            if (newLine)
            {
                Run run = GetRun();
                run.Append(new Break());
            }
            title = title == "" ? "" : title + ": ";
            AppendText(title, str.Trim());
        }
    }

    private void AppendVALine(string title, String od1, string od2, string os1, string os2, string noPref)
    {
        bool bNoPref = false;

        if (noPref != "" & dict.ContainsKey(noPref))
            bNoPref = Convert.ToBoolean(dict[noPref]);

        string od = (dict[od1] + " " + dict[od2]).Trim();
        string os = (dict[os1] + " " + dict[os2]).Trim();
        string ou = "";

        if (od == os)
            ou = od;

        od = od == "" ? od : od + " OD ";
        os = os == "" ? os : os + " OS";
        ou = ou == "" ? ou : ou + " OU";

        if (bNoPref && ou != "")
            ou += ". There is no preference demonstrated for either eye.";

        if (od != "" || os != "")
        {
            if (firstLine)
            {
                Run run = GetRun();
                run.Append(new Break());
            }

            if (ou != "")
                AppendText(title + ": ", ou);
            else
                AppendText(title + ": ", od + os);
        }
    }


    private int monthDifference(DateTime startDate, DateTime endDate)
    {
        int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
        return Math.Abs(monthsApart);
    }

    public void InsertAPicture(WordprocessingDocument wordprocessingDocument, string fileName)
    {
        MainDocumentPart mainPart = wordprocessingDocument.MainDocumentPart;

        ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);

        using (FileStream stream = new FileStream(fileName, FileMode.Open))
        {
            imagePart.FeedData(stream);
        }

        AddImageToBodyOld(wordprocessingDocument, mainPart.GetIdOfPart(imagePart));
    }

    private void AddImageToBodyOld(WordprocessingDocument wordDoc, string relationshipId)
    {
        // Define the reference of the image.
        var element =
             new Drawing(
                 new DW.Inline(
                     new DW.Extent() { Cx = 990000L, Cy = 792000L },
                     new DW.EffectExtent()
                     {
                         LeftEdge = 0L,
                         TopEdge = 0L,
                         RightEdge = 0L,
                         BottomEdge = 0L
                     },
                     new DW.DocProperties()
                     {
                         Id = (UInt32Value)1U,
                         Name = "Picture 1"
                     },
                     new DW.NonVisualGraphicFrameDrawingProperties(
                         new A.GraphicFrameLocks() { NoChangeAspect = true }),
                     new A.Graphic(
                         new A.GraphicData(
                             new PIC.Picture(
                                 new PIC.NonVisualPictureProperties(
                                     new PIC.NonVisualDrawingProperties()
                                     {
                                         Id = (UInt32Value)0U,
                                         Name = "New Bitmap Image.jpg"
                                     },
                                     new PIC.NonVisualPictureDrawingProperties()),
                                 new PIC.BlipFill(
                                     new A.Blip(
                                         new A.BlipExtensionList(
                                             new A.BlipExtension()
                                             {
                                                 Uri =
                                                   "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                             })
                                     )
                                     {
                                         Embed = relationshipId,
                                         CompressionState =
                                         A.BlipCompressionValues.Print
                                     },
                                     new A.Stretch(
                                         new A.FillRectangle())),
                                 new PIC.ShapeProperties(
                                     new A.Transform2D(
                                         new A.Offset() { X = 0L, Y = 0L },
                                         new A.Extents() { Cx = 990000L, Cy = 792000L }),
                                     new A.PresetGeometry(
                                         new A.AdjustValueList()
                                     ) { Preset = A.ShapeTypeValues.Rectangle }))
                         ) { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                 )
                 {
                     DistanceFromTop = (UInt32Value)0U,
                     DistanceFromBottom = (UInt32Value)0U,
                     DistanceFromLeft = (UInt32Value)0U,
                     DistanceFromRight = (UInt32Value)0U,
                     EditId = "50D07946"
                 });

        // Append the reference to body, the element should be in a Run.
        //wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
        paragraph.AppendChild(element);
    }
    private void InsertPicture(WordprocessingDocument wordprocessingDocument, string fileName)
    {
        // Start adding our image
        MainDocumentPart mainPart = wordprocessingDocument.MainDocumentPart;
        ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg, "rId1");
        long imageWidthEMU = 914400 * 50;
        long imageHeightEMU = 914400 * 50;
        UInt32Value ul1 = 1;
        UInt32Value ul2 = 2;

        GenerateImagePart(imagePart, fileName, ref imageWidthEMU, ref imageHeightEMU);

        AddImageToBody(wordprocessingDocument, mainPart.GetIdOfPart(imagePart), imageWidthEMU, imageHeightEMU, ul1, ul2);
        //AddImageToBodyOld(wordprocessingDocument, mainPart.GetIdOfPart(imagePart));
    }

    private void GenerateImagePart(OpenXmlPart part, string imagePath, ref long imageWidthEMU, ref long imageHeightEMU)
    {
        byte[] imageFileBytes;
        Bitmap imageFile;

        using (FileStream fsImageFile = File.OpenRead(imagePath))
        {
            imageFileBytes = new byte[fsImageFile.Length];
            fsImageFile.Read(imageFileBytes, 0, imageFileBytes.Length);

            imageFile = new Bitmap(fsImageFile);
        }

        imageWidthEMU = (long)((imageFile.Width / imageFile.HorizontalResolution) * 914400L * 7);
        imageHeightEMU = (long)((imageFile.Height / imageFile.VerticalResolution) * 914400L * 7);

        using (BinaryWriter writer = new BinaryWriter(part.GetStream()))
        {
            writer.Write(imageFileBytes);
            writer.Flush();
        }
    }

    private void AddImageToBody(WordprocessingDocument wordDoc, string relationshipId, long imageWidthEMU, long imageHeightEMU, UInt32 ID1, UInt32 ID2)
    {
        string GraphicDataUri = "http://schemas.openxmlformats.org/drawingml/2006/picture";
        double imageWidthInInches = imageWidthEMU / 914400.0;
        double imageHeightInInches = imageHeightEMU / 914400.0;

        long horizontalWrapPolygonUnitsPerInch = (long)(21600L / imageWidthInInches);

        long verticalWrapPolygonUnitsPerInch = (long)(21600L / imageHeightInInches);

        //Define the reference of the image.
        var element =
             new Drawing(
                 new DW.Inline(
                     new DW.Extent() { Cx = imageWidthEMU, Cy = imageHeightEMU },
                     new DW.EffectExtent()
                     {
                         LeftEdge = 19050L,
                         TopEdge = 0L,
                         RightEdge = 9525L,
                         BottomEdge = 0L
                     },
                     new DW.DocProperties()
                     {
                         Id = ID1,// (UInt32Value)1U,
                         Name = relationshipId,
                         Description = "Description_" + relationshipId
                     },
                     new DW.NonVisualGraphicFrameDrawingProperties(
                         new A.GraphicFrameLocks() { NoChangeAspect = true }),
                     new A.Graphic(
                         new A.GraphicData(
                             new PIC.Picture(
                                 new PIC.NonVisualPictureProperties(
                                     new PIC.NonVisualDrawingProperties()
                                     {
                                         Id = ID2,// (UInt32Value)2U,
                                         Name = relationshipId,
                                         Description = "Description_" + relationshipId
                                     },
                                     new PIC.NonVisualPictureDrawingProperties()),
                                 new PIC.BlipFill(
                                     new A.Blip(
                                         new A.BlipExtensionList(
                                             new A.BlipExtension()
                                             {
                                                 Uri = GraphicDataUri
                                             })
                                     )
                                     {
                                         Embed = relationshipId,
                                         CompressionState = A.BlipCompressionValues.Print
                                     },
                                     new A.Stretch(
                                         new A.FillRectangle())),
                                 new PIC.ShapeProperties(
                                     new A.Transform2D(
                                         new A.Offset() { X = 0L, Y = 0L },
                                         new A.Extents() { Cx = imageWidthEMU, Cy = imageHeightEMU }),
                                     new A.PresetGeometry(
                                         new A.AdjustValueList()
                                     ) { Preset = A.ShapeTypeValues.Rectangle }))
                         ) { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                 )
                 {
                     DistanceFromTop = (UInt32Value)0U,
                     DistanceFromBottom = (UInt32Value)0U,
                     DistanceFromLeft = (UInt32Value)0U,
                     DistanceFromRight = (UInt32Value)0U
                 });

        //Append the reference to body, the element should be in a Run.
        //body.AppendChild(new Paragraph(new Run(element)));
        paragraph.AppendChild(element);
    }
}
