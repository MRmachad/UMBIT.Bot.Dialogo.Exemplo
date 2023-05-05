using Microsoft.AspNetCore.Http;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Identity.Client;
using System.Threading;
using System.Threading.Tasks;
using UMBIT.Bot.Dialogo.Exemplo.Dialogs.Base;

namespace UMBIT.Bot.Dialogo.Exemplo.Dialogs
{
    public class RootDialog : RootDialogBase
    {
        public RootDialog(UserState userState) : base("root")
        {

            this.AddComponentesDeDialogo();

            AddDialog(new WaterfallDialog("waterfall_inicial", new WaterfallStep[] { this.InicioDeDialogoStep, this.ProcessamentoDeResultadoStep }));
            AddDialog(new WaterfallDialog("waterfall_Continue", new WaterfallStep[] { this.InicioDeDialogoStepContinue, this.ProcessamentoDeResultadoStep }));
            InitialDialogId = "waterfall_inicial";
        }

        private void AddComponentesDeDialogo()
        {
            this.AddDialog(new TextPrompt("reply"));
        }

        private async Task<DialogTurnResult> InicioDeDialogoStep(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.BeginDialogAsync(
                "reply",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Diga Algo!"),
                    RetryPrompt = MessageFactory.Text("Diga Algo!"),
                },
                cancellationToken);
        }
        private async Task<DialogTurnResult> ProcessamentoDeResultadoStep(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Você Disse! {stepContext.Context.Activity.Text}"), cancellationToken);

            return await stepContext.BeginDialogAsync(
                "waterfall_Continue",
                null,
                cancellationToken);
        }
        private async Task<DialogTurnResult> InicioDeDialogoStepContinue(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.BeginDialogAsync(
                "reply",
                new PromptOptions(),
                cancellationToken);
        }

        public override Task EndDialogAsync(ITurnContext turnContext, DialogInstance instance, DialogReason reason, CancellationToken cancellationToken = default)
        {
            return base.EndDialogAsync(turnContext, instance, reason, cancellationToken);
        }
    }
}
