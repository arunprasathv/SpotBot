 

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
 using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Hackathon.SpotBot;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Microsoft.Bot.Schema;
using Hackathon.SpotBot.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
using Newtonsoft.Json;
using System.Net.Mail;


namespace Hackathon.SpotBot
{
    public class MainDialog : ComponentDialog
    {
        private IBotServices _services;
        private UserState _userState;
        private ConversationState _conversationState;
        private IStatePropertyAccessor<CustomerSupportTemplateState> _stateAccessor;
        private MainResponses _responder = new MainResponses();
        private readonly BotStateService _botStateService;
        private PortalContext _portalContext;
        private IServiceClient _client;

        public MainDialog(BotStateService botStateService, IBotServices services, ConversationState conversationState, UserState userState, PortalContext portalContext)
            : base(nameof(MainDialog))
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _conversationState = conversationState;
            _userState = userState;
            _portalContext = portalContext;
            _stateAccessor = _conversationState.CreateProperty<CustomerSupportTemplateState>(nameof(CustomerSupportTemplateState));
            _botStateService = botStateService ?? throw new System.ArgumentNullException(nameof(botStateService));
            _client = new DemoServiceClient();
            InitializeWaterfallDialog();

            RegisterDialogs();
        }

        private void InitializeWaterfallDialog()
        {
            // Create Waterfall Steps
            var waterfallSteps = new WaterfallStep[]
            {
                InitialStepAsync,
                FinalStepAsync
            };

            // Add Named Dialogs
            //    AddDialog(new GreetingDialog($"{nameof(MainDialog)}.greeting", _botStateService));
            //  AddDialog(new BugReportDialog($"{nameof(MainDialog)}.bugReport", _botStateService));

            AddDialog(new WaterfallDialog($"{nameof(MainDialog)}.mainFlow", waterfallSteps));

            // Set the starting Dialog
            InitialDialogId = $"{nameof(MainDialog)}.mainFlow";
        }


