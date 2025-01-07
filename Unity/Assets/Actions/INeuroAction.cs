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

        /// <summary>
        /// If this returns false, the action won't be added to the action window.
        /// </summary>
        bool CanAddToActionWindow(ActionWindow actionWindow);

        ExecutionResult Validate(ActionJData actionData, out object? data);
        UniTask ExecuteAsync(object? data);

        WsAction GetWsAction();

        void SetActionWindow(ActionWindow actionWindow);
    }
}
