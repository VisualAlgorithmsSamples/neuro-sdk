#nullable enable

using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using NeuroSdk.Websocket;

namespace NeuroSdk.Actions
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public interface INeuroAction
    {
        string Name { get; }

        ActionWindow? ActionWindow { get; }

        bool CanAddToActionWindow(ActionWindow actionWindow);

        ExecutionResult Validate(ActionJData actionData, out object? data);
        UniTask ExecuteAsync(object? data);

        WsAction GetWsAction();

        public void SetActionWindow(ActionWindow actionWindow);
    }
}