        private async Task<DialogTurnResult> InitialStepAsync(DialogContext stepContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            var routeResult = EndOfTurn;

            // Check dispatch result
            var dispatchResult = await _services.Dispatch.RecognizeAsync(stepContext.Context, CancellationToken.None);
            var getIntent = dispatchResult.GetTopScoringIntent();
           var  topIntent = getIntent.intent;
            // this is for multiple luis applications
            //            var luisResult = dispatchResult.Properties["luisResult"] as LuisResult;
            //          var result = luisResult.ConnectedServiceResult;
            //        var topIntent = result.TopScoringIntent.Intent;

            var activity = (stepContext.Context).Activity as Activity;
            dynamic activityValue = activity.Value ?? null;
            if (activityValue != null && activityValue.source.ToString() == "Spots")
            {
                if (activityValue.id == "pdf")
                    ExportAsPDF();
                if (activityValue.id == "email")
                    CreateEmailMessage(activityValue);
                topIntent = "Help";
            }

            if (topIntent == "Order_Details" || topIntent == "GetOrder")
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.Order", null, cancellationToken);
                //routeResult = await dc.BeginDialogAsync(nameof(CheckOrderStatusDialog));
            }
            else if (topIntent == "Payment")
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.Payment", null, cancellationToken);
            }
            else if (topIntent == "Order_Performance")
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.OrderPerformance", null, cancellationToken);
            }
            else if (topIntent == "Spots")
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.Spots", null, cancellationToken);
            }
            else if (topIntent == "Greeting")
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.Greetings", null, cancellationToken);
            }
            else if (topIntent == "Help")
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.Help", null, cancellationToken);
            }
            else if (topIntent == "Commission")
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.Commission", null, cancellationToken);
            }
            else if (topIntent == "CAM_Account")
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.CAM_Account", null, cancellationToken);
            }
            else if (topIntent == "Invoice_Summary")
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.InvoiceSummary", null, cancellationToken);
            }
            else if (topIntent == "CAP_PAYMENT")
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.LatestPayment", null, cancellationToken);
            }
            else if (topIntent == "Goodbye")
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.Goodbye", null, cancellationToken);
            }
            else
            {
                return await stepContext.BeginDialogAsync($"{nameof(MainDialog)}.Confused", null, cancellationToken);
            }
        }
        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await _responder.ReplyWith(stepContext.Context, "What else can I help you with?");
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }
        private void RegisterDialogs()
        {
            AddDialog(new CheckOrderStatusDialog($"{nameof(MainDialog)}.Order", _botStateService, _services));
            AddDialog(new CheckPaymentDialog($"{nameof(MainDialog)}.Payment", _botStateService, _services));
            AddDialog(new GreetingDialog($"{nameof(MainDialog)}.Greetings", _botStateService, _services));
            AddDialog(new ConfusedDialog($"{nameof(MainDialog)}.Confused", _botStateService, _services));
            AddDialog(new HelpDialog($"{nameof(MainDialog)}.Help", _botStateService, _services));
            AddDialog(new CheckOrderPerformanceDialog($"{nameof(MainDialog)}.OrderPerformance", _botStateService, _services));
            AddDialog(new CheckSpotDataDialog($"{nameof(MainDialog)}.Spots", _botStateService, _services));
            AddDialog(new CommissionDialog($"{nameof(MainDialog)}.Commission", _botStateService, _services));
            AddDialog(new AdvertiserDialog($"{nameof(MainDialog)}.CAM_Account", _botStateService, _services));
            AddDialog(new InvoiceSummaryDialog($"{nameof(MainDialog)}.InvoiceSummary", _botStateService, _services, _portalContext));
            AddDialog(new LatestPaymentDialog($"{nameof(MainDialog)}.LatestPayment", _botStateService, _services, _portalContext));
            AddDialog(new GoodbyeDialog($"{nameof(MainDialog)}.Goodbye", _botStateService, _services));
        }

        private void CreateEmailMessage(dynamic emailValue)
        {
            var spotData = _client.GetSpotData("2805639");
            var spotJson = JsonConvert.SerializeObject(spotData);

            SmtpClient SmtpServer = new SmtpClient("mailrelay.comcast.com");
            MailMessage mail = SendMailCore(spotJson, emailValue);

            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

        }

        private static MailMessage SendMailCore(string spotJson, dynamic emailValue)
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("snair204@comcast.com"),
                Subject = "Details of spots played for order 2805639 requested by " + emailValue.name,
                Body = spotJson,
                IsBodyHtml = true
            };
            mail.To.Add(emailValue.email.ToString());

            return mail;
        }

        private void ExportAsPDF()
        {
            Document doc = new Document(PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(@"spotsPlayed.pdf", FileMode.Create));
            doc.Open();

            //Add border to page
            PdfContentByte content = w.DirectContent;
            iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(doc.PageSize);
            //customized border sizes
            rectangle.Left += doc.LeftMargin - 5;
            rectangle.Right -= doc.RightMargin - 5;
            rectangle.Top -= doc.TopMargin - 5;
            rectangle.Bottom += doc.BottomMargin - 5;
            content.SetColorStroke(BaseColor.BLACK); // setting the color of the border to black
            content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
            content.Stroke();

            //setting font type, font size and font color
            iTextSharp.text.Font font = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES_ROMAN, 30, BaseColor.BLUE);
            Paragraph prg = new Paragraph();
            prg.Alignment = Element.ALIGN_CENTER; // adjust the alignment of the heading
            prg.Add(new Chunk("Details of all spots played", font)); //adding a heading to the PDF
            doc.Add(prg); //add the component we created to the document

            // Data
            var spotData = _client.GetSpotData(" ");
            var spotJson = JsonConvert.SerializeObject(spotData);
            Type t = spotData[0].GetType();
            PdfPTable table = new PdfPTable(t.GetProperties().Count());

            for (int j = 0; j < t.GetProperties().Count(); j++)
            {
                PdfPCell cell = new PdfPCell(); //create object from the pdfpcell class
                cell.BackgroundColor = BaseColor.LIGHT_GRAY; //set color of cells to gray
                cell.AddElement(new Chunk(t.GetProperties()[j].Name.ToUpper(), font));
                table.AddCell(cell);
            }

            //add actual rows from grid to table
            for (int i = 0; i < spotData.Count; i++)
            {
                table.WidthPercentage = 100; //set width of the table
                for (int k = 0; k < t.GetProperties().Count(); k++)
                {
                    // get the value of each cell in the dataTable tblemp
                    table.AddCell(new Phrase(spotData[i].SpotID.ToString()));
                }
            }

            //doc.Add(table);

            Paragraph spotParagraph = new Paragraph(spotJson, FontFactory.GetFont("Tahoma", 14));
            doc.Add(spotParagraph);

            doc.Close();
        }
    }
}
