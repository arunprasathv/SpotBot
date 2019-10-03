using Microsoft.Bot.Builder.Dialogs;

namespace Hackathon.SpotBot
{
    public class CustomerSupportTemplateState : DialogState
    {
        public bool IntroSent { get; set; }
        public string SelectedOption { get; set; }

        public string AdvertiserName { get; set; }
        public Order Order { get; set; }
    }
}
