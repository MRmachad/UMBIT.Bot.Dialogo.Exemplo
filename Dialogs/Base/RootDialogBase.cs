using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace UMBIT.Bot.Dialogo.Exemplo.Dialogs.Base
{
    public abstract class RootDialogBase : ComponentDialog
    {
        public RootDialogBase(string nameRoot) : base(nameRoot) { }


        public override Task<DialogTurnResult> BeginDialogAsync(DialogContext outerDc, object options = null, CancellationToken cancellationToken = default)
        {
            return base.BeginDialogAsync(outerDc, options, cancellationToken);
        }

        public override Task<DialogTurnResult> ContinueDialogAsync(DialogContext outerDc, CancellationToken cancellationToken = default)
        {
            if (this.UsuariofinalizaDialogo(outerDc))
            {
                outerDc.Context.SendActivityAsync(MessageFactory.Text("Até Mais Meu chegado"));
                outerDc.CancelAllDialogsAsync();
                return outerDc.EndDialogAsync(outerDc,cancellationToken);
            }

            return base.ContinueDialogAsync(outerDc, cancellationToken);

        }

        


        private bool UsuariofinalizaDialogo(DialogContext outerDc)
        {
            return outerDc.Context.Activity.Text == "tchau";
        }
    }
}
